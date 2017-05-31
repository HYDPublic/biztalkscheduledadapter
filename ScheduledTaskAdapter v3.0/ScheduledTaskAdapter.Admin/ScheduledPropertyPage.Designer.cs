namespace ScheduledTaskAdapter.Admin
{
    partial class ScheduledPropertyPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.groupErrorHandling = new System.Windows.Forms.GroupBox();
            this.checkSuspendMessage = new System.Windows.Forms.CheckBox();
            this.textScheduleName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.tabSchedule = new System.Windows.Forms.TabPage();
            this.groupSchedule = new System.Windows.Forms.GroupBox();
            this.panelMonthly = new System.Windows.Forms.Panel();
            this.checkApril = new System.Windows.Forms.CheckBox();
            this.checkAugust = new System.Windows.Forms.CheckBox();
            this.checkDecember = new System.Windows.Forms.CheckBox();
            this.checkNovember = new System.Windows.Forms.CheckBox();
            this.checkJuly = new System.Windows.Forms.CheckBox();
            this.checkMarch = new System.Windows.Forms.CheckBox();
            this.checkFebruary = new System.Windows.Forms.CheckBox();
            this.checkJune = new System.Windows.Forms.CheckBox();
            this.checkOctober = new System.Windows.Forms.CheckBox();
            this.checkSeptember = new System.Windows.Forms.CheckBox();
            this.checkMay = new System.Windows.Forms.CheckBox();
            this.checkJanuary = new System.Windows.Forms.CheckBox();
            this.comboOrdinal = new System.Windows.Forms.ComboBox();
            this.radioOrdinal = new System.Windows.Forms.RadioButton();
            this.comboWeekday = new System.Windows.Forms.ComboBox();
            this.radioDayofMonth = new System.Windows.Forms.RadioButton();
            this.updownDayofMonth = new System.Windows.Forms.NumericUpDown();
            this.panelWeekly = new System.Windows.Forms.Panel();
            this.labelSelectDays = new System.Windows.Forms.Label();
            this.labelEvery = new System.Windows.Forms.Label();
            this.labelweeks = new System.Windows.Forms.Label();
            this.updownWeekInterval = new System.Windows.Forms.NumericUpDown();
            this.checkWeekSunday = new System.Windows.Forms.CheckBox();
            this.checkWeekMonday = new System.Windows.Forms.CheckBox();
            this.checkWeekTuesday = new System.Windows.Forms.CheckBox();
            this.checkWeekWednesday = new System.Windows.Forms.CheckBox();
            this.checkWeekThursday = new System.Windows.Forms.CheckBox();
            this.checkWeekFriday = new System.Windows.Forms.CheckBox();
            this.checkWeekSaturday = new System.Windows.Forms.CheckBox();
            this.panelDaily = new System.Windows.Forms.Panel();
            this.updownDayInterval = new System.Windows.Forms.NumericUpDown();
            this.radioDayInterval = new System.Windows.Forms.RadioButton();
            this.labelDays = new System.Windows.Forms.Label();
            this.radioSelectDays = new System.Windows.Forms.RadioButton();
            this.checkDaySunday = new System.Windows.Forms.CheckBox();
            this.checkDayMonday = new System.Windows.Forms.CheckBox();
            this.checkDayTuesday = new System.Windows.Forms.CheckBox();
            this.checkDayWednesday = new System.Windows.Forms.CheckBox();
            this.checkDayThursday = new System.Windows.Forms.CheckBox();
            this.checkDayFriday = new System.Windows.Forms.CheckBox();
            this.checkDaySaturday = new System.Windows.Forms.CheckBox();
            this.panelTimespan = new System.Windows.Forms.Panel();
            this.comboTimeUnits = new System.Windows.Forms.ComboBox();
            this.labelTimeUnits = new System.Windows.Forms.Label();
            this.labelTimeInterval = new System.Windows.Forms.Label();
            this.updownTimeInterval = new System.Windows.Forms.NumericUpDown();
            this.labelType = new System.Windows.Forms.Label();
            this.comboScheduleType = new System.Windows.Forms.ComboBox();
            this.dateStartTime = new System.Windows.Forms.DateTimePicker();
            this.labelTime = new System.Windows.Forms.Label();
            this.dateStartDate = new System.Windows.Forms.DateTimePicker();
            this.labelDate = new System.Windows.Forms.Label();
            this.tabTask = new System.Windows.Forms.TabPage();
            this.groupTask = new System.Windows.Forms.GroupBox();
            this.propertyGridTask = new System.Windows.Forms.PropertyGrid();
            this.buttonFindTask = new System.Windows.Forms.Button();
            this.textTaskClass = new System.Windows.Forms.TextBox();
            this.labelClass = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.groupErrorHandling.SuspendLayout();
            this.tabSchedule.SuspendLayout();
            this.groupSchedule.SuspendLayout();
            this.panelMonthly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updownDayofMonth)).BeginInit();
            this.panelWeekly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updownWeekInterval)).BeginInit();
            this.panelDaily.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updownDayInterval)).BeginInit();
            this.panelTimespan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updownTimeInterval)).BeginInit();
            this.tabTask.SuspendLayout();
            this.groupTask.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabGeneral);
            this.tabControl.Controls.Add(this.tabSchedule);
            this.tabControl.Controls.Add(this.tabTask);
            this.tabControl.HotTrack = true;
            this.tabControl.Location = new System.Drawing.Point(4, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(349, 435);
            this.tabControl.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.groupErrorHandling);
            this.tabGeneral.Controls.Add(this.textScheduleName);
            this.tabGeneral.Controls.Add(this.labelName);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(341, 409);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // groupErrorHandling
            // 
            this.groupErrorHandling.Controls.Add(this.checkSuspendMessage);
            this.groupErrorHandling.Location = new System.Drawing.Point(6, 74);
            this.groupErrorHandling.Name = "groupErrorHandling";
            this.groupErrorHandling.Size = new System.Drawing.Size(328, 57);
            this.groupErrorHandling.TabIndex = 3;
            this.groupErrorHandling.TabStop = false;
            this.groupErrorHandling.Text = "Error Handling";
            this.groupErrorHandling.Visible = false;
            // 
            // checkSuspendMessage
            // 
            this.checkSuspendMessage.AutoSize = true;
            this.checkSuspendMessage.Location = new System.Drawing.Point(6, 20);
            this.checkSuspendMessage.Name = "checkSuspendMessage";
            this.checkSuspendMessage.Size = new System.Drawing.Size(159, 17);
            this.checkSuspendMessage.TabIndex = 2;
            this.checkSuspendMessage.Text = "Suspend message on failure";
            this.checkSuspendMessage.UseVisualStyleBackColor = true;
            this.checkSuspendMessage.Visible = false;
            // 
            // textScheduleName
            // 
            this.textScheduleName.Location = new System.Drawing.Point(48, 27);
            this.textScheduleName.Name = "textScheduleName";
            this.textScheduleName.Size = new System.Drawing.Size(279, 20);
            this.textScheduleName.TabIndex = 1;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(6, 27);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(35, 13);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Name";
            // 
            // tabSchedule
            // 
            this.tabSchedule.Controls.Add(this.groupSchedule);
            this.tabSchedule.Controls.Add(this.labelType);
            this.tabSchedule.Controls.Add(this.comboScheduleType);
            this.tabSchedule.Controls.Add(this.dateStartTime);
            this.tabSchedule.Controls.Add(this.labelTime);
            this.tabSchedule.Controls.Add(this.dateStartDate);
            this.tabSchedule.Controls.Add(this.labelDate);
            this.tabSchedule.Location = new System.Drawing.Point(4, 22);
            this.tabSchedule.Name = "tabSchedule";
            this.tabSchedule.Padding = new System.Windows.Forms.Padding(3);
            this.tabSchedule.Size = new System.Drawing.Size(341, 409);
            this.tabSchedule.TabIndex = 1;
            this.tabSchedule.Text = "Schedule";
            this.tabSchedule.UseVisualStyleBackColor = true;
            // 
            // groupSchedule
            // 
            this.groupSchedule.Controls.Add(this.panelMonthly);
            this.groupSchedule.Controls.Add(this.panelWeekly);
            this.groupSchedule.Controls.Add(this.panelDaily);
            this.groupSchedule.Controls.Add(this.panelTimespan);
            this.groupSchedule.Location = new System.Drawing.Point(6, 160);
            this.groupSchedule.Name = "groupSchedule";
            this.groupSchedule.Size = new System.Drawing.Size(328, 240);
            this.groupSchedule.TabIndex = 8;
            this.groupSchedule.TabStop = false;
            this.groupSchedule.Text = "Schedule Properties";
            // 
            // panelMonthly
            // 
            this.panelMonthly.Controls.Add(this.checkApril);
            this.panelMonthly.Controls.Add(this.checkAugust);
            this.panelMonthly.Controls.Add(this.checkDecember);
            this.panelMonthly.Controls.Add(this.checkNovember);
            this.panelMonthly.Controls.Add(this.checkJuly);
            this.panelMonthly.Controls.Add(this.checkMarch);
            this.panelMonthly.Controls.Add(this.checkFebruary);
            this.panelMonthly.Controls.Add(this.checkJune);
            this.panelMonthly.Controls.Add(this.checkOctober);
            this.panelMonthly.Controls.Add(this.checkSeptember);
            this.panelMonthly.Controls.Add(this.checkMay);
            this.panelMonthly.Controls.Add(this.checkJanuary);
            this.panelMonthly.Controls.Add(this.comboOrdinal);
            this.panelMonthly.Controls.Add(this.radioOrdinal);
            this.panelMonthly.Controls.Add(this.comboWeekday);
            this.panelMonthly.Controls.Add(this.radioDayofMonth);
            this.panelMonthly.Controls.Add(this.updownDayofMonth);
            this.panelMonthly.Location = new System.Drawing.Point(6, 19);
            this.panelMonthly.Name = "panelMonthly";
            this.panelMonthly.Size = new System.Drawing.Size(318, 205);
            this.panelMonthly.TabIndex = 10;
            this.panelMonthly.Visible = false;
            // 
            // checkApril
            // 
            this.checkApril.Location = new System.Drawing.Point(7, 159);
            this.checkApril.Name = "checkApril";
            this.checkApril.Size = new System.Drawing.Size(72, 16);
            this.checkApril.TabIndex = 25;
            this.checkApril.Text = "April";
            // 
            // checkAugust
            // 
            this.checkAugust.Location = new System.Drawing.Point(111, 159);
            this.checkAugust.Name = "checkAugust";
            this.checkAugust.Size = new System.Drawing.Size(72, 16);
            this.checkAugust.TabIndex = 29;
            this.checkAugust.Text = "August";
            // 
            // checkDecember
            // 
            this.checkDecember.Location = new System.Drawing.Point(215, 159);
            this.checkDecember.Name = "checkDecember";
            this.checkDecember.Size = new System.Drawing.Size(80, 16);
            this.checkDecember.TabIndex = 33;
            this.checkDecember.Text = "December";
            // 
            // checkNovember
            // 
            this.checkNovember.Location = new System.Drawing.Point(215, 135);
            this.checkNovember.Name = "checkNovember";
            this.checkNovember.Size = new System.Drawing.Size(80, 16);
            this.checkNovember.TabIndex = 32;
            this.checkNovember.Text = "November";
            // 
            // checkJuly
            // 
            this.checkJuly.Location = new System.Drawing.Point(111, 135);
            this.checkJuly.Name = "checkJuly";
            this.checkJuly.Size = new System.Drawing.Size(72, 16);
            this.checkJuly.TabIndex = 28;
            this.checkJuly.Text = "July";
            // 
            // checkMarch
            // 
            this.checkMarch.Location = new System.Drawing.Point(7, 135);
            this.checkMarch.Name = "checkMarch";
            this.checkMarch.Size = new System.Drawing.Size(72, 16);
            this.checkMarch.TabIndex = 24;
            this.checkMarch.Text = "March";
            // 
            // checkFebruary
            // 
            this.checkFebruary.Location = new System.Drawing.Point(7, 111);
            this.checkFebruary.Name = "checkFebruary";
            this.checkFebruary.Size = new System.Drawing.Size(72, 16);
            this.checkFebruary.TabIndex = 23;
            this.checkFebruary.Text = "February";
            // 
            // checkJune
            // 
            this.checkJune.Location = new System.Drawing.Point(111, 111);
            this.checkJune.Name = "checkJune";
            this.checkJune.Size = new System.Drawing.Size(72, 16);
            this.checkJune.TabIndex = 27;
            this.checkJune.Text = "June";
            // 
            // checkOctober
            // 
            this.checkOctober.Location = new System.Drawing.Point(215, 111);
            this.checkOctober.Name = "checkOctober";
            this.checkOctober.Size = new System.Drawing.Size(80, 16);
            this.checkOctober.TabIndex = 31;
            this.checkOctober.Text = "October";
            // 
            // checkSeptember
            // 
            this.checkSeptember.Location = new System.Drawing.Point(215, 87);
            this.checkSeptember.Name = "checkSeptember";
            this.checkSeptember.Size = new System.Drawing.Size(80, 16);
            this.checkSeptember.TabIndex = 30;
            this.checkSeptember.Text = "September";
            // 
            // checkMay
            // 
            this.checkMay.Location = new System.Drawing.Point(111, 87);
            this.checkMay.Name = "checkMay";
            this.checkMay.Size = new System.Drawing.Size(72, 16);
            this.checkMay.TabIndex = 26;
            this.checkMay.Text = "May";
            // 
            // checkJanuary
            // 
            this.checkJanuary.Location = new System.Drawing.Point(7, 87);
            this.checkJanuary.Name = "checkJanuary";
            this.checkJanuary.Size = new System.Drawing.Size(64, 16);
            this.checkJanuary.TabIndex = 22;
            this.checkJanuary.Text = "January";
            // 
            // comboOrdinal
            // 
            this.comboOrdinal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboOrdinal.Location = new System.Drawing.Point(71, 39);
            this.comboOrdinal.MaxDropDownItems = 5;
            this.comboOrdinal.Name = "comboOrdinal";
            this.comboOrdinal.Size = new System.Drawing.Size(96, 21);
            this.comboOrdinal.TabIndex = 20;
            // 
            // radioOrdinal
            // 
            this.radioOrdinal.Location = new System.Drawing.Point(7, 37);
            this.radioOrdinal.Name = "radioOrdinal";
            this.radioOrdinal.Size = new System.Drawing.Size(56, 24);
            this.radioOrdinal.TabIndex = 19;
            this.radioOrdinal.Text = "The";
            // 
            // comboWeekday
            // 
            this.comboWeekday.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboWeekday.Location = new System.Drawing.Point(183, 39);
            this.comboWeekday.MaxDropDownItems = 9;
            this.comboWeekday.Name = "comboWeekday";
            this.comboWeekday.Size = new System.Drawing.Size(112, 21);
            this.comboWeekday.TabIndex = 21;
            // 
            // radioDayofMonth
            // 
            this.radioDayofMonth.Location = new System.Drawing.Point(7, 9);
            this.radioDayofMonth.Name = "radioDayofMonth";
            this.radioDayofMonth.Size = new System.Drawing.Size(56, 16);
            this.radioDayofMonth.TabIndex = 17;
            this.radioDayofMonth.Text = "Day";
            // 
            // updownDayofMonth
            // 
            this.updownDayofMonth.Location = new System.Drawing.Point(63, 7);
            this.updownDayofMonth.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.updownDayofMonth.Name = "updownDayofMonth";
            this.updownDayofMonth.Size = new System.Drawing.Size(40, 20);
            this.updownDayofMonth.TabIndex = 18;
            // 
            // panelWeekly
            // 
            this.panelWeekly.Controls.Add(this.labelSelectDays);
            this.panelWeekly.Controls.Add(this.labelEvery);
            this.panelWeekly.Controls.Add(this.labelweeks);
            this.panelWeekly.Controls.Add(this.updownWeekInterval);
            this.panelWeekly.Controls.Add(this.checkWeekSunday);
            this.panelWeekly.Controls.Add(this.checkWeekMonday);
            this.panelWeekly.Controls.Add(this.checkWeekTuesday);
            this.panelWeekly.Controls.Add(this.checkWeekWednesday);
            this.panelWeekly.Controls.Add(this.checkWeekThursday);
            this.panelWeekly.Controls.Add(this.checkWeekFriday);
            this.panelWeekly.Controls.Add(this.checkWeekSaturday);
            this.panelWeekly.Location = new System.Drawing.Point(6, 19);
            this.panelWeekly.Name = "panelWeekly";
            this.panelWeekly.Size = new System.Drawing.Size(318, 205);
            this.panelWeekly.TabIndex = 9;
            this.panelWeekly.Visible = false;
            // 
            // labelSelectDays
            // 
            this.labelSelectDays.Location = new System.Drawing.Point(23, 55);
            this.labelSelectDays.Name = "labelSelectDays";
            this.labelSelectDays.Size = new System.Drawing.Size(200, 16);
            this.labelSelectDays.TabIndex = 15;
            this.labelSelectDays.Text = "Select the day(s) of the week below:";
            // 
            // labelEvery
            // 
            this.labelEvery.Location = new System.Drawing.Point(23, 15);
            this.labelEvery.Name = "labelEvery";
            this.labelEvery.Size = new System.Drawing.Size(40, 16);
            this.labelEvery.TabIndex = 14;
            this.labelEvery.Text = "Every";
            // 
            // labelweeks
            // 
            this.labelweeks.Location = new System.Drawing.Point(127, 15);
            this.labelweeks.Name = "labelweeks";
            this.labelweeks.Size = new System.Drawing.Size(100, 16);
            this.labelweeks.TabIndex = 13;
            this.labelweeks.Text = "weeks";
            // 
            // updownWeekInterval
            // 
            this.updownWeekInterval.Location = new System.Drawing.Point(71, 15);
            this.updownWeekInterval.Maximum = new decimal(new int[] {
            52,
            0,
            0,
            0});
            this.updownWeekInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updownWeekInterval.Name = "updownWeekInterval";
            this.updownWeekInterval.Size = new System.Drawing.Size(48, 20);
            this.updownWeekInterval.TabIndex = 12;
            this.updownWeekInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // checkWeekSunday
            // 
            this.checkWeekSunday.Location = new System.Drawing.Point(55, 79);
            this.checkWeekSunday.Name = "checkWeekSunday";
            this.checkWeekSunday.Size = new System.Drawing.Size(104, 16);
            this.checkWeekSunday.TabIndex = 16;
            this.checkWeekSunday.Text = "Sunday";
            // 
            // checkWeekMonday
            // 
            this.checkWeekMonday.Location = new System.Drawing.Point(55, 103);
            this.checkWeekMonday.Name = "checkWeekMonday";
            this.checkWeekMonday.Size = new System.Drawing.Size(104, 16);
            this.checkWeekMonday.TabIndex = 17;
            this.checkWeekMonday.Text = "Monday";
            // 
            // checkWeekTuesday
            // 
            this.checkWeekTuesday.Location = new System.Drawing.Point(55, 127);
            this.checkWeekTuesday.Name = "checkWeekTuesday";
            this.checkWeekTuesday.Size = new System.Drawing.Size(104, 16);
            this.checkWeekTuesday.TabIndex = 18;
            this.checkWeekTuesday.Text = "Tuesday";
            // 
            // checkWeekWednesday
            // 
            this.checkWeekWednesday.Location = new System.Drawing.Point(175, 79);
            this.checkWeekWednesday.Name = "checkWeekWednesday";
            this.checkWeekWednesday.Size = new System.Drawing.Size(104, 16);
            this.checkWeekWednesday.TabIndex = 19;
            this.checkWeekWednesday.Text = "Wednesday";
            // 
            // checkWeekThursday
            // 
            this.checkWeekThursday.Location = new System.Drawing.Point(175, 103);
            this.checkWeekThursday.Name = "checkWeekThursday";
            this.checkWeekThursday.Size = new System.Drawing.Size(104, 16);
            this.checkWeekThursday.TabIndex = 20;
            this.checkWeekThursday.Text = "Thursday";
            // 
            // checkWeekFriday
            // 
            this.checkWeekFriday.Location = new System.Drawing.Point(175, 127);
            this.checkWeekFriday.Name = "checkWeekFriday";
            this.checkWeekFriday.Size = new System.Drawing.Size(104, 16);
            this.checkWeekFriday.TabIndex = 21;
            this.checkWeekFriday.Text = "Friday";
            // 
            // checkWeekSaturday
            // 
            this.checkWeekSaturday.Location = new System.Drawing.Point(175, 151);
            this.checkWeekSaturday.Name = "checkWeekSaturday";
            this.checkWeekSaturday.Size = new System.Drawing.Size(104, 16);
            this.checkWeekSaturday.TabIndex = 22;
            this.checkWeekSaturday.Text = "Saturday";
            // 
            // panelDaily
            // 
            this.panelDaily.Controls.Add(this.updownDayInterval);
            this.panelDaily.Controls.Add(this.radioDayInterval);
            this.panelDaily.Controls.Add(this.labelDays);
            this.panelDaily.Controls.Add(this.radioSelectDays);
            this.panelDaily.Controls.Add(this.checkDaySunday);
            this.panelDaily.Controls.Add(this.checkDayMonday);
            this.panelDaily.Controls.Add(this.checkDayTuesday);
            this.panelDaily.Controls.Add(this.checkDayWednesday);
            this.panelDaily.Controls.Add(this.checkDayThursday);
            this.panelDaily.Controls.Add(this.checkDayFriday);
            this.panelDaily.Controls.Add(this.checkDaySaturday);
            this.panelDaily.Location = new System.Drawing.Point(6, 19);
            this.panelDaily.Name = "panelDaily";
            this.panelDaily.Size = new System.Drawing.Size(318, 205);
            this.panelDaily.TabIndex = 8;
            this.panelDaily.Visible = false;
            // 
            // updownDayInterval
            // 
            this.updownDayInterval.Location = new System.Drawing.Point(77, 25);
            this.updownDayInterval.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.updownDayInterval.Name = "updownDayInterval";
            this.updownDayInterval.Size = new System.Drawing.Size(48, 20);
            this.updownDayInterval.TabIndex = 12;
            this.updownDayInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // radioDayInterval
            // 
            this.radioDayInterval.Checked = true;
            this.radioDayInterval.Location = new System.Drawing.Point(21, 25);
            this.radioDayInterval.Name = "radioDayInterval";
            this.radioDayInterval.Size = new System.Drawing.Size(64, 16);
            this.radioDayInterval.TabIndex = 11;
            this.radioDayInterval.TabStop = true;
            this.radioDayInterval.Text = "Every";
            // 
            // labelDays
            // 
            this.labelDays.Location = new System.Drawing.Point(133, 25);
            this.labelDays.Name = "labelDays";
            this.labelDays.Size = new System.Drawing.Size(32, 16);
            this.labelDays.TabIndex = 21;
            this.labelDays.Text = "days";
            // 
            // radioSelectDays
            // 
            this.radioSelectDays.Location = new System.Drawing.Point(21, 65);
            this.radioSelectDays.Name = "radioSelectDays";
            this.radioSelectDays.Size = new System.Drawing.Size(104, 16);
            this.radioSelectDays.TabIndex = 13;
            this.radioSelectDays.Text = "On these days";
            // 
            // checkDaySunday
            // 
            this.checkDaySunday.Enabled = false;
            this.checkDaySunday.Location = new System.Drawing.Point(53, 89);
            this.checkDaySunday.Name = "checkDaySunday";
            this.checkDaySunday.Size = new System.Drawing.Size(104, 16);
            this.checkDaySunday.TabIndex = 14;
            this.checkDaySunday.Text = "Sunday";
            // 
            // checkDayMonday
            // 
            this.checkDayMonday.Enabled = false;
            this.checkDayMonday.Location = new System.Drawing.Point(53, 113);
            this.checkDayMonday.Name = "checkDayMonday";
            this.checkDayMonday.Size = new System.Drawing.Size(104, 16);
            this.checkDayMonday.TabIndex = 15;
            this.checkDayMonday.Text = "Monday";
            // 
            // checkDayTuesday
            // 
            this.checkDayTuesday.Enabled = false;
            this.checkDayTuesday.Location = new System.Drawing.Point(53, 137);
            this.checkDayTuesday.Name = "checkDayTuesday";
            this.checkDayTuesday.Size = new System.Drawing.Size(104, 16);
            this.checkDayTuesday.TabIndex = 16;
            this.checkDayTuesday.Text = "Tuesday";
            // 
            // checkDayWednesday
            // 
            this.checkDayWednesday.Enabled = false;
            this.checkDayWednesday.Location = new System.Drawing.Point(173, 89);
            this.checkDayWednesday.Name = "checkDayWednesday";
            this.checkDayWednesday.Size = new System.Drawing.Size(104, 16);
            this.checkDayWednesday.TabIndex = 17;
            this.checkDayWednesday.Text = "Wednesday";
            // 
            // checkDayThursday
            // 
            this.checkDayThursday.Enabled = false;
            this.checkDayThursday.Location = new System.Drawing.Point(173, 113);
            this.checkDayThursday.Name = "checkDayThursday";
            this.checkDayThursday.Size = new System.Drawing.Size(104, 16);
            this.checkDayThursday.TabIndex = 18;
            this.checkDayThursday.Text = "Thursday";
            // 
            // checkDayFriday
            // 
            this.checkDayFriday.Enabled = false;
            this.checkDayFriday.Location = new System.Drawing.Point(173, 137);
            this.checkDayFriday.Name = "checkDayFriday";
            this.checkDayFriday.Size = new System.Drawing.Size(104, 16);
            this.checkDayFriday.TabIndex = 19;
            this.checkDayFriday.Text = "Friday";
            // 
            // checkDaySaturday
            // 
            this.checkDaySaturday.Enabled = false;
            this.checkDaySaturday.Location = new System.Drawing.Point(173, 161);
            this.checkDaySaturday.Name = "checkDaySaturday";
            this.checkDaySaturday.Size = new System.Drawing.Size(104, 16);
            this.checkDaySaturday.TabIndex = 20;
            this.checkDaySaturday.Text = "Saturday";
            // 
            // panelTimespan
            // 
            this.panelTimespan.Controls.Add(this.comboTimeUnits);
            this.panelTimespan.Controls.Add(this.labelTimeUnits);
            this.panelTimespan.Controls.Add(this.labelTimeInterval);
            this.panelTimespan.Controls.Add(this.updownTimeInterval);
            this.panelTimespan.Location = new System.Drawing.Point(6, 19);
            this.panelTimespan.Name = "panelTimespan";
            this.panelTimespan.Size = new System.Drawing.Size(318, 205);
            this.panelTimespan.TabIndex = 7;
            // 
            // comboTimeUnits
            // 
            this.comboTimeUnits.Location = new System.Drawing.Point(82, 44);
            this.comboTimeUnits.Name = "comboTimeUnits";
            this.comboTimeUnits.Size = new System.Drawing.Size(128, 21);
            this.comboTimeUnits.TabIndex = 18;
            // 
            // labelTimeUnits
            // 
            this.labelTimeUnits.Location = new System.Drawing.Point(18, 44);
            this.labelTimeUnits.Name = "labelTimeUnits";
            this.labelTimeUnits.Size = new System.Drawing.Size(64, 23);
            this.labelTimeUnits.TabIndex = 17;
            this.labelTimeUnits.Text = "Units";
            // 
            // labelTimeInterval
            // 
            this.labelTimeInterval.Location = new System.Drawing.Point(18, 20);
            this.labelTimeInterval.Name = "labelTimeInterval";
            this.labelTimeInterval.Size = new System.Drawing.Size(64, 20);
            this.labelTimeInterval.TabIndex = 15;
            this.labelTimeInterval.Text = "Interval";
            // 
            // updownTimeInterval
            // 
            this.updownTimeInterval.Location = new System.Drawing.Point(82, 20);
            this.updownTimeInterval.Name = "updownTimeInterval";
            this.updownTimeInterval.Size = new System.Drawing.Size(128, 20);
            this.updownTimeInterval.TabIndex = 16;
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(10, 122);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(79, 13);
            this.labelType.TabIndex = 6;
            this.labelType.Text = "Schedule Type";
            // 
            // comboScheduleType
            // 
            this.comboScheduleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboScheduleType.FormattingEnabled = true;
            this.comboScheduleType.Location = new System.Drawing.Point(95, 118);
            this.comboScheduleType.Name = "comboScheduleType";
            this.comboScheduleType.Size = new System.Drawing.Size(199, 21);
            this.comboScheduleType.TabIndex = 4;
            this.comboScheduleType.SelectedIndexChanged += new System.EventHandler(this.comboScheduleType_SelectedIndexChanged);
            // 
            // dateStartTime
            // 
            this.dateStartTime.CustomFormat = " h:mm tt";
            this.dateStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateStartTime.Location = new System.Drawing.Point(95, 72);
            this.dateStartTime.Name = "dateStartTime";
            this.dateStartTime.ShowUpDown = true;
            this.dateStartTime.Size = new System.Drawing.Size(199, 20);
            this.dateStartTime.TabIndex = 3;
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(10, 76);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(55, 13);
            this.labelTime.TabIndex = 2;
            this.labelTime.Text = "Start Time";
            // 
            // dateStartDate
            // 
            this.dateStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateStartDate.Location = new System.Drawing.Point(95, 26);
            this.dateStartDate.Name = "dateStartDate";
            this.dateStartDate.Size = new System.Drawing.Size(199, 20);
            this.dateStartDate.TabIndex = 1;
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(10, 30);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(55, 13);
            this.labelDate.TabIndex = 0;
            this.labelDate.Text = "Start Date";
            // 
            // tabTask
            // 
            this.tabTask.Controls.Add(this.groupTask);
            this.tabTask.Controls.Add(this.buttonFindTask);
            this.tabTask.Controls.Add(this.textTaskClass);
            this.tabTask.Controls.Add(this.labelClass);
            this.tabTask.Location = new System.Drawing.Point(4, 22);
            this.tabTask.Name = "tabTask";
            this.tabTask.Size = new System.Drawing.Size(341, 409);
            this.tabTask.TabIndex = 2;
            this.tabTask.Text = "Task";
            this.tabTask.UseVisualStyleBackColor = true;
            // 
            // groupTask
            // 
            this.groupTask.Controls.Add(this.propertyGridTask);
            this.groupTask.Location = new System.Drawing.Point(6, 91);
            this.groupTask.Name = "groupTask";
            this.groupTask.Size = new System.Drawing.Size(328, 309);
            this.groupTask.TabIndex = 3;
            this.groupTask.TabStop = false;
            this.groupTask.Text = "Task Properties";
            // 
            // propertyGridTask
            // 
            this.propertyGridTask.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.propertyGridTask.Location = new System.Drawing.Point(6, 19);
            this.propertyGridTask.Name = "propertyGridTask";
            this.propertyGridTask.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGridTask.Size = new System.Drawing.Size(312, 281);
            this.propertyGridTask.TabIndex = 2;
            this.propertyGridTask.ToolbarVisible = false;
            // 
            // buttonFindTask
            // 
            this.buttonFindTask.Location = new System.Drawing.Point(249, 46);
            this.buttonFindTask.Name = "buttonFindTask";
            this.buttonFindTask.Size = new System.Drawing.Size(75, 23);
            this.buttonFindTask.TabIndex = 2;
            this.buttonFindTask.Text = "FindTask";
            this.buttonFindTask.UseVisualStyleBackColor = true;
            this.buttonFindTask.Click += new System.EventHandler(this.buttonFindTask_Click);
            // 
            // textTaskClass
            // 
            this.textTaskClass.Location = new System.Drawing.Point(60, 20);
            this.textTaskClass.Name = "textTaskClass";
            this.textTaskClass.Size = new System.Drawing.Size(264, 20);
            this.textTaskClass.TabIndex = 1;
            this.textTaskClass.TextChanged += new System.EventHandler(this.textTaskAssembly_TextChanged);
            // 
            // labelClass
            // 
            this.labelClass.AutoSize = true;
            this.labelClass.Location = new System.Drawing.Point(3, 20);
            this.labelClass.Name = "labelClass";
            this.labelClass.Size = new System.Drawing.Size(32, 13);
            this.labelClass.TabIndex = 0;
            this.labelClass.Text = "Class";
            // 
            // ScheduledPropertyPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "ScheduledPropertyPage";
            this.Size = new System.Drawing.Size(356, 439);
            this.tabControl.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.groupErrorHandling.ResumeLayout(false);
            this.groupErrorHandling.PerformLayout();
            this.tabSchedule.ResumeLayout(false);
            this.tabSchedule.PerformLayout();
            this.groupSchedule.ResumeLayout(false);
            this.panelMonthly.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.updownDayofMonth)).EndInit();
            this.panelWeekly.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.updownWeekInterval)).EndInit();
            this.panelDaily.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.updownDayInterval)).EndInit();
            this.panelTimespan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.updownTimeInterval)).EndInit();
            this.tabTask.ResumeLayout(false);
            this.tabTask.PerformLayout();
            this.groupTask.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        //General Tab
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textScheduleName;
        private System.Windows.Forms.GroupBox groupErrorHandling;
        private System.Windows.Forms.CheckBox checkSuspendMessage;
        //Schedule Tab
        private System.Windows.Forms.TabPage tabSchedule;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.DateTimePicker dateStartDate;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.DateTimePicker dateStartTime;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.ComboBox comboScheduleType;
        //TimeSpan Panel
        private System.Windows.Forms.Label labelTimeInterval;
        private System.Windows.Forms.NumericUpDown updownTimeInterval;
        private System.Windows.Forms.Label labelTimeUnits;
        private System.Windows.Forms.ComboBox comboTimeUnits;
        //Daily Panel
        //Weekly Panel
        //Monthly Panel
        
        
        //private System.Windows.Forms.Label labelInterval;
       // private System.Windows.Forms.TextBox timeSpan;
        private System.Windows.Forms.GroupBox groupTask;
        private System.Windows.Forms.Panel panelTimespan;
        private System.Windows.Forms.GroupBox groupSchedule;
        private System.Windows.Forms.Panel panelDaily;
        
        
        
        private System.Windows.Forms.Panel panelWeekly;
        private System.Windows.Forms.NumericUpDown updownDayInterval;
        private System.Windows.Forms.RadioButton radioDayInterval;
        private System.Windows.Forms.Label labelDays;
        private System.Windows.Forms.RadioButton radioSelectDays;
        private System.Windows.Forms.CheckBox checkDaySunday;
        private System.Windows.Forms.CheckBox checkDayMonday;
        private System.Windows.Forms.CheckBox checkDayTuesday;
        private System.Windows.Forms.CheckBox checkDayWednesday;
        private System.Windows.Forms.CheckBox checkDayThursday;
        private System.Windows.Forms.CheckBox checkDayFriday;
        private System.Windows.Forms.CheckBox checkDaySaturday;
        private System.Windows.Forms.Label labelSelectDays;
        private System.Windows.Forms.Label labelEvery;
        private System.Windows.Forms.Label labelweeks;
        private System.Windows.Forms.NumericUpDown updownWeekInterval;
        private System.Windows.Forms.CheckBox checkWeekSunday;
        private System.Windows.Forms.CheckBox checkWeekMonday;
        private System.Windows.Forms.CheckBox checkWeekTuesday;
        private System.Windows.Forms.CheckBox checkWeekWednesday;
        private System.Windows.Forms.CheckBox checkWeekThursday;
        private System.Windows.Forms.CheckBox checkWeekFriday;
        private System.Windows.Forms.CheckBox checkWeekSaturday;
        private System.Windows.Forms.Panel panelMonthly;
        private System.Windows.Forms.CheckBox checkApril;
        private System.Windows.Forms.CheckBox checkAugust;
        private System.Windows.Forms.CheckBox checkDecember;
        private System.Windows.Forms.CheckBox checkNovember;
        private System.Windows.Forms.CheckBox checkJuly;
        private System.Windows.Forms.CheckBox checkMarch;
        private System.Windows.Forms.CheckBox checkFebruary;
        private System.Windows.Forms.CheckBox checkJune;
        private System.Windows.Forms.CheckBox checkOctober;
        private System.Windows.Forms.CheckBox checkSeptember;
        private System.Windows.Forms.CheckBox checkMay;
        private System.Windows.Forms.CheckBox checkJanuary;
        private System.Windows.Forms.ComboBox comboOrdinal;
        private System.Windows.Forms.RadioButton radioOrdinal;
        private System.Windows.Forms.ComboBox comboWeekday;
        private System.Windows.Forms.RadioButton radioDayofMonth;
        private System.Windows.Forms.NumericUpDown updownDayofMonth;

        //Task Tab
        private System.Windows.Forms.TabPage tabTask;
        private System.Windows.Forms.Label labelClass;
        private System.Windows.Forms.TextBox textTaskClass;
        private System.Windows.Forms.Button buttonFindTask;
        private System.Windows.Forms.PropertyGrid propertyGridTask;

        
       
    }
}
