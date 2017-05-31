using System;
using System.Reflection;
using System.Xml;
using Calendar.Schedules;
//using Microsoft.BizTalk.Scheduler;
using  Microsoft.BizTalk.Adapter.Common;

namespace ScheduledTaskAdapter
{
	/// <summary>
	/// Summary description for ScheduledProperties.
	/// </summary>
	public class ScheduledProperties: ConfigProperties
	{
		// Fields
		private string name;
		private Schedule schedule;
		private string streamProviderAssemblyName;
		private string streamProviderTypeName;
		private string streamProviderArguments;
		// Properties			
		public string Name				{ get { return this.name; } }
		public Schedule Schedule	{ get { return this.schedule; } }
		public string StreamProviderAssemblyName		{ get { return this.streamProviderAssemblyName; } } 
		public string StreamProviderTypeName			{ get { return this.streamProviderTypeName; } } 
		public string StreamProviderArguments			{ get { return this.streamProviderArguments; } } 
		// Methods
		public ScheduledProperties() : base()
		{
			// defaults
			this.name = String.Empty;
			this.schedule = null;
		}
		public static void ReceiveHandlerConfiguration (XmlDocument configDOM)
		{
			// No Receive Handler properties
		}

		public void LocationConfiguration (XmlDocument configDOM)
		{
			// set endpoint properties
			this.name = Extract(configDOM, "/Config/name", null);
			
			// create the schedule
			XmlDocument scheduleXml = new XmlDocument();
			scheduleXml.LoadXml(Schedule.Extract(configDOM, "/Config/schedule", true));
			ScheduleType type = Schedule.ExtractScheduleType(scheduleXml);
			switch (type)
			{
				case ScheduleType.Daily:
					this.schedule = new DaySchedule(scheduleXml.OuterXml);
					break;
				case ScheduleType.Weekly:
					this.schedule = new WeekSchedule(scheduleXml.OuterXml);
					break;
				case ScheduleType.Monthly:
					this.schedule = new MonthSchedule(scheduleXml.OuterXml);
					break;
				case ScheduleType.TimeSpan:
					this.schedule = new TimeSpanSchedule(scheduleXml.OuterXml);
					break;
				default:
					break;
			}
			
			XmlDocument taskXml = new XmlDocument();
			taskXml.LoadXml(Schedule.Extract(configDOM, "/Config/taskproperties", true));
			string qualifiedName = Extract(taskXml,"/task/qualifiedname", null);
			this.streamProviderTypeName = GetTypeFromAssemblyQualifiedName(qualifiedName);
			this.streamProviderAssemblyName = GetAssemblyFromAssemblyQualifiedName(qualifiedName);
			this.streamProviderArguments = Extract(taskXml,"/task/arguments", null);
		}
		public static string GetTypeFromAssemblyQualifiedName(string typequalifedname)
		{
			int offset = typequalifedname.IndexOf(",",0);
			return  typequalifedname.Substring(0, offset);
		}
		public static string GetAssemblyFromAssemblyQualifiedName(string typequalifedname)
		{
			int offset = typequalifedname.IndexOf(",",0);
			return  typequalifedname.Substring(offset + 1).Trim();
		}
	}
}

