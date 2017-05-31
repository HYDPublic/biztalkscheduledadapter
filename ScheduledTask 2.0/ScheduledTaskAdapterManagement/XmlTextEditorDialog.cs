using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;

namespace ScheduledTaskAdapterManagement
{
	/// <summary>
	/// XmlTextEditorDialog: Utility for editing and validating Xml data
	/// </summary>
	public class XmlTextEditorDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.TextBox textBox;
		private System.Windows.Forms.MenuItem menuWordwrap;
		private System.Windows.Forms.MenuItem menuCut;
		private System.Windows.Forms.MenuItem menuCopy;
		private System.Windows.Forms.MenuItem menuPaste;
		private System.Windows.Forms.MenuItem menuSelectAll;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuEdit;
		private System.Windows.Forms.MenuItem menuSeparator1;
		private System.Windows.Forms.MenuItem menuView;
		private System.Windows.Forms.MenuItem menuFile;
		private System.Windows.Forms.MenuItem menuValidate;
		
		public string EditText
		{
			//textbox requires crlf, but Xml strips \r
			get{return textBox.Text.Replace("\r\n","\n");}
			set{textBox.Text = value.Replace("\n", "\r\n");}
		}
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public XmlTextEditorDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(XmlTextEditorDialog));
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.textBox = new System.Windows.Forms.TextBox();
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.menuFile = new System.Windows.Forms.MenuItem();
			this.menuValidate = new System.Windows.Forms.MenuItem();
			this.menuEdit = new System.Windows.Forms.MenuItem();
			this.menuCut = new System.Windows.Forms.MenuItem();
			this.menuCopy = new System.Windows.Forms.MenuItem();
			this.menuPaste = new System.Windows.Forms.MenuItem();
			this.menuSeparator1 = new System.Windows.Forms.MenuItem();
			this.menuSelectAll = new System.Windows.Forms.MenuItem();
			this.menuView = new System.Windows.Forms.MenuItem();
			this.menuWordwrap = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(232, 224);
			this.okButton.Name = "okButton";
			this.okButton.TabIndex = 1;
			this.okButton.Text = "OK";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(320, 224);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "Cancel";
			// 
			// textBox
			// 
			this.textBox.AcceptsReturn = true;
			this.textBox.AcceptsTab = true;
			this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox.Location = new System.Drawing.Point(8, 8);
			this.textBox.Multiline = true;
			this.textBox.Name = "textBox";
			this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox.Size = new System.Drawing.Size(392, 208);
			this.textBox.TabIndex = 1;
			this.textBox.Text = "";
			this.textBox.WordWrap = false;
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuFile,
																					 this.menuEdit,
																					 this.menuView});
			// 
			// menuFile
			// 
			this.menuFile.Index = 0;
			this.menuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuValidate});
			this.menuFile.Text = "File";
			// 
			// menuValidate
			// 
			this.menuValidate.Index = 0;
			this.menuValidate.Text = "Validate";
			this.menuValidate.Click += new System.EventHandler(this.menuValidate_Click);
			// 
			// menuEdit
			// 
			this.menuEdit.Index = 1;
			this.menuEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuCut,
																					 this.menuCopy,
																					 this.menuPaste,
																					 this.menuSeparator1,
																					 this.menuSelectAll});
			this.menuEdit.Text = "Edit";
			// 
			// menuCut
			// 
			this.menuCut.Index = 0;
			this.menuCut.Text = "Cut";
			this.menuCut.Click += new System.EventHandler(this.menuCut_Click);
			// 
			// menuCopy
			// 
			this.menuCopy.Index = 1;
			this.menuCopy.Text = "Copy";
			this.menuCopy.Click += new System.EventHandler(this.menuCopy_Click);
			// 
			// menuPaste
			// 
			this.menuPaste.Index = 2;
			this.menuPaste.Text = "Paste";
			this.menuPaste.Click += new System.EventHandler(this.menuPaste_Click);
			// 
			// menuSeparator1
			// 
			this.menuSeparator1.Index = 3;
			this.menuSeparator1.Text = "-";
			// 
			// menuSelectAll
			// 
			this.menuSelectAll.Index = 4;
			this.menuSelectAll.Text = "Select All";
			this.menuSelectAll.Click += new System.EventHandler(this.menuSelectAll_Click);
			// 
			// menuView
			// 
			this.menuView.Index = 2;
			this.menuView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuWordwrap});
			this.menuView.Text = "View";
			// 
			// menuWordwrap
			// 
			this.menuWordwrap.Index = 0;
			this.menuWordwrap.Text = "Wordwrap";
			this.menuWordwrap.Click += new System.EventHandler(this.menuWordwrap_Click_1);
			// 
			// XmlTextEditorDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(408, 257);
			this.Controls.Add(this.textBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Menu = this.mainMenu;
			this.MinimizeBox = false;
			this.Name = "XmlTextEditorDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Xml Text Editor";
			this.ResumeLayout(false);

		}
		#endregion

		private void menuCut_Click(object sender, System.EventArgs e)
		{
			textBox.Cut();
		}

		private void menuCopy_Click(object sender, System.EventArgs e)
		{
			textBox.Copy();
		}

		private void menuPaste_Click(object sender, System.EventArgs e)
		{
			textBox.Paste();
		}

		private void menuSelectAll_Click(object sender, System.EventArgs e)
		{
			textBox.SelectAll();
		}

		private void menuWordwrap_Click_1(object sender, System.EventArgs e)
		{
			if (menuWordwrap.Checked)
			{
				menuWordwrap.Checked = false;
				textBox.WordWrap = false;
			}
			else
			{
				menuWordwrap.Checked = true;
				textBox.WordWrap = true;
			}
		}

		private void menuValidate_Click(object sender, System.EventArgs e)
		{
			XmlDocument test = new XmlDocument();
			try
			{
				test.LoadXml(textBox.Text);
			}
			catch(Exception exception)
			{
				MessageBox.Show(exception.Message, "Error");
			}
		}

		private void okButton_Click(object sender, System.EventArgs e)
		{
			XmlDocument test = new XmlDocument();
			try
			{
				test.LoadXml(textBox.Text);
			}
			catch(Exception exception)
			{
				MessageBox.Show(exception.Message, "Error");
				this.DialogResult = DialogResult.None;
			}
		}
	}
}
