using System;
using System.IO;
using System.ComponentModel;

namespace ScheduledTask.TaskComponents
{
	[Serializable()]
	public class FileArguments
	{
		private string filename = string.Empty;
		
		[Description("The file that is read by this scheduled task")]
		public string FileName
		{
			get{return filename;}
			set {filename = value;}
		}
	}
	
	/// <summary>
	/// FileStreamProvider: implements the IScheduledTaskStreamProvider interface.
	/// returns the contents of the specified file to the  ScheduledTask Adapter as a stream
	/// </summary>
	public class FileStreamProvider: IScheduledTaskStreamProvider
	{
		public FileStreamProvider()
		{}
		
		public Stream GetStream(object parameter)
		{
			FileArguments args = (FileArguments)parameter;
			if ((args.FileName == string.Empty))
					throw (new ArgumentException("FileStreamProvider requires filename", "filename"));

			return new FileStream(args.FileName,FileMode.Open, FileAccess.Read);
			
		}

		public System.Type GetParameterType()
		{
			return typeof(FileArguments);
		}
	}
}
