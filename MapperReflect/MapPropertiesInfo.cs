using System;
using System.Reflection;

namespace MapperReflect
{
    public class MapPropertiesInfo
    {
        public Type src { get; }
        public Type dst { get; }
        public PropertyInfo[] srcPropertyInfo { get; }
        public PropertyInfo[] dstPropertyInfo { get; }

        public MapPropertiesInfo(Type klasssrc, Type klassdst)
        {
            src = klasssrc;
            dst = klassdst;
            srcPropertyInfo = klasssrc.GetProperties();
            dstPropertyInfo = klassdst.GetProperties();
        }


    }
}