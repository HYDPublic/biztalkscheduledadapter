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
        private int selectedDays = 0;
        private object days = ScheduleDay.None;     //days of week

        public int Interval
        {
            get
            {
                return this.timeSpan;
            }
            set
            {
                #region to be removed
                //if (value <= 0)
                //{
                //    throw (new ArgumentOutOfRangeException("Interval", "Must specify scheduled interval greater than zero"));
                //}
                #endregion
                if (value != Interlocked.Exchange(ref this.timeSpan, value))
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
                #region to be removed
                //if ((value == ScheduleDay.None) && (this.Interval < 1))
                //{
                //    throw (new ArgumentOutOfRangeException("days", "Must specify scheduled days or interval"));
                //}
                #endregion

                if (value != (ScheduleDay)Interlocked.Exchange(ref this.days, value))
                {
                    this.FireChangedEvent();
                }
            }
        }

        public int SelectedDays
        {
            get
            {
                return this.selectedDays;
            }
            set
            {
                #region to be removed
                //if (value <= 0)
                //{
                //    throw (new ArgumentOutOfRangeException("Interval", "Must specify scheduled interval greater than zero"));
                //}
                #endregion
                if (value != Interlocked.Exchange(ref this.selectedDays, value))
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
                throw (new ApplicationException("Uninitialized Timespan schedule"));
            }

            #region Add 2016 new schedule capabitities

            if ((this.SelectedDays == 1) && (this.ScheduledDays == ScheduleDay.None))
            {
                throw (new ApplicationException("Uninitialized Timespan schedule"));
            }

            #endregion

            DateTime start = new DateTime(this.StartDate.Year, this.StartDate.Month, this.StartDate.Day,
                                          this.StartTime.Hour, this.StartTime.Minute, 0);

            if (start >= now)
            {
                return start;
            }

            if (this.SelectedDays == 0)
            {
                return now.AddSeconds(this.timeSpan);
            }

            #region Add 2016 new schedule capabitities
            //Day of Week
            if ((GetScheduleDayFlag(now) & this.ScheduledDays) > 0)
            { //today could be our lucky day
                return now.AddSeconds(this.timeSpan);
            }
            //Find next day
            for (int i = 1; i < 8; i++)
            {
                now = now.AddDays(1);
                if ((GetScheduleDayFlag(now) & this.ScheduledDays) > 0)
                    break;
            }
            return new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);


            #endregion

            #region to be removed
            /*
			 *If timepSpan is less than 60 seconds then operate as a timer interval
			 */
            //if (this.timeSpan < 60)
            //{
            //    return now.AddSeconds(this.timeSpan);
            //}
            /*
			 * Compare current DateTime with the start DateTime 
			 */
            //TimeSpan fromStart = now - start;
            //double secondsFromLast = fromStart.TotalSeconds % this.timeSpan;
            //double secondsToGo = this.timeSpan - secondsFromLast;
            //DateTime result = now.AddSeconds(secondsToGo);
            ////Handle rounding errors - we always fire on 0 seconds
            //if (result.Second == 59)
            //   result = result.AddSeconds(1);
            //if (result.Second == 1)
            //    result = result.AddSeconds(-1);
            //return new DateTime(result.Year, result.Month, result.Day, result.Hour, result.Minute, 0);
            #endregion
        }
    }
}
