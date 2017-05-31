using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

using Microsoft.BizTalk.Admin;

namespace ScheduledTaskAdapter.Admin
{
    [ComVisible(true), Guid("5C5E388C-2C32-406b-A2B0-287CCAD384F0")]
    public class ScheduledPropertyPageFrame: IPropertyPageFrame, IPersistPropertyBag, Microsoft.BizTalk.ExplorerOM.IPersistPropertyBag
    {       
        private AdapterConfiguration adapterConfiguration;
        private string title = null;

        public AdapterConfiguration AdapterConfiguration
        {
            get
            {
                if (this.adapterConfiguration == null)
                    this.adapterConfiguration = new AdapterConfiguration();
                return adapterConfiguration;
            }
            set { adapterConfiguration = value; }
        }

        public ScheduledPropertyPageFrame()
        {            
        }

        #region IPropertyPageFrame Members

        public bool ShowPropertyFrame(IntPtr hwndParent, string pageTitle)
        {
            bool result;
            NativeWindow owner = new NativeWindow();
            try
            {
                owner.AssignHandle(hwndParent);
                using (ScheduledPropertyPageForm form = new ScheduledPropertyPageForm(this))
                {                    
                    form.Text = title != null ? string.Format("{0} Properties",title) : pageTitle ?? string.Empty;
                    result = form.ShowDialog(owner) == DialogResult.OK;
                }
            }
            finally
            {
                owner.ReleaseHandle();
            }
            return result;

        }

        #endregion

        #region Microsoft.BizTalk.Admin.IPersistPropertyBag Members

        public void GetClassID(out Guid classID)
        {
            classID = base.GetType().GUID;
        }

        public void InitNew()
        {
        }

        public void Load(IPropertyBag propertyBag, int errorLog)
        {
            using (new DisposableObjectWrapper(new object[] { propertyBag }))
            {
                if (propertyBag == null)
                {
                    throw new ArgumentNullException("propertyBag");
                }               
                object property = null;
                propertyBag.Read("LocationContext_Name", out property, 0);
                if (property != null)
                {
                    title = (string)property;
                }
                propertyBag.Read("AdapterConfig", out property, 0);
                if (property != null)
                {
                    XmlDocument document = new XmlDocument();
                    document.LoadXml((string)property);
                    this.AdapterConfiguration.Load(document);
                }
            }            
        }

        public void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {                      
            using (new DisposableObjectWrapper(new object[] { propertyBag }))
            {
                if (propertyBag == null)
                {
                    throw new ArgumentNullException("propertyBag");
                }               
                object uri = this.AdapterConfiguration.Uri;
                propertyBag.Write("URI", ref uri);
                object config = this.AdapterConfiguration.Save();
                propertyBag.Write("AdapterConfig", ref config);                
            }            
        }

        #endregion

        #region Microsoft.BizTalk.ExplorerOM.IPersistPropertyBag Members
        public void Load(Microsoft.BizTalk.ExplorerOM.IPropertyBag propertyBag, int errorLog)
        {
            using (new DisposableObjectWrapper(new object[] { propertyBag }))
            {
                if (propertyBag == null)
                {
                    throw new ArgumentNullException("propertyBag");
                }
                object property = null;
                propertyBag.Read("LocationContext_Name", out property, 0);
                if (property != null)
                {
                    title = (string)property;
                }
                propertyBag.Read("AdapterConfig", out property, 0);
                if (property != null)
                {
                    XmlDocument document = new XmlDocument();
                    document.LoadXml((string)property);
                    this.AdapterConfiguration.Load(document);
                }
            }            
        }

        public void Save(Microsoft.BizTalk.ExplorerOM.IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {
            using (new DisposableObjectWrapper(new object[] { propertyBag }))
            {
                if (propertyBag == null)
                {
                    throw new ArgumentNullException("propertyBag");
                }
                object uri = this.AdapterConfiguration.Uri;
                propertyBag.Write("URI", ref uri);
                object config = this.AdapterConfiguration.Save();
                propertyBag.Write("AdapterConfig", ref config);   
            }            
        }

        #endregion
    }
    [ComVisible(false)]
    public class DisposableObjectWrapper : IDisposable
    {
        // Fields
        private object[] _objs;

        // Methods
        public DisposableObjectWrapper(params object[] objs)
        {
            this._objs = objs;
        }

        public void Dispose()
        {
            DisposeObjects(this._objs);
            this._objs = null;
        }

        public static void DisposeObjects(params object[] objs)
        {
            if ((objs != null) && (objs.Length > 0))
            {
                foreach (object obj2 in objs)
                {
                    if (obj2 != null)
                    {
                        IDisposable disposable = obj2 as IDisposable;
                        if (disposable != null)
                        {
                            disposable.Dispose();
                        }
                        if (Marshal.IsComObject(obj2))
                        {
                            Marshal.ReleaseComObject(obj2);
                        }
                    }
                }
            }
        }
    }

 

}
