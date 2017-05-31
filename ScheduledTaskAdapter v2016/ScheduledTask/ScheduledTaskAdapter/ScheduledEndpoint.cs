using System;
using System.IO;
using System.Reflection;
using System.Transactions;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.TransportProxy.Interop;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.Scheduler;
using Microsoft.BizTalk.Streaming;
using Microsoft.BizTalk.Adapter.Common;
using ScheduledTaskAdapter.TaskComponents;
using Calendar.Schedules;

namespace ScheduledTaskAdapter
{
	/// <summary>
	/// Biztalk Adapter Receiver Endpoint
	/// </summary>
	internal sealed class ScheduledEndpoint: ReceiverEndpoint
	{
		// Fields
		bool initiated = false;
		private string transportType;
        private string uri;
        private AdapterConfiguration adapterConfiguration;
        private ManualResetEvent batchFinished = new ManualResetEvent(false);
		private IBTTransportProxy transportProxy;
		private ControlledTermination controlledTermination;
        private TaskController taskController;
		//message context properties
        private TaskName TaskNameProperty = new TaskName();
		private NextScheduleTime NextScheduleTimeProperty = new NextScheduleTime();
		
		public override void Open (string uri, IPropertyBag config, IPropertyBag bizTalkConfig, IPropertyBag handlerPropertyBag, IBTTransportProxy transportProxy, string transportType, string propertyNamespace, ControlledTermination controlledTermination)
		{
			this.transportProxy = transportProxy;
			this.transportType = transportType;
            this.uri = uri;
			this.controlledTermination = controlledTermination;

            this.adapterConfiguration = new AdapterConfiguration();
            object property = null;
            config.Read("AdapterConfig", out property, 0);
            if (property != null)
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml((string)property);
                this.adapterConfiguration.Load(document);
            }
            //  create and schedule a new the task
            this.taskController = new TaskController(new ScheduledTask(this.adapterConfiguration.Name, new ScheduledTask.TaskDelegate(this.ControlledEndpointTask)), this.adapterConfiguration.Schedule);
			this.taskController.StateChanged += new StateChangedEventHandler(this.OnStateChanged);
			this.taskController.Enabled = true;
			this.taskController.Start();
		}
		public override void Update(IPropertyBag config, IPropertyBag bizTalkConfig, IPropertyBag handlerPropertyBag)
		{
			lock (this)
			{
				if (this.taskController.State != State.Running)
				{
					if (config != null)
					{						
						lock (this)
						{
                            object property = null;
                            config.Read("AdapterConfig", out property, 0);
                            if (property != null)
                            {
                                XmlDocument document = new XmlDocument();
                                document.LoadXml((string)property);
                                this.adapterConfiguration.Load(document);
                            }
                            this.taskController.Schedule = this.adapterConfiguration.Schedule;
                            transportProxy.SetErrorInfo(new ScheduledException(string.Format("\r\n{0}:  scheduled activation changed to  {1}", this.adapterConfiguration.Name, this.adapterConfiguration.Schedule.GetNextActivationTime())));
						}
					}
				}
			}
		}
		public override void Dispose () 
		{
            transportProxy.SetErrorInfo(new ScheduledException("\r\n" + this.adapterConfiguration.Name + ":   has been disabled"));
			this.taskController.Stop();
			this.taskController.Enabled = false;
			this.taskController.Dispose();
		}
		private void OnStateChanged(object sender, StateChangedEventArgs args)
		{
		}
		private void ControlledEndpointTask()
		{
			///The taskcontroller always starts the task when initiated and the
			///second run time is determined by a call to ISchedule.GetNextActivationTime()
			///So we skip the first start and wait for our first scheduled time.
			if (!initiated)
			{
				initiated = true;
                //transportProxy.SetErrorInfo(new ScheduledException(string.Format("\r\n{0}:  scheduled activation at  {1}", this.adapterConfiguration.Name, this.adapterConfiguration.Schedule.GetNextActivationTime())));
				return;
			}
			if (this.controlledTermination.Enter())
			{
				this.EndpointTask();
				GC.Collect();
				this.controlledTermination.Leave();
			}
		}
		private void EndpointTask()
		{            
			object provider = null;
            this.adapterConfiguration.Schedule.LastActivated = this.adapterConfiguration.Schedule.NextActivated.Value;
			try
			{
				try
				{
                    provider = this.adapterConfiguration.Task.TaskType.Assembly.CreateInstance(this.adapterConfiguration.Task.TaskType.FullName);
				}
				catch( Exception providerException)
				{
					transportProxy.SetErrorInfo(new ApplicationException("Error creating ScheduledTaskStreamProvider instance", providerException));
				}
				if (provider != null)
				{                    
                    if (provider.GetType().GetInterface(typeof(IScheduledTaskStreamProvider).ToString()) != null)
                    {                        
                        NonTransactionalTask((IScheduledTaskStreamProvider)provider);
                    }
                    else
                    {
                        if (provider.GetType().GetInterface(typeof(IScheduledTaskStreamProvider2).ToString()) != null)
                        {
                            TransactionalTask((IScheduledTaskStreamProvider2)provider);
                        }
                    }					
				}
			}
			catch(Exception exception)
			{
				string errorMessage = exception.Message;
				//transportProxy.SetErrorInfo(exception);
				while (exception.InnerException != null)
				{
					exception = exception.InnerException;
					errorMessage += "\r\n" + exception.Message;
				}
				transportProxy.ReceiverShuttingdown(this.uri, new ScheduledException(errorMessage));
			}
		}
        public void NonTransactionalTask(IScheduledTaskStreamProvider provider)
        {
           //call GetStream
            Stream result = ((IScheduledTaskStreamProvider)provider).GetStream(this.adapterConfiguration.Task.TaskParameters);

            if (result != null)
            {
                Stream readonlystream = new ReadOnlySeekableStream(result);

                ReceiveBatch batch = new ReceiveBatch(transportProxy, controlledTermination, batchFinished, 0);
                
                IBaseMessageFactory messageFactory = this.transportProxy.GetMessageFactory();
                IBaseMessage message = this.CreateMessage(messageFactory, readonlystream, this.adapterConfiguration.Name);
                batch.SubmitMessage(message, new StreamAndUserData(readonlystream, null));
                batch.Done();
                this.batchFinished.WaitOne();               
            }
        }

