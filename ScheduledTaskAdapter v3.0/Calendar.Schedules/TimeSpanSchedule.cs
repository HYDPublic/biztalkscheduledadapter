using System;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace Calendar.Schedules
{
	/// <summary>
	/// TimeSpan Schedule class supporting  Microsoft.Biztalk.Scheduler.ISchedule interface.
	/// Allows scheduling by a given TimeSpan in seconds. (e.g. every 60 seconds)
	/// </summary>
	[Serializable()]
	public class TimeSpanSchedule : Schedule
	{
		private int timeSpan = 0;
		
		public int Interval 
		{
			get
			{
				return this.timeSpan;
			}
			set
			{
				if (value <= 0)
				{
					throw (new ArgumentOutOfRangeException("Interval", "Must specify scheduled interval greater than zero"));
				}
				if (value != Interlocked.Exchange(ref this.timeSpan, value))
				{
					this.FireChangedEvent();
				}
			}
		}
		/// <summary>
		/// Create an empty TimeSpanSchedule.
		/// </summary>
		public TimeSpanSchedule()
		{
            base.type = ScheduleType.TimeSpan;
		}		

        protected override DateTime GetNextActivationTime(DateTime now)
        {
			if (this.timeSpan == 0)
			{
				throw(new ApplicationException("Uninitialized Timespan schedule")); 
			}
            DateTime start = new DateTime(this.StartDate.Year, this.StartDate.Month, this.StartDate.Day,
                                          this.StartTime.Hour, this.StartTime.Minute, 0);
            
			if (start >= now)
			{                
                return start;
            }
			/*
			 *If timepSpan is less than 60 seconds then operate as a timer interval
			 */
			if (this.timeSpan < 60)
			{
                return now.AddSeconds(this.timeSpan);
			}
			/*
			 * Compare current DateTime with the start DateTime 
			 */
			TimeSpan fromStart = now - start;
			double secondsFromLast = fromStart.TotalSeconds % this.timeSpan;
			double secondsToGo = this.timeSpan - secondsFromLast;
            DateTime result = now.AddSeconds(secondsToGo);
            //Handle rounding errors - we always fire on 0 seconds
            if (result.Second == 59)
               result = result.AddSeconds(1);
            if (result.Second == 1)
                result = result.AddSeconds(-1);
            return new DateTime(result.Year, result.Month, result.Day, result.Hour, result.Minute, 0);
		}
	}
}
