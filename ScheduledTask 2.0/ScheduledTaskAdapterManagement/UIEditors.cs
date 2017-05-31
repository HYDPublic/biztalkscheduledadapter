using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml;
using ScheduledTask.TaskComponents;


namespace ScheduledTaskAdapterManagement
{
	/// <summary>
	/// Implements a basic user interface for editing a text property 
	/// within a visual designer.
	/// </summary>
	public class XmlTextUITypeEditor : UITypeEditor 
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
			if (null != context && null != context.Instance && null != provider) 
			{
				this.service = (IWindowsFormsEditorService) provider.GetService(typeof(IWindowsFormsEditorService));
				if (null != this.service) 
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
	/// <summary>
	/// Implements a user interface for setting schedule parameters
	/// within a visual designer.
	/// </summary>
	public class ScheduleUITypeEditor : UITypeEditor 
	{
		private IWindowsFormsEditorService service = null;
		private ScheduleDialog dialog;

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
			if (null != context && null != context.Instance && null != provider) 
			{
				this.service = (IWindowsFormsEditorService) provider.GetService(typeof(IWindowsFormsEditorService));
				if (null != this.service) 
				{
					this.dialog = new ScheduleDialog();
					if (value != null)
						this.dialog.ConfigXml.LoadXml((string)value);
					if (this.service.ShowDialog(this.dialog) == DialogResult.OK)
					{
						value = this.dialog.ConfigXml.OuterXml;
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

	public class ScheduleConverter : System.ComponentModel.StringConverter 
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
				if ((string)value == string.Empty)
					return string.Empty;			   
				XmlDocument configXml = new XmlDocument();
				configXml.LoadXml((string)value);
				return configXml.DocumentElement.GetAttribute("type") + " Schedule";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}

	public class TaskPropertiesUITypeEditor : UITypeEditor 
	{
		private IWindowsFormsEditorService service = null;
		private TaskPropertiesDialog dialog;

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
			if (null != context && null != context.Instance && null != provider) 
			{
				this.service = (IWindowsFormsEditorService) provider.GetService(typeof(IWindowsFormsEditorService));
				if (null != this.service) 
				{
					this.dialog = new TaskPropertiesDialog();
					if (value != null)
						this.dialog.TaskProperties.LoadXml((string)value);
					if (this.service.ShowDialog(this.dialog) == DialogResult.OK)
					{
						value = this.dialog.TaskProperties.OuterXml;
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
	public class TaskPropertiesConverter : System.ComponentModel.StringConverter 
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
				if ((string)value == string.Empty)
					return string.Empty;			   
				XmlDocument configXml = new XmlDocument();
				configXml.LoadXml((string)value);
				XmlNode taskname = configXml.SelectSingleNode("/task/qualifiedname") ;
				string displayname = taskname.InnerText;
				return displayname.Substring(0, displayname.IndexOf(",",0));
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}


} 