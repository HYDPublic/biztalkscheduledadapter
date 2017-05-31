using System;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace Calendar.Schedules
{
	/// <summary>
	/// Daily Schedule class supporting  Microsoft.Biztalk.Scheduler.ISchedule interface.
	/// Allows scheduling by interval (e.g. every 3 days)  or by  weekday (e.g. on Mondays and Fridays)
	/// </summary>
	[Serializable()]
	public class DaySchedule: Schedule
	{
		//Fields        
		private int interval = 1;					//day interval
        private object days = ScheduleDay.None;		//days of week
		// Properties
		public int Interval 
		{
			get
			{
				return this.interval;
			}
			set
			{
				if ((this.ScheduledDays == ScheduleDay.None) && (value < 1))
				{
					throw (new ArgumentOutOfRangeException("days", "Must specify scheduled days or interval"));
				}
				if (value != Interlocked.Exchange(ref this.interval, value))
				{
					this.FireChangedEvent();
				}
			}
		}
		public ScheduleDay ScheduledDays 
		{
			get
			{
				return (ScheduleDay)this.days;
			}
			set
			{
				if ((value == ScheduleDay.None) && (this.Interval < 1))
				{
					throw (new ArgumentOutOfRangeException("days", "Must specify scheduled days or interval"));
				}
				if (value != (ScheduleDay)Interlocked.Exchange(ref this.days, value))
				{
					this.FireChangedEvent();
				}
			}
		}		
		//Methods
		public DaySchedule()
		{
            base.type = ScheduleType.Daily;
		}
		
        protected override DateTime GetNextActivationTime(DateTime now)
        {
			if ((this.Interval == 0) && (this.ScheduledDays == ScheduleDay.None))
			{
				throw(new ApplicationException("Uninitialized daily schedule")); 
			}

            DateTime start = new DateTime(this.StartDate.Year, this.StartDate.Month, this.StartDate.Day,
                                          this.StartTime.Hour, this.StartTime.Minute, 0);
			if (start > now)
			{				
				if (this.Interval > 0)
				{                   
					return start;
				}
			}            
			//Interval Days
			if (interval > 0)
			{
				DateTime compare =  new DateTime(now.Year, now.Month, now.Day,0, 0, 0);
				TimeSpan diff = compare.Subtract(this.StartDate);
				int daysAhead = diff.Days % interval;
				int daysToGo = 0;
				if (daysAhead == 0)
				{   //later today?
                    if (((this.StartTime.Hour == now.Hour) && (this.StartTime.Minute > now.Minute)) || (this.StartTime.Hour > now.Hour))
					{
                        return new DateTime(now.Year, now.Month, now.Day, this.StartTime.Hour, this.StartTime.Minute, 0);
					}
					daysToGo = interval;
				}
				else
				{
					daysToGo = interval - daysAhead;
				}
                DateTime returnDate = new DateTime(now.Year, now.Month, now.Day, this.StartTime.Hour, this.StartTime.Minute, 0);
                return returnDate.AddDays(daysToGo);
			}
			//Day of Week
			if ((GetScheduleDayFlag(now) & this.ScheduledDays) > 0)
			{ //today could be our lucky day
                if (((this.StartTime.Hour == now.Hour) && (this.StartTime.Minute > now.Minute)) || (this.StartTime.Hour > now.Hour))
				{
                    return new DateTime(now.Year, now.Month, now.Day, this.StartTime.Hour, this.StartTime.Minute, 0);
				}
			}
			//Find next day
			for (int i = 1; i < 8; i++)
			{
				now = now.AddDays(1);
				if ((GetScheduleDayFlag(now) & this.ScheduledDays) > 0)
					break;
			}
            return new DateTime(now.Year, now.Month, now.Day, this.StartTime.Hour, this.StartTime.Minute, 0);
		}
	}
}
