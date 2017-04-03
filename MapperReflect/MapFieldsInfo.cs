using System;
using System.Reflection;

namespace MapperReflect
{
    public class MapFieldsInfo
    {
        public Type src { get; }
        public Type dst { get; }
        public FieldInfo[] srcFieldInfo { get; }
        public FieldInfo[] dstFieldInfo { get; }
     
        public MapFieldsInfo(Type klasssrc, Type klassdst)
        {
            src = klasssrc;
            dst = klassdst;
            srcFieldInfo = klasssrc.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            dstFieldInfo = klassdst.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
        }

    }
}