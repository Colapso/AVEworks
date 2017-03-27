using System;
using System.Reflection;

namespace MapperReflect
{
    public class Mapping
    {
        FieldInfo[] Fields;
        PropertyInfo[] Properties;
        
        public Mapping(Type t)
        {
            Fields = t.GetFields();
            Properties = t.GetProperties();
        }
    }
}