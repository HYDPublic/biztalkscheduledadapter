using System;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ScheduledTaskAdapter.TaskComponents
{
    public class Task
    {
        private Type taskType = null;        
        private object taskParameters = null;

        public Type TaskType
        {
            get { return taskType; }
            set { taskType = value; } 
        }
        
        public object TaskParameters
        {
            get { return taskParameters; }
            set { taskParameters = value; }
        }

        public Task()
        {
        }

        public bool SetType(Type taskType)
        {
            try
            {
                if (this.taskType != taskType)
                {
                    Assembly assembly = taskType.Assembly;
                    object provider = assembly.CreateInstance(taskType.FullName);
                    object[] args = new object[] { };
                    Type parameterType = (Type)taskType.InvokeMember("GetParameterType", BindingFlags.InvokeMethod, null, provider, args);
                    object parameters = null;
                    if (parameterType != null)
                    {
                        parameters = assembly.CreateInstance(parameterType.FullName);
                    }
                    this.taskType = taskType;                    
                    this.taskParameters = parameters;
                    return true;           
                }                
            }
            catch { }
            return false;
        }
       
        public static void Serialize(XmlWriter writer, Task task)
        {
            writer.WriteStartElement("task");
            writer.WriteElementString("qualifiedname", task.TaskType.AssemblyQualifiedName);
            if (task.TaskParameters != null)
            {
                XmlSerializer serializer = new XmlSerializer(task.TaskParameters.GetType());
                serializer.Serialize(writer, task.TaskParameters);
            }
            writer.WriteEndElement();
        }

        public static Task Deserialize(XmlReader reader)
        {
            Task task = null;            
            reader.ReadStartElement("task");
            reader.ReadStartElement("qualifiedname");
                             
            string qualifiedName = reader.ReadString();
            reader.ReadEndElement();

            Type taskType = Type.GetType(qualifiedName);
            Assembly assembly = taskType.Assembly;
            object provider = assembly.CreateInstance(taskType.FullName);
            object[] args = new object[] { };
            Type parameterType = (Type)taskType.InvokeMember("GetParameterType", BindingFlags.InvokeMethod, null, provider, args);
            
            XmlSerializer serializer = new XmlSerializer(parameterType);
            object taskParameters = serializer.Deserialize(reader);
            task = new Task();
            task.taskType = taskType;
            task.taskParameters = taskParameters;
           
            reader.ReadEndElement();
            return task;
        }
    }
}
