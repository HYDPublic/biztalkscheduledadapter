using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml;
using ScheduledTaskAdapter.TaskComponents;


namespace ScheduledTaskAdapter.Admin
{
	/// <summary>
	/// Implements a basic user interface for editing a text property 
	/// within a visual designer.
	/// </summary>
	public class XmlTextUITypeEditor : OpenFileUITypeEditor 
	{
		private IWindowsFormsEditorService service = null;
		private XmlTextEditorDialog dialog;

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) 
		{
			if (null != context && null != context.Instance) 
			{
				return UITypeEditorEditStyle.Modal;
			}
			return base.GetEditStyle(context);
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) 
		{
			if (context != null && context.Instance != null && provider != null) 
			{
				this.service = (IWindowsFormsEditorService) provider.GetService(typeof(IWindowsFormsEditorService));
				if (this.service != null) 
				{
					this.dialog =  new XmlTextEditorDialog();
					if (value != null)
						this.dialog.EditText = (string)value;
					if (this.service.ShowDialog(this.dialog) == DialogResult.OK)
					{
						value = this.dialog.EditText;
					}
				}
			}
			return value;
		}

		private void Exit(object sender, EventArgs e) 
		{
			if (null != this.service) 
			{
				this.service.CloseDropDown();
			}
		}
	}
	public class XmlTextConverter : System.ComponentModel.StringConverter 
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return false;
		}
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		} 
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (typeof(string) == destinationType && value is string)
			{
				return ((string)value).Replace("\n", "\\n");
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
} 