using System;
using System.ComponentModel;
using System.IO;
using System.Xml;

namespace ScheduledTaskAdapter.TaskComponents
{
	[Serializable()]
	public class XmlStringArguments
	{
		private string xmlstring = string.Empty;

		[Description("The XML that will be returned by this scheduled task."),
        CategoryAttribute("Document Specifications"),
        EditorAttribute("ScheduledTaskAdapter.Admin.XmlTextUITypeEditor, ScheduledTaskAdapter.Admin, Version=5.0.0.3, Culture=neutral, PublicKeyToken=aa9f2dd0f13442dc", typeof(System.Drawing.Design.UITypeEditor)),
        TypeConverterAttribute("ScheduledTaskAdapter.Admin.XmlTextConverter, ScheduledTaskAdapter.Admin, Version=5.0.0.3, Culture=neutral, PublicKeyToken=aa9f2dd0f13442dc")]
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
