using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.TransportProxy.Interop;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.Scheduler;
using Microsoft.BizTalk.Streaming;
using Microsoft.BizTalk.Adapter.Common;
using Calendar.Schedules;

namespace ScheduledTaskAdapter
{
	/// <summary>
	/// Biztalk Adapter Receiver Endpoint
	/// </summary>
	internal sealed class ScheduledEndpoint: ReceiverEndpoint, IBTBatchCallBack
	{
		// Fields
		bool initiated = false;
		private string transportType;
        private string uri;
		private ScheduledProperties properties;
		private System.Threading.AutoResetEvent batchFinished = new System.Threading.AutoResetEvent(false);
		private IBTTransportProxy transportProxy;
		private Microsoft.BizTalk.Adapter.Common.ControlledTermination controlledTermination;
		private ScheduledTaskAdapter.TaskName TaskNameProperty = new ScheduledTaskAdapter.TaskName();
		private ScheduledTaskAdapter.NextScheduleTime NextScheduleTimeProperty = new ScheduledTaskAdapter.NextScheduleTime();
		private TaskController taskController;
		public override void Open (string uri, IPropertyBag config, IPropertyBag bizTalkConfig, IPropertyBag handlerPropertyBag, IBTTransportProxy transportProxy, string transportType, string propertyNamespace, ControlledTermination controlledTermination)
		{
			this.transportProxy = transportProxy;
			this.transportType = transportType;
            this.uri = uri;
			this.controlledTermination = controlledTermination;
			this.properties = new ScheduledProperties();
			XmlDocument locationConfigDom = ConfigProperties.ExtractConfigDom(config);
			this.properties.LocationConfiguration(locationConfigDom);
			//  create and schedule a new the task
			this.taskController = new TaskController(new ScheduledTask(this.properties.Name, new ScheduledTask.TaskDelegate(this.ControlledEndpointTask)), this.properties.Schedule);
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
						XmlDocument updatedConfigDom = ConfigProperties.ExtractConfigDom(config);
						lock (this)
						{
							this.properties.LocationConfiguration(updatedConfigDom);
							this.taskController.Schedule = this.properties.Schedule;
							transportProxy.SetErrorInfo(new ScheduledException("\r\n" +  this.properties.Name + ":  scheduled activation changed to  " + this.properties.Schedule.GetNextActivationTime().ToString() ));
						}
					}
				}
			}
		}
		public override void Dispose () 
		{
			transportProxy.SetErrorInfo(new ScheduledException("\r\n" +  this.properties.Name + ":   has been disabled"));
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
				//transportProxy.SetErrorInfo(new ScheduledException("\r\n" +  this.properties.Name + ":  scheduled activation at  " + this.properties.Schedule.GetNextActivationTime().ToString() ));
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
			//Stream readonlystream = null;
			object provider = null;
			object taskArguments = null;
			try
			{
				try
				{
					Assembly streamProviderAssembly = Assembly.Load(this.properties.StreamProviderAssemblyName);
					provider = streamProviderAssembly.CreateInstance(this.properties.StreamProviderTypeName);
				}
				catch( Exception provderException)
				{
					transportProxy.SetErrorInfo(new ApplicationException("Error creating IScheduledTaskStreamProvider interface", provderException));
				}
				if (provider != null)
				{
					//create GetStream argument object
					object [] args = new object [] {};
					object result = provider.GetType().InvokeMember("GetParameterType", BindingFlags.InvokeMethod, null, provider, args);
					if (result != null)
					{
						StringReader strReader = new StringReader(this.properties.StreamProviderArguments);
						XmlSerializer serializer = new XmlSerializer((System.Type)result);
						taskArguments = serializer.Deserialize(strReader);
					}
					//call GetStream
					args = new object[] {taskArguments};
					result = provider.GetType().InvokeMember("GetStream", BindingFlags.InvokeMethod, null, provider, args);
					
					if (result != null)
					{				
						Stream readonlystream = new ReadOnlySeekableStream((Stream)result);
					
						IBaseMessageFactory messageFactory = this.transportProxy.GetMessageFactory();
						IBTTransportBatch batch = transportProxy.GetBatch(this, null);
				
						IBaseMessage message = this.CreateMessage(messageFactory, readonlystream, this.properties.Name);
			
						batch.SubmitMessage(message);
						batch.Done(null);
						this.batchFinished.WaitOne();
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

		public void BatchComplete(int status, short opCount, BTBatchOperationStatus[] operationStatus, object callbackCookie)
		{
			this.batchFinished.Set();
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
				message.Context.Write(TaskNameProperty.Name.Name, TaskNameProperty.Name.Namespace, this.properties.Name);
				message.Context.Write(NextScheduleTimeProperty.Name.Name, NextScheduleTimeProperty.Name.Namespace, this.properties.Schedule.GetNextActivationTime());
				return message;
		}
	}
}


