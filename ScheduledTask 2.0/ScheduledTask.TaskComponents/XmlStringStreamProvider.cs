using System;
using System.ComponentModel;
using System.IO;
using System.Xml;

namespace ScheduledTask.TaskComponents
{
	[Serializable()]
	public class XmlStringArguments
	{
		private string xmlstring = string.Empty;

		[Description("The Xml that will be returned by this scheduled task"),
		EditorAttribute("ScheduledTaskAdapterManagement.XmlTextUITypeEditor, ScheduledTaskAdapterManagement, Version=1.0.2.0, Culture=neutral, PublicKeyToken=29f92781ce46bc36", typeof(System.Drawing.Design.UITypeEditor)),
			TypeConverterAttribute("ScheduledTaskAdapterManagement.XmlTextConverter, Biztalk.Adapter.ScheduledTaskAdmin, Version=1.0.2.0, Culture=neutral, PublicKeyToken=29f92781ce46bc36")]
		public string XmlString
		{
			get{return xmlstring;}
			set {xmlstring = value;}
		}
	}
	/// <summary>
	/// XmlStringStreamProvider: implements the IScheduledTaskStreamProvider interface.
	/// returns the configured Xml String to the  ScheduledTask Adapter as a stream
	/// </summary>
	public class XmlStringStreamProvider: IScheduledTaskStreamProvider
	{
		Stream baseStream;

		public XmlStringStreamProvider()
		{}

		public Stream GetStream(object parameter)
		{
			XmlStringArguments args = (XmlStringArguments)parameter;
			if (args.XmlString == string.Empty)
					throw (new ArgumentException("XmlStreamProvider requires Xml string", "filename"));

			XmlDocument document = new XmlDocument();
			document.LoadXml((string)args.XmlString);
			baseStream = new MemoryStream();
			XmlTextWriter tw = new XmlTextWriter(baseStream, System.Text.Encoding.UTF8);
			document.WriteTo(tw);
			tw.Flush();
			baseStream.Seek(0, SeekOrigin.Begin);
			return baseStream;
		}

		public System.Type GetParameterType()
		{
			return typeof(XmlStringArguments);
		}
	}
}
