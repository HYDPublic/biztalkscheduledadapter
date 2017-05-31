using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Calendar.Schedules;
using ScheduledTaskAdapter.TaskComponents;

namespace ScheduledTaskAdapter.Admin
{
    public partial class ScheduledPropertyPage : UserControl
    {
        private AdapterConfiguration adapterConfiguration;
        private Type taskType;

        public ScheduledPropertyPage(AdapterConfiguration adapterConfiguration)
        {
            this.adapterConfiguration = adapterConfiguration;
            InitializeComponent();
            
            //Schedule tab defaults
            this.checkSuspendMessage.Checked = false;
            this.radioDayofMonth.Checked = true;
            this.radioDayInterval.Checked = true;
            DateTime now = DateTime.Now;
            this.dateStartDate.Value = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            this.dateStartTime.Value = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            this.updownTimeInterval.Value = 1;           
            this.comboScheduleType.Items.AddRange(new object[] { ScheduleType.TimeSpan, ScheduleType.Daily, ScheduleType.Weekly, ScheduleType.Monthly });
            this.comboScheduleType.SelectedItem = ScheduleType.TimeSpan;
            this.comboTimeUnits.Items.AddRange(new object[] { ScheduleTimeUnit.Seconds, ScheduleTimeUnit.Minutes, ScheduleTimeUnit.Hours });
            this.comboTimeUnits.SelectedItem = ScheduleTimeUnit.Hours;
            this.comboOrdinal.Items.AddRange(new object[] { ScheduleOrdinal.First, ScheduleOrdinal.Second, ScheduleOrdinal.Third, ScheduleOrdinal.Fourth, ScheduleOrdinal.Last });
            this.comboWeekday.Items.AddRange(new object[] { ScheduleDay.Sunday, ScheduleDay.Monday, ScheduleDay.Tuesday, ScheduleDay.Wednesday, ScheduleDay.Thursday, ScheduleDay.Friday, ScheduleDay.Saturday, ScheduleDay.Weekday, ScheduleDay.Day });

            this.Load += ScheduledPropertyPage_OnLoad;
        }
        
