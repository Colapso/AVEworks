using System;
using System.Reflection;

namespace MapperReflect
{
    public abstract class Mapping
    {

        public static Mapping Properties = new MappingPropreties();
        public static Mapping Fields = new MappingFields();

        public Type srcType { get; set; }
        public Type dstType { get; set; }

        public Mapping()
        {

        }

        public abstract object getMappedObject(object src);

        public abstract void MatchAttrib(string nameFrom, string nameDest);
    }
}