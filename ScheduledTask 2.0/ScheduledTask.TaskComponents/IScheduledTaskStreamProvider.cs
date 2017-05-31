using System;
using System.IO;
using System.Net;

namespace ScheduledTask.TaskComponents
{
	public interface IScheduledTaskStreamProvider
	{
		Stream GetStream( object args);
		System.Type GetParameterType();
	}
}
