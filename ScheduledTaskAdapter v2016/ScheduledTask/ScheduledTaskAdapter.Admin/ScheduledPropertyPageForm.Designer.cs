namespace ScheduledTaskAdapter.Admin
{
    partial class ScheduledPropertyPageForm
    {
        // Fields
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;       
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelFooter;
        private System.Windows.Forms.Panel panelBody;          
        private ScheduledPropertyPage scheduledPropertyPage;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScheduledPropertyPageForm));
            this.flowLayoutPanelFooter = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.panelBody = new System.Windows.Forms.Panel();
            this.flowLayoutPanelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanelFooter
            // 
            this.flowLayoutPanelFooter.AutoSize = true;
            this.flowLayoutPanelFooter.Controls.Add(this.buttonApply);
            this.flowLayoutPanelFooter.Controls.Add(this.buttonCancel);
            this.flowLayoutPanelFooter.Controls.Add(this.buttonOK);
            this.flowLayoutPanelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanelFooter.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelFooter.Location = new System.Drawing.Point(0, 455);
            this.flowLayoutPanelFooter.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelFooter.Name = "flowLayoutPanelFooter";
            this.flowLayoutPanelFooter.Padding = new System.Windows.Forms.Padding(3);
            this.flowLayoutPanelFooter.Size = new System.Drawing.Size(364, 38);
            this.flowLayoutPanelFooter.TabIndex = 1;
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(269, 6);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(86, 26);
            this.buttonApply.TabIndex = 1;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(177, 6);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(86, 26);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(85, 6);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(86, 26);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // panelBody
            // 
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBody.Location = new System.Drawing.Point(0, 0);
            this.panelBody.Name = "panelBody";
            this.panelBody.Padding = new System.Windows.Forms.Padding(5);
            this.panelBody.Size = new System.Drawing.Size(364, 450);
            this.panelBody.TabIndex = 0;
            // 
            // ScheduledPropertyPageForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(364, 493);
            this.Controls.Add(this.panelBody);
            this.Controls.Add(this.flowLayoutPanelFooter);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScheduledPropertyPageForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.ScheduledPropertyPageForm_Load);
            this.flowLayoutPanelFooter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}