using System;
using System.Transactions;
using System.IO;
using System.Net;

namespace ScheduledTaskAdapter.TaskComponents
{
	public interface IScheduledTaskStreamProvider
	{
		Stream GetStream( object args);
		System.Type GetParameterType();
	}

    public interface IScheduledTaskStreamProvider2
    {
        Stream GetStreams(object args, out CommittableTransaction transaction);
        void Done(bool success);
        System.Type GetParameterType();
    }
}
