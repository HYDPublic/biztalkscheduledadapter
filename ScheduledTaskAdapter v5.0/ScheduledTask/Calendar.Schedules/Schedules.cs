using System;
using System.Collections;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace Calendar.Schedules
{
    public enum ScheduleTimeUnit
    {
        Seconds = 0,
        Minutes = 1,
        Hours = 2
    }
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
		Day = 127
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
        protected DateTime? lastActivated = null;
        protected DateTime? nextActivated = null;
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
        public virtual DateTime? LastActivated
        {
            get { return lastActivated; }
            set { lastActivated = value; }
        }        
        public virtual DateTime? NextActivated
        {
            get { return nextActivated; }
            set { nextActivated = value; }
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
                    lastActivated = new DateTime(1900, 1, 1, 0, 0, 0);
                    nextActivated = null;
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
                    lastActivated = new DateTime(1900, 1, 1, 0, 0, 0);
                    nextActivated = null;
					this.FireChangedEvent();
				}
			}
		}

		//Methods
		public Schedule()
		{
			this.type = ScheduleType.None;
			this.starttime = new DateTime(1900, 1, 1, 0, 0, 0);
			this.startdate = DateTime.Now;
		}

		protected void FireChangedEvent()
		{
			if (this.Changed != null)
			{
				this.Changed(this, EventArgs.Empty);
			}
		}

        /// <summary>
        /// Get the next DateTime when this schedule should run.
        /// </summary>
        /// 
        /// <returns></returns>
        public DateTime GetNextActivationTime()
		{
            DateTime now = DateTime.Now;                       
            if (!lastActivated.HasValue)
            {
                nextActivated = GetNextActivationTime(now);
                return nextActivated.Value;
            }            
            if (now <= lastActivated)
            {
                nextActivated = GetNextActivationTime(lastActivated.Value);
                return nextActivated.Value;
            }
            nextActivated = GetNextActivationTime(now);
            return nextActivated.Value;            
        }
        /// <summary>
        /// Schedule implementation specific GetNextActivationTime.
        /// </summary>
        /// <returns></returns>
        protected abstract DateTime GetNextActivationTime(DateTime now);


        public static void Serialize(XmlWriter writer, Schedule schedule)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Schedule), new Type[] { typeof(DaySchedule), typeof(WeekSchedule), typeof(MonthSchedule), typeof(TimeSpanSchedule) });           
            serializer.Serialize(writer, schedule);            
        }

        public static Schedule Deserialize(XmlReader reader)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Schedule), new Type[] { typeof(DaySchedule), typeof(WeekSchedule), typeof(MonthSchedule), typeof(TimeSpanSchedule) });
            return (Schedule)serializer.Deserialize(reader); 
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
