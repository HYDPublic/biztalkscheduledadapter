using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Transactions;
using System.Xml;

namespace ScheduledTaskAdapter.TaskComponents
{
   
    [Serializable()]
    public class SQLArguments
    {
        private string connectionString = null;
        private string sqlCommand = null;
        private string targetNamespace = null;
        private string rootElementName = null;


        [Description("Connection String"),
        EditorAttribute("ScheduledTaskAdapter.Admin.DatalinkUITypeEditor, ScheduledTaskAdapter.Admin, Version=3.0.0.0, Culture=neutral, PublicKeyToken=aa9f2dd0f13442dc", typeof(System.Drawing.Design.UITypeEditor))]
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

         [Description("SQL Command")]
        public string SQLCommand
        {
            get { return sqlCommand; }
            set { sqlCommand = value; }
        }

        [Description("Target Namespace")]
        public string TargetNamespace
        {
            get { return targetNamespace; }
            set { targetNamespace = value; }
        }
        [Description("Root Element Name")]
        public string RootElementName
        {
            get { return rootElementName; }
            set { rootElementName = value; }
        }
    }

    /// <summary>
    /// XmlStringStreamProvider: implements the IScheduledTaskStreamProvider interface.
    /// returns the configured Xml String to the  ScheduledTask Adapter as a stream
    /// </summary>
    public class SQLStreamProvider : IScheduledTaskStreamProvider2
    {
        
        public SQLStreamProvider()
        { }

        public System.Type GetParameterType()
        {
            return typeof(SQLArguments);
        }

        public Stream GetStreams(object parameter, out CommittableTransaction transaction)
        {
            SQLArguments args = (SQLArguments)parameter;
            if (string.IsNullOrEmpty(args.ConnectionString))
                throw new ArgumentException("SQLStreamProvider requires connection string");

            //  Create the System.Transactions transaction
            transaction = new CommittableTransaction();
            try
            {
                MemoryStream memoryStream = new MemoryStream();

                using (TransactionScope ts = new TransactionScope(transaction, TimeSpan.FromHours(1), EnterpriseServicesInteropOption.Full))
                {
                    using (SqlConnection connection = new SqlConnection(args.ConnectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(args.SQLCommand, connection);

                        XmlWriterSettings settings = new XmlWriterSettings();
                        settings.Encoding = Encoding.Unicode;
                        settings.Indent = false;
                        settings.OmitXmlDeclaration = false;

                        using (XmlWriter writer = XmlTextWriter.Create(memoryStream, settings))
                        {
                            writer.WriteStartDocument();
                            writer.WriteStartElement(args.RootElementName, args.TargetNamespace);
                            using (XmlReader reader = command.ExecuteXmlReader())
                            {
                                while (!reader.EOF)
                                {
                                    writer.WriteNode(reader, true);
                                }
                            }
                            writer.WriteEndElement();
                            writer.Flush();
                        }                       
                    }
                    //  An exception will have skipped this next line
                    ts.Complete();
                }
                memoryStream.Seek(0, SeekOrigin.Begin);
                return memoryStream;
            }
            catch
            {               
                if (transaction != null)
                    transaction.Rollback();
                throw;
            }
        }

        public void Done(bool success)
        {
        }
    }
}
