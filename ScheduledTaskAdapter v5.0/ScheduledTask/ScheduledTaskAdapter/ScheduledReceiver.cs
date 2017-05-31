using System;
using System.Collections;
using System.Xml;
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
					"5.0.0.0",																	//version
					"Schedules a future task, and submits resultant message",					//description
					"Schedule",																	//transport type
                    new Guid("EF1B0E22-88A7-4385-A360-E49B64B31F77"),							//clsid
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
