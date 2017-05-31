using System;
using System.Runtime.Serialization;

namespace ScheduledTaskAdapter
{
    [Serializable]
	internal class ScheduledException : ApplicationException
	{
		public static string ScheduledErrorString = "The Scheduled Task Adapter encounted an error.";

		public ScheduledException () { }

		public ScheduledException (string msg) : base(msg) { }

		public ScheduledException (Exception inner) : base(String.Empty, inner) { }

		public ScheduledException (string msg, Exception e) : base(msg, e) { }

		protected ScheduledException (SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}