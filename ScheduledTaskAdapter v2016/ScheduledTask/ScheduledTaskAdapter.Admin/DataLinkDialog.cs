using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ScheduledTaskAdapter.Admin
{
    [ComImport, ClassInterface(ClassInterfaceType.None), TypeLibType(TypeLibTypeFlags.FCanCreate), Guid("2206CDB2-19C1-11D1-89E0-00C04FD7A829")]
    public class CDataLink
    {
    }

    [ComImport, Guid("2206CCB1-19C1-11D1-89E0-00C04FD7A829"), CLSCompliant(false), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDataInitialize
    {
        void GetDataSource([MarshalAs(UnmanagedType.Interface)] object pUnkOuter, uint dwClsCtx, string pwszInitializationString, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppDataSource);
        void GetInitializationString([MarshalAs(UnmanagedType.Interface)] object pDataSource, bool fIncludePassword, [MarshalAs(UnmanagedType.LPWStr)] out string ppwszInitString);
    }

    [ComImport, CLSCompliant(false), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("2206CCB0-19C1-11D1-89E0-00C04FD7A829")]
    public interface IDBPromptInitialize
    {
        void PromptDataSource([MarshalAs(UnmanagedType.Interface)] object pUnkOuter, IntPtr hWndParent, uint dwPromptOptions, uint cSourceTypeFilter, int rgSourceTypeFilter, string pwszszzProviderFilter, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] ref object ppDataSource);
    }

    internal class LaunchForm : Form
    {
        // Fields        
        private DataLinkDialog form;
        private NextDialogDelegate nextDialog;
        // Properties
       
        // Nested Types
        private delegate void NextDialogDelegate();

        // Methods
        public LaunchForm(DataLinkDialog form)
        {
            this.form = form;
            this.DialogResult = DialogResult.Cancel;
            //base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            if ((Environment.OSVersion.Version.Major > 5) || ((Environment.OSVersion.Version.Major == 5) && (Environment.OSVersion.Version.Minor > 0)))
            {
                base.Opacity = 0.0;
            }
            else
            {
                base.WindowState = FormWindowState.Minimized;
            }
            this.nextDialog = new NextDialogDelegate(this.NextDialog);
            base.Load += new EventHandler(this.LoadEvent);
        }

        private void LoadEvent(object sender, EventArgs e)
        {
            this.nextDialog.BeginInvoke(null, null);
        }

        private void NextDialog()
        {
            this.DialogResult = this.form.Show(base.Handle);
            base.Close();
        }
    }

    public class DataLinkDialog
    {
        // Fields
        private string connectionString = string.Empty;
        // Properties
        public string ConnectionString
        {
            get { return this.connectionString; }
            set { this.connectionString = value; }
        }

        // Methods
        public DialogResult Show(IntPtr parent)
        {
            try
            {
                CDataLink link = new CDataLink();
                Guid riid = new Guid("0C733A8B-2A1C-11CE-ADE5-00AA0044773D");
                IDataInitialize initialize = (IDataInitialize)link;
                object ppDataSource = null;
                initialize.GetDataSource(null, 1, this.connectionString, ref riid, out ppDataSource);
                ((IDBPromptInitialize)link).PromptDataSource(null, parent, 0x12, 0, 0, null, ref riid, ref ppDataSource);
                initialize.GetInitializationString(ppDataSource, true, out this.connectionString);
            }
            catch (COMException)
            {
                return DialogResult.Cancel;
            }
            return DialogResult.OK;
        }

      
    }


}
