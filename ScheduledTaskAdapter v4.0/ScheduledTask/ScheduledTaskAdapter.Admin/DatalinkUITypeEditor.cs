using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Security.Permissions;
using Microsoft.Data.ConnectionUI;

namespace ScheduledTaskAdapter.Admin
{
    public class DatalinkUITypeEditor : OpenFileUITypeEditor
    {
        // Fields        
        private IWindowsFormsEditorService service;
        private DataLinkDialog dialog = new DataLinkDialog();
        // Methods
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
                this.service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (this.service != null)
                {                    
                    string connectionString = value as string;                    
                    this.dialog.ConnectionString = string.Concat("Provider=SQLOLEDB.1;", connectionString);
                    
                    LaunchForm launch = new LaunchForm(this.dialog);
                    DialogResult result = this.service.ShowDialog(launch);
                    if (result == DialogResult.OK)
                    {
                        value = this.dialog.ConnectionString.Replace("Provider=SQLOLEDB.1;", "");
                    }
                }
            }
            return value;
        }        
    }
}