        public bool Apply()
        {
            if (!Validate()) return false;

            this.adapterConfiguration.Name = this.textScheduleName.Text;
            this.adapterConfiguration.Uri = string.Format("schedule://{0}/{1}", this.comboScheduleType.SelectedItem, this.textScheduleName.Text);
            this.adapterConfiguration.SuspendMessage = this.checkSuspendMessage.Checked;

            #region Schedule
            Schedule schedule = null;
            switch ((ScheduleType)this.comboScheduleType.SelectedItem)
            {
                case ScheduleType.TimeSpan:
                    schedule = new TimeSpanSchedule();
                    schedule.StartDate = this.dateStartDate.Value;
                    schedule.StartTime = this.dateStartTime.Value;
                    int interval = 0;
                    switch((ScheduleTimeUnit)this.comboTimeUnits.SelectedItem)
                    {
                        case ScheduleTimeUnit.Seconds:
                            interval = Convert.ToInt32(this.updownTimeInterval.Value);
                            break;
                        case ScheduleTimeUnit.Minutes:
                            interval = Convert.ToInt32(this.updownTimeInterval.Value * 60);
                            break;
                        case ScheduleTimeUnit.Hours:
                            interval = Convert.ToInt32(this.updownTimeInterval.Value * 3600);
                            break;
                        default:
                            interval = 3600;
                            break;
                    }
                    ((TimeSpanSchedule)schedule).Interval = interval;
                    break;
                case ScheduleType.Daily:
                    schedule = new DaySchedule();
                    schedule.StartDate = this.dateStartDate.Value;
                    schedule.StartTime = this.dateStartTime.Value;
                    if (radioDayInterval.Checked)
                    {
                        ((DaySchedule)schedule).Interval = Convert.ToInt32(this.updownDayInterval.Value);
                    }
                    else
                    {
                        ScheduleDay days = ScheduleDay.None;
                        if (checkDaySunday.Checked) days |= ScheduleDay.Sunday;
                        if (checkDayMonday.Checked) days |= ScheduleDay.Monday;
                        if (checkDayTuesday.Checked) days |= ScheduleDay.Tuesday;
                        if (checkDayWednesday.Checked) days |= ScheduleDay.Wednesday;
                        if (checkDayThursday.Checked) days |= ScheduleDay.Thursday;
                        if (checkDayFriday.Checked) days |= ScheduleDay.Friday;
                        if (checkDaySaturday.Checked) days |= ScheduleDay.Saturday;
                        ((DaySchedule)schedule).ScheduledDays = days;
                    }
                    break;
                case ScheduleType.Weekly:
                    schedule = new WeekSchedule();
                    schedule.StartDate = this.dateStartDate.Value;
                    schedule.StartTime = this.dateStartTime.Value;
                    ((WeekSchedule)schedule).Interval = Convert.ToInt32(this.updownWeekInterval.Value);
                    ScheduleDay weekDays = ScheduleDay.None;
                    if (checkWeekSunday.Checked) weekDays |= ScheduleDay.Sunday;
                    if (checkWeekMonday.Checked) weekDays |= ScheduleDay.Monday;
                    if (checkWeekTuesday.Checked) weekDays |= ScheduleDay.Tuesday;
                    if (checkWeekWednesday.Checked) weekDays |= ScheduleDay.Wednesday;
                    if (checkWeekThursday.Checked) weekDays |= ScheduleDay.Thursday;
                    if (checkWeekFriday.Checked) weekDays |= ScheduleDay.Friday;
                    if (checkWeekSaturday.Checked) weekDays |= ScheduleDay.Saturday;
                    ((WeekSchedule)schedule).ScheduledDays = weekDays;
                    break;
                case ScheduleType.Monthly:
                    schedule = new MonthSchedule();
                    schedule.StartDate = this.dateStartDate.Value;
                    schedule.StartTime = this.dateStartTime.Value;
                    if (radioDayofMonth.Checked)
                    {
                        ((MonthSchedule)schedule).Day = Convert.ToInt32(this.updownDayofMonth.Value);
                    }
                    else
                    {
                        ((MonthSchedule)schedule).Ordinal = (ScheduleOrdinal)this.comboOrdinal.SelectedItem;
                        ((MonthSchedule)schedule).WeekDay = (ScheduleDay)this.comboWeekday.SelectedItem;
                    }
                    ScheduleMonth months = ScheduleMonth.None;
                    if (checkJanuary.Checked) months |= ScheduleMonth.January;
                    if (checkFebruary.Checked) months |= ScheduleMonth.February;
                    if (checkMarch.Checked) months |= ScheduleMonth.March;
                    if (checkApril.Checked) months |= ScheduleMonth.April;
                    if (checkMay.Checked) months |= ScheduleMonth.May;
                    if (checkJune.Checked) months |= ScheduleMonth.June;
                    if (checkJuly.Checked) months |= ScheduleMonth.July;
                    if (checkAugust.Checked) months |= ScheduleMonth.August;
                    if (checkSeptember.Checked) months |= ScheduleMonth.September;
                    if (checkOctober.Checked) months |= ScheduleMonth.October;
                    if (checkNovember.Checked) months |= ScheduleMonth.November;
                    if (checkDecember.Checked) months |= ScheduleMonth.December;
                    ((MonthSchedule)schedule).ScheduledMonths = months;
                    break;
                default:
                    break;
            }
            
            adapterConfiguration.Schedule = schedule;
            #endregion

            #region Task
            Task task = new Task();
            task.TaskType = this.taskType;
            task.TaskParameters = propertyGridTask.SelectedObject;
            this.adapterConfiguration.Task = task;            
            #endregion

            return true;
        }
        public new bool Validate()
        {
            if (string.IsNullOrEmpty(this.textScheduleName.Text))
            {
                this.tabControl.SelectedTab = this.tabGeneral;
                this.textScheduleName.Focus();
                MessageBox.Show(this.Parent.Parent, "Must specify Schedule Name");
                return false;
            }
            if ((ScheduleType)this.comboScheduleType.SelectedItem == ScheduleType.TimeSpan)
            {
                if (this.updownTimeInterval.Value <= 0)
                {
                    this.tabControl.SelectedTab = this.tabSchedule;
                    this.updownTimeInterval.Focus();
                    MessageBox.Show(this.Parent.Parent, "Must specify a Timespan greater than zero");
                    return false;
                }
            }

            if (taskType == null)
            {
                this.tabControl.SelectedTab = this.tabTask;
                this.textTaskClass.Focus();
                MessageBox.Show(this.Parent.Parent, "Must specify a Task");
                return false;
            }
            return true;
        }


