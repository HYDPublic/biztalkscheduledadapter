using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;

using Microsoft.BizTalk.Component.Interop;
using Calendar.Schedules;
using ScheduledTaskAdapter.TaskComponents;

namespace ScheduledTaskAdapter
{
    public class AdapterConfiguration
    {
        object uri = string.Empty;
        private string name = string.Empty;
        private bool suspendMessage = true;
        private Schedule schedule = null;
        private Task task = null;

        public string Uri
        {
            get { return (string)uri; }
            set { uri = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public bool SuspendMessage
        {
            get { return suspendMessage; }
            set { suspendMessage = value; }
        }
        public Schedule Schedule
        {
            get { return schedule; }
            set { schedule = value; }
        }
        public Task Task
        {
            get { return task; }
            set { task = value; }
        }

        public AdapterConfiguration()
        {
        }

        public void Load(XmlDocument document)
        {
            XmlReader reader = document.CreateNavigator().ReadSubtree();
            reader.Read();
            reader.ReadStartElement("config");
            reader.ReadStartElement("uri");
            this.uri = reader.ReadString();
            reader.ReadEndElement();
            reader.ReadStartElement("name");
            this.name = reader.ReadString();
            reader.ReadEndElement();
            reader.ReadStartElement("suspend");
            this.suspendMessage = reader.ReadContentAsBoolean();
            reader.ReadEndElement();
            this.schedule = Schedule.Deserialize(reader);
            this.task = Task.Deserialize(reader);
            reader.ReadEndElement();
        }

        public string Save()
        {
            StringBuilder sb = new StringBuilder();
            XmlTextWriter writer = new XmlTextWriter(new StringWriter(sb));
            try
            {
                writer.WriteStartElement("config");
                writer.WriteElementString("uri", this.Uri);
                writer.WriteElementString("name", this.Name);
                writer.WriteStartElement("suspend");
                writer.WriteValue(this.suspendMessage);
                writer.WriteEndElement();
                Schedule.Serialize(writer, this.schedule);
                Task.Serialize(writer, this.task);
                writer.WriteEndElement();
                writer.Flush();

                return sb.ToString();
            }
            finally
            {
                writer.Close();
            }
        }

        public void Validate()
        {
            switch (this.schedule.Type)
            {
                case ScheduleType.TimeSpan:
                    var tsSchedule = this.schedule as TimeSpanSchedule;
                    if (tsSchedule.Interval <= 0)
                        throw (new ArgumentOutOfRangeException("Interval", "Must specify scheduled interval greater than zero"));
                    break;
                case ScheduleType.Daily:
                    var dSchedule = this.schedule as DaySchedule;
                    if ((dSchedule.ScheduledDays == ScheduleDay.None) && (dSchedule.Interval < 1))
                        throw (new ArgumentOutOfRangeException("days", "Must specify scheduled days or interval"));
                    break;
                case ScheduleType.Weekly:
                    var wSchedule = this.schedule as WeekSchedule;
                    if ((wSchedule.Interval < 1) || (wSchedule.Interval > 52))
                        throw (new ArgumentOutOfRangeException("Interval", "Week interval must be between 1 and 52"));
                    if ((wSchedule.ScheduledDays == ScheduleDay.None))
                        throw (new ArgumentOutOfRangeException("days", "Must specify the scheduled days"));
                    break;
                case ScheduleType.Monthly:
                    var mSchedule = this.schedule as MonthSchedule;
                    if (mSchedule.Day < 0 || mSchedule.Day > 31)
                        throw (new ArgumentOutOfRangeException("value", "Day range: 0 - 31"));
                    if (mSchedule.ScheduledMonths == ScheduleMonth.None)
                    {
                        throw (new ArgumentOutOfRangeException("months", "Must specify a month"));
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
