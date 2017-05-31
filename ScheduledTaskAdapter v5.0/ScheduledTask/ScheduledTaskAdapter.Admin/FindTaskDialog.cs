using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace ScheduledTaskAdapter.Admin
{
	/// <summary>
	/// Dialog box for selecting a .Net type that implements a particular interface
	/// </summary>
	public class AssemblyQualifiedTypeNameDialog : System.Windows.Forms.Form
	{		
		private Type type = null;
        private IList<Type> supportedInterfaces = new List<Type>();
		private System.Windows.Forms.TextBox qualifiedName;
		private System.Windows.Forms.TreeView typeTree;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button browseButton;
		private System.Windows.Forms.Button selectButton;
		private System.Windows.Forms.Label labelBrowser;
		private System.Windows.Forms.Label labelResult;

		//public string AssemblyName	{get{return assemblyName; }}
        public Type Type { get { return type; } set { type = value; } }
		
        public IList<Type> SupportedInterfaces
        {
            get { return supportedInterfaces; }
        }
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AssemblyQualifiedTypeNameDialog()
		{
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssemblyQualifiedTypeNameDialog));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.qualifiedName = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.selectButton = new System.Windows.Forms.Button();
            this.typeTree = new System.Windows.Forms.TreeView();
            this.labelBrowser = new System.Windows.Forms.Label();
            this.labelResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(252, 349);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(360, 349);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            // 
            // qualifiedName
            // 
            this.qualifiedName.Location = new System.Drawing.Point(16, 320);
            this.qualifiedName.Name = "qualifiedName";
            this.qualifiedName.Size = new System.Drawing.Size(419, 20);
            this.qualifiedName.TabIndex = 5;
            this.qualifiedName.TextChanged += new System.EventHandler(this.qualifiedName_TextChanged);
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(360, 47);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "Browse";
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // selectButton
            // 
            this.selectButton.Enabled = false;
            this.selectButton.Location = new System.Drawing.Point(360, 79);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(75, 23);
            this.selectButton.TabIndex = 4;
            this.selectButton.Text = "Select";
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // typeTree
            // 
            this.typeTree.Location = new System.Drawing.Point(16, 48);
            this.typeTree.Name = "typeTree";
            this.typeTree.Size = new System.Drawing.Size(327, 240);
            this.typeTree.Sorted = true;
            this.typeTree.TabIndex = 3;
            this.typeTree.DoubleClick += new System.EventHandler(this.typeTree_DoubleClick);
            this.typeTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.typeTree_AfterSelect);
            // 
            // labelBrowser
            // 
            this.labelBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBrowser.Location = new System.Drawing.Point(16, 32);
            this.labelBrowser.Name = "labelBrowser";
            this.labelBrowser.Size = new System.Drawing.Size(176, 16);
            this.labelBrowser.TabIndex = 0;
            this.labelBrowser.Text = "Assembly";
            // 
            // labelResult
            // 
            this.labelResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelResult.Location = new System.Drawing.Point(16, 304);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(192, 16);
            this.labelResult.TabIndex = 6;
            this.labelResult.Text = "Assembly Qualified Type Name";
            // 
            // AssemblyQualifiedTypeNameDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(450, 384);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.labelBrowser);
            this.Controls.Add(this.typeTree);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.qualifiedName);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AssemblyQualifiedTypeNameDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Assembly Qualified Type Name";
            this.Load += new System.EventHandler(this.FindTask_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void FindTask_Load(object sender, System.EventArgs e)
		{
            if (supportedInterfaces.Count == 0)
			{
				MessageBox.Show("Assembly Qualified Name Dialog: requires the InterfaceType property");
				this.DialogResult = DialogResult.Cancel;
				this.Close();
			}
			if (type != null)
			{
				try
				{					
					PopulateTree(type.Assembly);
				}
				catch(Exception)
				{
					type = null;
				}
			}
		}

		private void PopulateTree(Assembly assembly)
		{
			typeTree.Nodes.Clear();
						
			TreeNode node = new TreeNode(assembly.FullName);
			node.Tag = null;
			typeTree.Nodes.Add(node);
			Type[] types = assembly.GetExportedTypes();
			foreach(Type type in types)
			{
				System.Type[] interfaces = type.FindInterfaces(new TypeFilter(InterfaceFilter), this.SupportedInterfaces);
				TreeNode child = new TreeNode(type.Name);
				if (interfaces.Length == 0)
				{
					child.ForeColor = Color.Gray;
					child.Tag = null;
				}
				else
				{
                    child.Tag = type;
				}
				node.Nodes.Add(child);
			}
			node.Expand();
		}
		
		private  bool InterfaceFilter(Type typeObj, Object criteriaObj)
		{
            if ((typeObj.ToString() == ((IList<Type>)criteriaObj)[0].ToString()) || 
                (typeObj.ToString() == ((IList<Type>)criteriaObj)[1].ToString()))
                return true;
            return false;
            #region to be removed
            //return ((IList<Type>)criteriaObj).Contains(typeObj);
            //return (typeObj == ((IList<Type>)criteriaObj)[0]) || (typeObj == ((IList<Type>)criteriaObj)[1]);
            //if (typeObj.ToString() == criteriaObj.ToString())
            //    return true;
            //else
            //    return false;
            #endregion
        }

		private void browseButton_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog fileDialog = new OpenFileDialog();
			fileDialog.Filter = "Executables(*.dll, *.exe)|*.dll; *.exe" ;
			fileDialog.InitialDirectory = "C:\\Program Files (x86)\\BizTalk ScheduledTask Adapter 5.0";
			if (fileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					Assembly assembly = Assembly.LoadFile(fileDialog.FileName);
					PopulateTree(assembly);
				}
				catch(Exception exception)
				{
					throw(new ApplicationException("Qualified Name Dialog: " + exception.Message, exception));
				}
			}
		}

		private void qualifiedName_TextChanged(object sender, System.EventArgs e)
		{
			if (qualifiedName.Text == string.Empty)
				okButton.Enabled = false;
			else
				okButton.Enabled = true;
		}

		private void typeTree_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if (e.Node.Tag != null)
			{
				selectButton.Enabled = true;
			}
			else
			{
				selectButton.Enabled = false;
			}
		}

		private void typeTree_DoubleClick(object sender, System.EventArgs e)
		{
			Type tag = (Type)((TreeView)sender).SelectedNode.Tag;
			if (tag != null)
			{
				this.type = tag;
                qualifiedName.Text = this.type.AssemblyQualifiedName;
			}
		}

		private void selectButton_Click(object sender, System.EventArgs e)
		{
            this.type = (Type)typeTree.SelectedNode.Tag;
            qualifiedName.Text = this.type.AssemblyQualifiedName;
		}
	}
}