        public void ScheduledPropertyPage_OnLoad(object sender, EventArgs e)
        {
            this.textScheduleName.Text = adapterConfiguration.Name;
            this.checkSuspendMessage.Checked = adapterConfiguration.SuspendMessage;

            if (adapterConfiguration.Schedule != null)
            {
                this.dateStartDate.Value = adapterConfiguration.Schedule.StartDate;
                this.dateStartTime.Value = adapterConfiguration.Schedule.StartTime;
                this.comboScheduleType.SelectedItem = adapterConfiguration.Schedule.Type;
                switch (adapterConfiguration.Schedule.Type)
                {
                    case ScheduleType.Daily:
                        //set Daily schedule properties
                        DaySchedule daySchedule = adapterConfiguration.Schedule as DaySchedule;
                        this.updownDayInterval.Value = Convert.ToDecimal(daySchedule.Interval);
                        if (this.updownDayInterval.Value == 0)
                        {
                            ScheduleDay days = daySchedule.ScheduledDays;
                            if ((days & ScheduleDay.Sunday) > 0) checkDaySunday.Checked = true;
                            if ((days & ScheduleDay.Monday) > 0) checkDayMonday.Checked = true;
                            if ((days & ScheduleDay.Tuesday) > 0) checkDayTuesday.Checked = true;
                            if ((days & ScheduleDay.Wednesday) > 0) checkDayWednesday.Checked = true;
                            if ((days & ScheduleDay.Thursday) > 0) checkDayThursday.Checked = true;
                            if ((days & ScheduleDay.Friday) > 0) checkDayFriday.Checked = true;
                            if ((days & ScheduleDay.Saturday) > 0) checkDaySaturday.Checked = true;
                            radioDayInterval.Checked = false;
                        }
                        else
                        {
                            radioDayInterval.Checked = true;
                        }
                        break;
                    case ScheduleType.Weekly:
                        //set Weekly schedule properties
                        WeekSchedule weekSchedule = adapterConfiguration.Schedule as WeekSchedule;
                        this.updownWeekInterval.Value = weekSchedule.Interval;
                        ScheduleDay weekDays = weekSchedule.ScheduledDays;
                        if ((weekDays & ScheduleDay.Sunday) > 0) checkWeekSunday.Checked = true;
                        if ((weekDays & ScheduleDay.Monday) > 0) checkWeekMonday.Checked = true;
                        if ((weekDays & ScheduleDay.Tuesday) > 0) checkWeekTuesday.Checked = true;
                        if ((weekDays & ScheduleDay.Wednesday) > 0) checkWeekWednesday.Checked = true;
                        if ((weekDays & ScheduleDay.Thursday) > 0) checkWeekThursday.Checked = true;
                        if ((weekDays & ScheduleDay.Friday) > 0) checkWeekFriday.Checked = true;
                        if ((weekDays & ScheduleDay.Saturday) > 0) checkWeekSaturday.Checked = true;
                        break;                    
                    case ScheduleType.Monthly:
                        //set Monthly schedule properties
                        MonthSchedule monthSchedule = adapterConfiguration.Schedule as MonthSchedule;
                        this.updownDayofMonth.Value = monthSchedule.Day;
                        if (this.updownDayofMonth.Value == 0)
                        {
                            this.comboOrdinal.SelectedItem = monthSchedule.Ordinal;                                                                                   
                            this.comboWeekday.SelectedItem = monthSchedule.WeekDay;
                            radioDayofMonth.Checked = false;
                        }
                        else
                        {
                            radioDayofMonth.Checked = true;
                        }
                        ScheduleMonth months = monthSchedule.ScheduledMonths;
                        if ((months & ScheduleMonth.January) > 0) checkJanuary.Checked = true;
                        if ((months & ScheduleMonth.February) > 0) checkFebruary.Checked = true;
                        if ((months & ScheduleMonth.March) > 0) checkMarch.Checked = true;
                        if ((months & ScheduleMonth.April) > 0) checkApril.Checked = true;
                        if ((months & ScheduleMonth.May) > 0) checkMay.Checked = true;
                        if ((months & ScheduleMonth.June) > 0) checkJune.Checked = true;
                        if ((months & ScheduleMonth.July) > 0) checkJuly.Checked = true;
                        if ((months & ScheduleMonth.August) > 0) checkAugust.Checked = true;
                        if ((months & ScheduleMonth.September) > 0) checkSeptember.Checked = true;
                        if ((months & ScheduleMonth.October) > 0) checkOctober.Checked = true;
                        if ((months & ScheduleMonth.November) > 0) checkNovember.Checked = true;
                        if ((months & ScheduleMonth.December) > 0) checkDecember.Checked = true;
                        break;
                    case ScheduleType.TimeSpan:
                        //set Timespan schedule properties
                        TimeSpanSchedule timeSchedule = adapterConfiguration.Schedule as TimeSpanSchedule;
                        int timeinseconds = timeSchedule.Interval;
                        if (timeinseconds % 3600 == 0)
                        {
                            this.updownTimeInterval.Value = (timeinseconds / 3600);
                            this.comboTimeUnits.SelectedItem = ScheduleTimeUnit.Hours;
                        }
                        else if (timeinseconds % 60 == 0)
                        {
                            this.updownTimeInterval.Value = (timeinseconds / 60);
                            this.comboTimeUnits.SelectedItem =  ScheduleTimeUnit.Minutes;
                        }
                        else
                        {
                            this.updownTimeInterval.Value = timeinseconds;
                            this.comboTimeUnits.SelectedItem = ScheduleTimeUnit.Seconds;
                        }
                        break;

                    default:                        
                        break;
                }
            }
            if (adapterConfiguration.Task != null)
            {
                this.taskType = adapterConfiguration.Task.TaskType;
                this.textTaskClass.Text = adapterConfiguration.Task.TaskType.AssemblyQualifiedName;
                this.propertyGridTask.SelectedObject = adapterConfiguration.Task.TaskParameters;
            }

        }
       
