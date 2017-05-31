using System;
using System.Collections;
using System.Xml;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.TransportProxy.Interop;
using Microsoft.BizTalk.Adapter.Common;

namespace ScheduledTaskAdapter
{
	/// <summary>
	/// Biztalk Adapter Receiver.
	/// </summary>
	public sealed class ScheduledReceiver : Receiver
	{
		public ScheduledReceiver(): 
			base(
					"Scheduled Task Receive Adapter",											//name
					"2.0.0.0",																	//version
					"Schedules a future task, and submits resultant message",					//description
					"Schedule",																	//transport type
                    new Guid("91D61783-DE7E-47ea-BC44-95AF3B825F9D"),							//clsid
					"http://schemas.custom.com/Biztalk/2003/scheduledtask-properties",          //property namespace									
					typeof(ScheduledEndpoint)													//endpoint type	
			 	)
		{
		}

		protected override void HandlerPropertyBagLoaded ()
		{
		}
	}
}
