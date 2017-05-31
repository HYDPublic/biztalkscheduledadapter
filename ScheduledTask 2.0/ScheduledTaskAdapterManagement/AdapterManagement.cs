using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Xml;
using System.Runtime.InteropServices;
using Microsoft.BizTalk.Adapter.Framework;
using Microsoft.BizTalk.Component.Interop;
using ScheduledTask.TaskComponents;
using Calendar.Schedules;

namespace ScheduledTaskAdapterManagement
{
	/// <summary>
	/// Biztalk Adapter configuration interfaces
	/// </summary>
	public class AdapterManagement: IStaticAdapterConfig, IAdapterConfig, IAdapterConfigValidation, IAdapterInfo
	{
		#region IAdapterConfig

		public string GetConfigSchema(ConfigType configType)
		{
				switch (configType)
				{
					case ConfigType.ReceiveLocation:
						return GetResource("ScheduledTaskAdapterManagement.ReceiveLocation.xsd");
					
					default:
						return null;
				}
		}

		public Result GetSchema(string uri, string namespaceName, out string filelocation)
		{
			filelocation = null;
			return Result.Continue;
		}
		#endregion

		#region IStaticAdapterConfig

		public string [] GetServiceDescription(string [] wsdls) 
		{
			string []result = null;
			return result;
		}
		public string GetServiceOrganization(IPropertyBag endPointConfiguration, string NodeIdentifier) 
		{
			return null;
		}
		#endregion
				
		#region IAdapterInfo

		public string GetHelpString(ConfigType configType)
		{
			switch (configType)
			{
				case ConfigType.ReceiveLocation:
					return  "ms-help://TKH.ScheduledTaskAdapter/ScheduledTaskHelpFile/Operations/Configuration.htm";
				default:
					return null;
			}
		}
		#endregion

		#region IAdapterConfigValidation

		public string ValidateConfiguration(ConfigType type, string config)
		{
			UriBuilder uriBuilder;
			XmlNode uriNode;
			switch (type)
			{
				case ConfigType.ReceiveLocation:
				{
					XmlDocument configDOM = new XmlDocument();
					configDOM.LoadXml(config);
										
					string taskname = Extract(configDOM,"/Config/name",true);
					if (taskname == string.Empty)
					{
						throw(new ApplicationException("Must have a Task Name"));
					}
					//Assume ScheduleDialog will prevalidate schedule configuration
					string schedule = Extract(configDOM, "/Config/schedule", true);
					if (schedule == string.Empty)
					{
						throw(new ApplicationException("Must specify a schedule"));
					}
					XmlDocument scheduleXml = new XmlDocument();
					scheduleXml.LoadXml(schedule);
					string scheduletype = Extract(scheduleXml,"/schedule/@type", true);

					//uri  "schedule://scheduletype/taskname"
					uriNode = configDOM.SelectSingleNode("/Config/uri");
					if (uriNode == null)
					{
						uriNode = configDOM.CreateNode("element", "uri", null);
						configDOM.DocumentElement.InsertBefore(uriNode, configDOM.DocumentElement.FirstChild);
					}
					uriBuilder = new UriBuilder("schedule", scheduletype.ToString());
					uriBuilder.Path = taskname;
					uriNode.InnerText = uriBuilder.ToString();
					//Task Assembly parameters
					string taskproperties = Extract(configDOM,"/Config/taskproperties", true);
					if (taskproperties == string.Empty)
					{
						throw(new ApplicationException("Must specify the task properties"));
					}
					return configDOM.OuterXml;
				}
				default:
					break;
			}
			return string.Empty;
		}
		#endregion

		private  string Extract(XmlDocument document, string path, bool required)
		{
			XmlNode node = document.SelectSingleNode(path);
			if (!required && null == node)
				return String.Empty;
			if (null == node)
				throw new ApplicationException(string.Format("Config property missing: {0} ",path));
			return node.InnerText;
		}
		private string GetTypeFromAssemblyQualifiedName(string typequalifedname)
		{
			int offset = typequalifedname.IndexOf(",",0);
			return  typequalifedname.Substring(0, offset);
		}
		private string GetAssemblyFromAssemblyQualifiedName(string typequalifedname)
		{
			int offset = typequalifedname.IndexOf(",",0);
			return  typequalifedname.Substring(offset + 1).Trim();
		}
		private  bool InterfaceFilter(Type typeObj,Object criteriaObj)
		{
			if(typeObj.ToString() == criteriaObj.ToString())
				return true;
			return false;
		}
		private string GetResource(string resource) 
		{
			string value = null;
			if (null != resource) 
			{
				Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);
				StreamReader reader = null;
				using (reader = new StreamReader(stream)) 
				{
					value = reader.ReadToEnd();
				}
			}
			return value;
		}
	}
}
