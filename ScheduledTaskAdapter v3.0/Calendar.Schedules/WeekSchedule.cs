using System;
using System.Threading;
//using System.Xml.Serialization;

namespace Calendar.Schedules
{
	/// <summary>
	///Weekly Schedule class supporting  Microsoft.Biztalk.Scheduler.ISchedule interface.
	/// </summary>
	[Serializable()]
	public class WeekSchedule: Schedule
	{
		///Fields
		private int interval = 0;
		private object days = 0;
		
		// Properties
		public int Interval 
		{
			get {return this.interval;}

			set
			{
				if ((value < 1) || (value > 52))
				{
					throw (new ArgumentOutOfRangeException("interval", "Week interval must be between 1 and 52"));
				}
				if (value != Interlocked.Exchange(ref this.interval, value))
				{
					this.FireChangedEvent();
				}
			}
		}
				
		public ScheduleDay ScheduledDays 
		{
			get {return (ScheduleDay)this.days;}

			set
			{
				if ((value == ScheduleDay.None))
				{
					throw (new ArgumentOutOfRangeException("days", "Must specify the scheduled days"));
				}
				if (value != (ScheduleDay)Interlocked.Exchange(ref this.days, value))
				{
					this.FireChangedEvent();
				}
			}
		}

		//Methods
		public WeekSchedule()
		{
            base.type = ScheduleType.Weekly;
		}
		
        protected override DateTime GetNextActivationTime(DateTime now)
        {
			if (this.ScheduledDays == ScheduleDay.None)
			{
				throw(new ApplicationException("Uninitialized weekly schedule")); 
			}			
			if (this.StartDate > now)
			{
				now =  new DateTime(this.StartDate.Year, this.StartDate.Month, this.StartDate.Day, 0, 0,0);
			}
			//Interval set
			DateTime lastSunday = GetLastSunday(now);
			DateTime firstSunday = GetLastSunday(this.StartDate);
			TimeSpan diff = lastSunday.Subtract(firstSunday);
			int daysAhead = diff.Days % (interval * 7);
			if (daysAhead == 0)
			{//possibly this week
				if ((GetScheduleDayFlag(now) & this.ScheduledDays) > 0)
				{//possibly today
					if (((StartTime.Hour == now.Hour) && (StartTime.Minute > now.Minute)) || (StartTime.Hour > now.Hour))
					{
                        return new DateTime(now.Year, now.Month, now.Day, StartTime.Hour, StartTime.Minute, 0);
					}
				}
				while (now.DayOfWeek != DayOfWeek.Saturday)
				{
					now = now.AddDays(1);
                    if ((GetScheduleDayFlag(now) & this.ScheduledDays) > 0)
                    {
                        return new DateTime(now.Year, now.Month, now.Day, StartTime.Hour, StartTime.Minute, 0);
                    }
				}	
			}
			//future week
			DateTime nextWeek = lastSunday.AddDays((interval*7)-daysAhead);
			while (nextWeek.DayOfWeek != DayOfWeek.Saturday)
			{
				if ((GetScheduleDayFlag(nextWeek) & this.ScheduledDays) > 0)
					break;		
				nextWeek = nextWeek.AddDays(1);
			}
            return new DateTime(nextWeek.Year, nextWeek.Month, nextWeek.Day, StartTime.Hour, StartTime.Minute, 0);
		}
	}
}
