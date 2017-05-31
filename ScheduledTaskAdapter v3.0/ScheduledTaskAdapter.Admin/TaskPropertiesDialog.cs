using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using ScheduledTaskAdapter.TaskComponents;


namespace ScheduledTaskAdapter.Management
{
	/// <summary>
	/// TaskDialog: dialog for setting task properties
	/// </summary>
	public class TaskPropertiesDialog : System.Windows.Forms.Form
	{
		//Non-form fields
		private bool loaded;
		private string assemblyName;
		private string typeName;
		public object taskArguments;
		private XmlDocument taskProperties;

		//Properties
		public XmlDocument TaskProperties
		{
			get{return taskProperties;}            
		}
		//Form Fields
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.PropertyGrid propertyGrid;
		private System.Windows.Forms.Button findButton;
		private System.Windows.Forms.TextBox textQualifiedName;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelTaskParameters;
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TaskPropertiesDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			loaded = false;
			taskProperties = new XmlDocument();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskPropertiesDialog));
            this.okButton = new System.Windows.Forms.Button();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.textQualifiedName = new System.Windows.Forms.TextBox();
            this.findButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelTaskParameters = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(216, 392);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // propertyGrid
            // 
            this.propertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.propertyGrid.Location = new System.Drawing.Point(16, 144);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid.Size = new System.Drawing.Size(368, 192);
            this.propertyGrid.TabIndex = 1;
            this.propertyGrid.ToolbarVisible = false;
            // 
            // textQualifiedName
            // 
            this.textQualifiedName.Location = new System.Drawing.Point(16, 40);
            this.textQualifiedName.Name = "textQualifiedName";
            this.textQualifiedName.Size = new System.Drawing.Size(368, 20);
            this.textQualifiedName.TabIndex = 2;
            this.textQualifiedName.TextChanged += new System.EventHandler(this.textQualifiedName_TextChanged);
            // 
            // findButton
            // 
            this.findButton.Location = new System.Drawing.Point(288, 72);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(88, 23);
            this.findButton.TabIndex = 3;
            this.findButton.Text = "Find Task";
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(304, 392);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Assembly Qualified Type Name";
            // 
            // labelTaskParameters
            // 
            this.labelTaskParameters.Location = new System.Drawing.Point(16, 120);
            this.labelTaskParameters.Name = "labelTaskParameters";
            this.labelTaskParameters.Size = new System.Drawing.Size(100, 16);
            this.labelTaskParameters.TabIndex = 6;
            this.labelTaskParameters.Text = "Task Parameters";
            // 
            // TaskPropertiesDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(394, 432);
            this.Controls.Add(this.labelTaskParameters);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.findButton);
            this.Controls.Add(this.textQualifiedName);
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaskPropertiesDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Task Properties";
            this.Load += new System.EventHandler(this.TaskDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void TaskDialog_Load(object sender, System.EventArgs e)
		{
			if (taskProperties.DocumentElement != null)
			{
				this.LoadTaskProperties();
			}
			else
			{
				XmlNode root = taskProperties.CreateNode("element", "task", "");
				taskProperties.AppendChild(root);
			}
		}

		private void findButton_Click(object sender, System.EventArgs e)
		{
			FindTaskDialog typeDialog =  new FindTaskDialog();			
            typeDialog.SupportedInterfaces.Add(typeof(IScheduledTaskStreamProvider));
            typeDialog.SupportedInterfaces.Add(typeof(IScheduledTaskStreamProvider2));
			typeDialog.AssemblyQualifiedTypeName = textQualifiedName.Text;
			if (typeDialog.ShowDialog() == DialogResult.OK)
			{
				textQualifiedName.Text = typeDialog.AssemblyQualifiedTypeName;
			}
		}
		private void LoadTaskProperties()
		{
			XmlNode node = taskProperties.SelectSingleNode("/task/qualifiedname");
			if (node != null)
			{
				textQualifiedName.Text = node.InnerText;
			}
			//most of the work is done in textQualifiedName_TextChanged()
		}
		private void UnloadTaskProperties()
		{
			if (textQualifiedName.Text != string.Empty)
			{
				XmlNode node = taskProperties.SelectSingleNode("/task/qualifiedname");
				if (node == null)
				{
					node = taskProperties.CreateNode("element", "qualifiedname", "");
					node.InnerText = textQualifiedName.Text;
					taskProperties.DocumentElement.AppendChild(node);
				}
				else
				{
					node.InnerText = textQualifiedName.Text;
				}			               
            }
			if (taskArguments != null)
			{
				XmlSerializer serializer = new XmlSerializer(taskArguments.GetType());
				StringWriter strWriter = new StringWriter();
				serializer.Serialize(strWriter,taskArguments);
				XmlNode node = taskProperties.SelectSingleNode("/task/arguments");
				if (node == null)
				{
					node = taskProperties.CreateNode("element", "arguments", "");
					node.InnerText = strWriter.ToString();
					taskProperties.DocumentElement.AppendChild(node);
				}
				else
				{
					node.InnerText = strWriter.ToString();
				}
			}
		}

		private void textQualifiedName_TextChanged(object sender, System.EventArgs e)
		{
			int offset = textQualifiedName.Text.IndexOf(",",0);
			typeName = textQualifiedName.Text.Substring(0, offset);
			assemblyName = textQualifiedName.Text.Substring(offset + 1).Trim();
           
			Assembly assembly = Assembly.Load(assemblyName);
			object provider = assembly.CreateInstance(typeName);

           	object [] args = new object [] {};
			Type parameterType = (Type)provider.GetType().InvokeMember("GetParameterType", BindingFlags.InvokeMethod, null, provider, args);
            if (parameterType != null)
			{
				if (!loaded) 
				{
					XmlNode node = taskProperties.SelectSingleNode("/task/arguments");
					if (node != null)
					{
						StringReader strReader = new StringReader(node.InnerText);
                        XmlSerializer serializer = new XmlSerializer(parameterType);
						taskArguments = serializer.Deserialize(strReader);
						propertyGrid.SelectedObject = taskArguments;
						loaded = true;
						return;
					}
				}
                taskArguments = assembly.CreateInstance(parameterType.ToString());
				propertyGrid.SelectedObject = taskArguments;
				loaded = true;
			}
			return;
		}

		private void okButton_Click(object sender, System.EventArgs e)
		{
			UnloadTaskProperties();
		}

        private bool InterfaceFilter(Type typeObj, Object criteriaObj)
        {

            if (criteriaObj.ToString() == typeObj.ToString())
                return true;
            return false;
        }
	}
}
