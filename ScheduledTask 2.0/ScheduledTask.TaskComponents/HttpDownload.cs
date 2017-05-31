using System;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace ScheduledTask.TaskComponents
{
	[Serializable()]
	public class HttpDownloadArguments
	{
		private string url = string.Empty;
		private string user = string.Empty;
		private string password = string.Empty;

		[Description("The URL of the file to be downloaded"),
		CategoryAttribute("Document")]
		public string Url
		{
			get{return url;}
			set {url = value;}
		}

		[Description("Basic authentication: user name"),
		CategoryAttribute("Basic Authentication")]
		public string User
		{
			get{return user;}
			set {user = value;}
		}

		[Description("Basic authentication: password"),
			CategoryAttribute("Basic Authentication"),
			EditorAttribute("Microsoft.BizTalk.Adapter.Framework.ComponentModel.PasswordUITypeEditor, Microsoft.BizTalk.Adapter.Framework, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", typeof(System.Drawing.Design.UITypeEditor)),
			TypeConverterAttribute("Microsoft.BizTalk.Adapter.Framework.ComponentModel.PasswordTypeConverter, Microsoft.BizTalk.Adapter.Framework, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
		public string Password
		{
			get{return password;}
			set {password = value;}
		}
	}
	/// <summary>
	/// HttpDownload: implements the IScheduledTaskStreamProvider interface.
	/// downloads the data at the specified Url and passes to the  ScheduledTask Adapter as a stream
	/// </summary>
	public class HttpDownload: IScheduledTaskStreamProvider
	{
		public HttpDownload()
		{}

		public Stream GetStream(object parameter)
		{
			HttpDownloadArguments args = (HttpDownloadArguments)parameter;
			if (args.Url == string.Empty)
				throw (new ArgumentException("ScheduledTask HttpDownload","url"));
			
			Uri requestUri = new Uri(args.Url);
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
			//Credential info
			if (args.User != string.Empty)
			{
				request.Credentials = new NetworkCredential(args.User, args.Password);
			}
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			return response.GetResponseStream();
		}

		public System.Type GetParameterType()
		{
			return typeof(HttpDownloadArguments);
		}
	}
}