        #region Schedule Tab
        private void comboScheduleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((ScheduleType)this.comboScheduleType.SelectedItem)
            {                
                case ScheduleType.Daily:
                    this.panelDaily.Visible = true;
                    this.panelMonthly.Visible = false;
                    this.panelTimespan.Visible = false;
                    this.panelWeekly.Visible = false;
                    this.groupSchedule.Text = "Daily Properties";
                    break;
                case ScheduleType.Weekly:
                    this.panelDaily.Visible = false;
                    this.panelMonthly.Visible = false;
                    this.panelTimespan.Visible = false;
                    this.panelWeekly.Visible = true;
                    this.groupSchedule.Text = "Weekly Properties";
                    break;
                case ScheduleType.Monthly:
                    this.panelDaily.Visible = false;
                    this.panelMonthly.Visible = true;
                    this.panelTimespan.Visible = false;
                    this.panelWeekly.Visible = false;
                    this.groupSchedule.Text = "Monthly Properties";
                    break;
                default:
                    this.panelDaily.Visible = false;
                    this.panelMonthly.Visible = false;
                    this.panelTimespan.Visible = true;
                    this.panelWeekly.Visible = false;
                    this.groupSchedule.Text = "Timespan Properties";
                    break;
            }
        }
        private void radioSelectDays_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSelectDays.Checked)
            {
                this.updownDayInterval.Enabled = false;
                this.updownDayInterval.Value = 0;
                this.checkDayMonday.Enabled = true;
                this.checkDayTuesday.Enabled = true;
                this.checkDayWednesday.Enabled = true;
                this.checkDayThursday.Enabled = true;
                this.checkDayFriday.Enabled = true;
                this.checkDaySaturday.Enabled = true;
                this.checkDaySunday.Enabled = true;
            }
            else
            {
                this.updownDayInterval.Enabled = true;
                this.updownDayInterval.Value = 1;
                this.checkDayMonday.Enabled = false;
                this.checkDayTuesday.Enabled = false;
                this.checkDayWednesday.Enabled = false;
                this.checkDayThursday.Enabled = false;
                this.checkDayFriday.Enabled = false;
                this.checkDaySaturday.Enabled = false;
                this.checkDaySunday.Enabled = false;
            }


        }
        #endregion

        #region Task Tab
        private void buttonFindTask_Click(object sender, EventArgs e)
        {
            AssemblyQualifiedTypeNameDialog typeDialog = new AssemblyQualifiedTypeNameDialog();
            typeDialog.SupportedInterfaces.Add(typeof(IScheduledTaskStreamProvider));
            typeDialog.SupportedInterfaces.Add(typeof(IScheduledTaskStreamProvider2));
            typeDialog.Type = taskType;
            if (typeDialog.ShowDialog() == DialogResult.OK)
            {
                taskType = typeDialog.Type;
                textTaskClass.Text = taskType.AssemblyQualifiedName;
            }
        }

        private void textTaskAssembly_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textTaskClass.Text))
                {
                    //handle cut and paste of assembly qualified name
                    Type type = Type.GetType(textTaskClass.Text);
                    this.taskType = type;
                    if (adapterConfiguration.Task == null || adapterConfiguration.Task.TaskType != type)
                    {
                        object provider = type.Assembly.CreateInstance(type.FullName);
                        object[] args = new object[] { };
                        Type taskArgumentsType = (Type)provider.GetType().InvokeMember("GetParameterType", BindingFlags.InvokeMethod, null, provider, args);
                        if (taskArgumentsType != null)
                        {
                            this.propertyGridTask.SelectedObject = taskArgumentsType.Assembly.CreateInstance(taskArgumentsType.FullName); ;
                        }
                    }
                    else
                    {
                        this.propertyGridTask.SelectedObject = adapterConfiguration.Task.TaskParameters;
                    }
                }
                return;
            }
            catch
            {
                MessageBox.Show("Invalid assembly qualified name");
            }
        } 
        #endregion       
    }
}
