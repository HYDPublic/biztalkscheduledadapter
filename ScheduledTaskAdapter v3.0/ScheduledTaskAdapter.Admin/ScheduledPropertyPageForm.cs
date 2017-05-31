using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ScheduledTaskAdapter.Admin
{
    public partial class ScheduledPropertyPageForm : Form
    {
        private ScheduledPropertyPageFrame frame;
        // Methods
        public ScheduledPropertyPageForm(ScheduledPropertyPageFrame frame)
        {            
            this.frame = frame;
            this.InitializeComponent();           
        }

        private void ScheduledPropertyPageForm_Load(object sender, EventArgs e)
        {
            this.scheduledPropertyPage = new ScheduledPropertyPage(frame.AdapterConfiguration);
            this.scheduledPropertyPage.Dock = DockStyle.Fill;
            this.panelBody.Controls.Add(scheduledPropertyPage);
        }
     
        private void buttonApply_Click(object sender, EventArgs e)
        {
            scheduledPropertyPage.Apply();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close();
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
           
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (scheduledPropertyPage.Apply())
            {
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
        }       
    }
}
