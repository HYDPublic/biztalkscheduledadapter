namespace ScheduledTaskAdapter {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [SchemaType(SchemaTypeEnum.Property)]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"TaskName", @"NextScheduleTime"})]
    public sealed class bts_scheduledtask_properties : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" xmlns=""http://schemas.custom.com/Biztalk/2003/scheduledtask-properties"" targetNamespace=""http://schemas.custom.com/Biztalk/2003/scheduledtask-properties"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:annotation>
    <xs:appinfo>
      <b:schemaInfo schema_type=""property"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" />
    </xs:appinfo>
  </xs:annotation>
  <xs:element name=""TaskName"" type=""xs:string"">
    <xs:annotation>
      <xs:appinfo>
        <b:fieldInfo propertyGuid=""b2b94bc2-4ba9-40b8-a589-843edfcf2dc9"" propSchFieldBase=""MessageContextPropertyBase"" />
      </xs:appinfo>
    </xs:annotation>
  </xs:element>
  <xs:element name=""NextScheduleTime"" type=""xs:dateTime"">
    <xs:annotation>
      <xs:appinfo>
        <b:fieldInfo propertyGuid=""338b9c48-6c28-4643-a6a6-bab82eb6306d"" propSchFieldBase=""MessageContextPropertyBase"" />
      </xs:appinfo>
    </xs:annotation>
  </xs:element>
</xs:schema>";
        
        public bts_scheduledtask_properties() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [2];
                _RootElements[0] = "TaskName";
                _RootElements[1] = "NextScheduleTime";
                return _RootElements;
            }
        }
        
        protected override object RawSchema {
            get {
                return _rawSchema;
            }
            set {
                _rawSchema = value;
            }
        }
    }
    
    [System.SerializableAttribute()]
    [PropertyType(@"TaskName",@"http://schemas.custom.com/Biztalk/2003/scheduledtask-properties","string","System.String")]
    [PropertyGuidAttribute(@"b2b94bc2-4ba9-40b8-a589-843edfcf2dc9")]
    public sealed class TaskName : Microsoft.XLANGs.BaseTypes.MessageContextPropertyBase {
        
        [System.NonSerializedAttribute()]
        private static System.Xml.XmlQualifiedName _QName = new System.Xml.XmlQualifiedName(@"TaskName", @"http://schemas.custom.com/Biztalk/2003/scheduledtask-properties");
        
        private static string PropertyValueType {
            get {
                throw new System.NotSupportedException();
            }
        }
        
        public override System.Xml.XmlQualifiedName Name {
            get {
                return _QName;
            }
        }
        
        public override System.Type Type {
            get {
                return typeof(string);
            }
        }
    }
    
    [System.SerializableAttribute()]
    [PropertyType(@"NextScheduleTime",@"http://schemas.custom.com/Biztalk/2003/scheduledtask-properties","dateTime","System.DateTime")]
    [PropertyGuidAttribute(@"338b9c48-6c28-4643-a6a6-bab82eb6306d")]
    public sealed class NextScheduleTime : Microsoft.XLANGs.BaseTypes.MessageContextPropertyBase {
        
        [System.NonSerializedAttribute()]
        private static System.Xml.XmlQualifiedName _QName = new System.Xml.XmlQualifiedName(@"NextScheduleTime", @"http://schemas.custom.com/Biztalk/2003/scheduledtask-properties");
        
        private static System.DateTime PropertyValueType {
            get {
                throw new System.NotSupportedException();
            }
        }
        
        public override System.Xml.XmlQualifiedName Name {
            get {
                return _QName;
            }
        }
        
        public override System.Type Type {
            get {
                return typeof(System.DateTime);
            }
        }
    }
}
