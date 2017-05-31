using System;
using System.IO;
using System.ComponentModel;

namespace ScheduledTaskAdapter.TaskComponents
{
	[Serializable()]
	public class FileArguments
	{
		private string filename = string.Empty;
		
		[Description("The file that is read by this scheduled task."),
        CategoryAttribute("Document Specifications"),
        EditorAttribute("ScheduledTaskAdapter.Admin.OpenFileUITypeEditor, ScheduledTaskAdapter.Admin, Version=5.0.0.3, Culture=neutral, PublicKeyToken=aa9f2dd0f13442dc", typeof(System.Drawing.Design.UITypeEditor))]
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
