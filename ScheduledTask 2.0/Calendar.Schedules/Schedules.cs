using System;
using System.Collections;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace Calendar.Schedules
{
	public enum ScheduleType
	{
		None = 0,
		Daily = 1,
		Weekly = 2,
		Monthly = 3,
		TimeSpan = 4
	};
	[FlagsAttribute]
	public enum ScheduleDay
	{
		None = 0,
		Sunday = 1,
		Monday = 2,
		Tuesday = 4,
		Wednesday = 8,
		Thursday = 16,
		Friday = 32,
		Weekday = 62,
		Saturday = 64,
		Weekend = 65,
		All = 127
	};					
	[FlagsAttribute]						
	public enum ScheduleOrdinal
	{
		None = 0,
		First = 1,
		Second = 2,
		Third = 4,
		Fourth = 8,
		All = 15,
		Last = 16
	};
	[FlagsAttribute]
	public enum ScheduleMonth
	{
		None = 0,
		January = 1,
		February = 2,
		March = 4,
		April = 8,
		May = 16,
		June = 32,
		July = 64,
		August = 128,
		September = 256,
		October = 512,
		November = 1024,
		December = 2048,
		StartofQuarter= 585,
		EndofQuarter=2340,
		All = 4095
	};

	[Serializable()]
	public abstract class Schedule:  Microsoft.BizTalk.Scheduler.ISchedule
	{
		// Events
		public event EventHandler Changed;
		
		// Fields
		protected ScheduleType type;
		protected object starttime;
		protected object startdate;
		
		//Properties
		public ScheduleType Type
		{
			get
			{
				return this.type;
			}
		}
		public virtual DateTime StartTime
		{
			get
			{
				return  (DateTime)this.starttime;
			}
			set
			{
				DateTime newDate = new DateTime(1900,1,1,value.Hour, value.Minute, value.Second);
				if (newDate != (DateTime)Interlocked.Exchange(ref this.starttime, (object)newDate))
				{
					this.FireChangedEvent();
				}
			}
		}
		public virtual DateTime StartDate
		{
			get
			{
				return  (DateTime)this.startdate;
			}
			set
			{
				DateTime newDate = new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);
				if (newDate != (DateTime)Interlocked.Exchange(ref this.startdate, (object)newDate))
				{
					this.FireChangedEvent();
				}
			}
		}

		//Methods
		public Schedule()
		{
			this.type = ScheduleType.None;
			this.starttime = new DateTime(1900,1,1, 0, 0, 0);
			this.startdate = DateTime.Now;
		}

		protected void FireChangedEvent()
		{
			if (this.Changed != null)
			{
				this.Changed(this, EventArgs.Empty);
			}
		}
		public abstract DateTime GetNextActivationTime();
	
		//
		//Configuration Xml Handling
		//
		/// <summary>
		/// Used internally to read schedule configuration from Xml document
		/// </summary>
		public static string Extract(XmlDocument document, string path, bool required)
		{
			XmlNode node = document.SelectSingleNode(path);
			if (!required && null == node)
				return String.Empty;
			if (null == node)
				throw new ApplicationException(string.Format("Schedule property missing: {0} ",path));
			return node.InnerText;
		}
		/// <summary>
		/// Used internally to read schedule configuration from Xml document
		/// </summary>
		public static int ExtractInt (XmlDocument document, string path)
		{
			string s = Extract(document, path, true);
			return int.Parse(s);
		}
		/// <summary>
		/// Used internally to read schedule configuration from Xml document
		/// </summary>
		public static int IfExistsExtractInt (XmlDocument document, string path, int defaultValue)
		{
			string s = Extract(document, path, false);
			if (0 == s.Length)
				return defaultValue;
			return int.Parse(s);
		}
		/// <summary>
		/// Used internally to read schedule configuration from Xml document
		/// </summary>
		public static DateTime ExtractDate(XmlDocument document, string path, bool required)
		{
			string s = Extract(document, path, required);
			return DateTime.Parse(s);
		}
		/// <summary>
		/// Used internally to read schedule configuration from Xml document
		/// </summary>
		public static DateTime IfExistsExtractDate(XmlDocument document, string path, DateTime defaultValue)
		{
			string s = Extract(document, path, false);
			if (0 == s.Length)
				return defaultValue;
			return DateTime.Parse(s);
		}
		/// <summary>
		/// Used internally to read schedule configuration from Xml document
		/// </summary>
		public static DateTime ExtractTime(XmlDocument document, string path, bool required)
		{
			string s = Extract(document, path, required);
			return DateTime.Parse("1900-01-01T" + s.Substring(0, 5) + ":00");
		}
		/// <summary>
		/// Used internally to read schedule configuration from Xml document
		/// </summary>
		public static DateTime IfExistsExtractTime(XmlDocument document, string path, DateTime defaultValue)
		{
			string s = Extract(document, path, false);
			if (0 == s.Length)
				return defaultValue;
			return DateTime.Parse("1900-01-01T" + s.Substring(0, 5) + ":00");
		}
		/// <summary>
		/// Used internally to read schedule configuration from Xml document
		/// </summary>
		public static ScheduleType ExtractScheduleType(XmlDocument document)
		{
			string type = document.DocumentElement.GetAttribute("type");
			if (type == String.Empty)
				throw new ApplicationException(string.Format("Schedule Type missing: "));
			return (ScheduleType)Enum.Parse(typeof(ScheduleType), type);
		}
		/// <summary>
		/// Used internally to read schedule configuration from Xml document
		/// </summary>
		public static ScheduleType IfExistsExtractScheduleType(XmlDocument document)
		{
			string type = document.DocumentElement.GetAttribute("type");
			if (type == String.Empty)
				return ScheduleType.None;
			return (ScheduleType)Enum.Parse(typeof(ScheduleType), type);
		}
		/// <summary>
		/// Used internally to read schedule configuration from Xml document
		/// </summary>
		public static ScheduleOrdinal ExtractScheduleOrdinal(XmlDocument document, string path, bool required)
		{
			string s = Extract(document, path, required);
			return (ScheduleOrdinal)Enum.Parse(typeof(ScheduleOrdinal), s);
		}
		/// <summary>
		/// Used internally to read schedule configuration from Xml document
		/// </summary>
		public static ScheduleDay ExtractScheduleDay(XmlDocument document, string path, bool required)
		{
			string s = Extract(document, path, required);
			return (Calendar.Schedules.ScheduleDay)Enum.Parse(typeof(Calendar.Schedules.ScheduleDay), s);
		}
		/// <summary>
		/// Used internally to read schedule configuration from Xml document
		/// </summary>
		public static ScheduleMonth ExtractScheduleMonth(XmlDocument document, string path, bool required)
		{
			string s = Extract(document, path, required);
			return (Calendar.Schedules.ScheduleMonth)Enum.Parse(typeof(Calendar.Schedules.ScheduleMonth), s);
		}
		//
		// Utility Methods
		//
		/// <summary>
		/// Converts the DateTime.DayOfWeek into a ScheduleDay flag
		/// </summary>
		protected  ScheduleDay GetScheduleDayFlag(DateTime date)
		{
			return (ScheduleDay)(int)(Math.Pow(2,(int)date.DayOfWeek));
		}
		/// <summary>
		/// Converts the DateTime.Month into a ScheduleMonth flag
		/// </summary>
		protected ScheduleMonth GetScheduleMonthFlag(DateTime date)
		{
			return (ScheduleMonth)(Math.Pow(2,(int)date.Month-1));
		}
		/// <summary>
		/// Determines the previous Sunday, from the date parameter
		/// </summary>
		protected DateTime GetLastSunday(DateTime date)
		{
			DateTime lastSunday = date.Date;
			if (lastSunday.DayOfWeek != DayOfWeek.Sunday)
			{
				for (int i = 1; i < 8; i++)
				{
					lastSunday = lastSunday.AddDays(-1);
					if (lastSunday.DayOfWeek == DayOfWeek.Sunday)
						break;
				}
			}
			return lastSunday;
		}
		/// <summary>
		/// Determines the ordinal week day for the month
		/// e.g first Monday, last weekday or second Tuesday 
		/// </summary>
		protected int GetOrdinalWeekDay(ScheduleOrdinal ordinal, ScheduleDay weekday, int month, int year)
		{
			int result = -1;
			int index = 0;
			int[] days = new int[32];

			if ((ordinal == ScheduleOrdinal.None) || (weekday == ScheduleDay.None))
				return result;

			DateTime date = new DateTime(year, month, 1);
			int limit = DateTime.DaysInMonth(date.Year, date.Month);
			for (int i = 0; i < limit; i++)
			{
				if ((GetScheduleDayFlag(date) & weekday) > 0)
				{
					days[index++] = date.Day;
				}
				date = date.AddDays(1);	
			}
			switch (ordinal)
			{
				case ScheduleOrdinal.First:
					result = days[0];
					break;
				case ScheduleOrdinal.Second:
					result = days[1];
					break;
				case ScheduleOrdinal.Third:
					result = days[2];
					break;
				case ScheduleOrdinal.Fourth:
					result = days[3];
					break;
				case ScheduleOrdinal.Last:
					result = days[index-1];
					break;
				default:
					break;
			}
			return result;
		}
	}
}