        public void TransactionalTask(IScheduledTaskStreamProvider2 provider)
        {
            //call GetStream
            CommittableTransaction transaction = null;
           
            Stream result = ((IScheduledTaskStreamProvider2)provider).GetStreams(this.adapterConfiguration.Task.TaskParameters, out transaction);

            if (result != null)
            {
                Batch batch = null;
                if (transaction != null)
                {
                    batch = new ReceiveTxnBatch(transportProxy, controlledTermination, transaction, batchFinished, this.adapterConfiguration.SuspendMessage);
                }
                else
                {
                    batch = new ReceiveBatch(transportProxy, controlledTermination, batchFinished, 0);
                }
                Stream readonlystream = new ReadOnlySeekableStream(result);
                IBaseMessageFactory messageFactory = this.transportProxy.GetMessageFactory();

                IBaseMessage message = this.CreateMessage(messageFactory, readonlystream, this.adapterConfiguration.Name);
                batch.SubmitMessage(message, new StreamAndUserData(readonlystream, null));
                batch.Done();
                this.batchFinished.WaitOne();                
                ((IScheduledTaskStreamProvider2)provider).Done(batch.OverallSuccess);
            }
        }
    
		private IBaseMessage CreateMessage(IBaseMessageFactory messageFactory, Stream stream, string taskName)
		{
				IBaseMessagePart messagePart = messageFactory.CreateMessagePart();
				messagePart.Data = stream;
				IBaseMessage message = messageFactory.CreateMessage();
				message.AddPart("body", messagePart, true);
				SystemMessageContext context = new SystemMessageContext(message.Context);
				context.InboundTransportLocation = this.uri;
				context.InboundTransportType = this.transportType;
                message.Context.Write(TaskNameProperty.Name.Name, TaskNameProperty.Name.Namespace, this.adapterConfiguration.Name);
                message.Context.Write(NextScheduleTimeProperty.Name.Name, NextScheduleTimeProperty.Name.Namespace, this.adapterConfiguration.Schedule.GetNextActivationTime());
				return message;
		}
	}
}


