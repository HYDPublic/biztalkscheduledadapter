using System;
using System.ComponentModel;
using System.Data.OracleSqlClient;
using System.IO;
using System.Text;
using System.Transactions;
using System.Xml;

namespace ScheduledTaskAdapter.TaskComponents
{
   
    [Serializable()]
    public class OracleSqlArguments
    {
        private string connectionString = null;
        private string SqlCommand = null;
        private string targetNamespace = null;
        private string rootElementName = null;
        private string SqlCommandTimeout = null;

        [Description("The connection string used to connect to a OracleSql database"),
        CategoryAttribute("OracleSql Configuration"),
        EditorAttribute("ScheduledTaskAdapter.Admin.DatalinkUITypeEditor, ScheduledTaskAdapter.Admin, Version=5.0.0.3, Culture=neutral, PublicKeyToken=aa9f2dd0f13442dc", typeof(System.Drawing.Design.UITypeEditor))]
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        [Description("The select statement or stored procedure used when polling OracleSql Server for data."),
        CategoryAttribute("OracleSql Configuration")]
        public string SqlCommand
        {
            get { return SqlCommand; }
            set { SqlCommand = value; }
        }

        [Description("Wait time (in seconds) before terminating the attempt to execute a OracleSql Command and generating an error."),
        CategoryAttribute("OracleSql Configuration")]
        public string SqlCommandTimeout
        {
            get { return SqlCommandTimeout; }
            set { SqlCommandTimeout = value; }
        }

        [Description("The target namespace used in the XML document received from OracleSql Server."),
        CategoryAttribute("Document Specifications")]
        public string TargetNamespace
        {
            get { return targetNamespace; }
            set { targetNamespace = value; }
        }
        [Description("Document root element name that will be used in the XML document received from OracleSql Server."),
        CategoryAttribute("Document Specifications")]
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
    public class OracleSqlStreamProvider : IScheduledTaskStreamProvider2
    {
        
        public OracleSqlStreamProvider()
        { }

        public System.Type GetParameterType()
        {
            return typeof(OracleSqlArguments);
        }

        public Stream GetStreams(object parameter, out CommittableTransaction transaction)
        {
            OracleSqlArguments args = (OracleSqlArguments)parameter;
            if (string.IsNullOrEmpty(args.ConnectionString))
                throw new ArgumentException("OracleSqlStreamProvider requires connection string");
            
            int parsedTimeout;
            if (!int.TryParse(args.SqlCommandTimeout, out parsedTimeout))
                parsedTimeout = 30;

            //  Create the System.Transactions transaction
            transaction = new CommittableTransaction();
            try
            {
                MemoryStream memoryStream = new MemoryStream();

               
                    using (OracleConnection connection = new OracleConnection(args.ConnectionString))
                    {
                        connection.Open();
                        OracleCommand command = new OracleCommand(args.OracleCommand, connection);
                        command.CommandTimeout = parsedTimeout;

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
