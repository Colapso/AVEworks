using System;
using System.Collections.Generic;
using System.Reflection;

namespace MapperReflect
{
    public class MappingFields : Mapping
    {
        class FieldInfoSrcDst
        {
            public FieldInfo src { get; set; }
            public FieldInfo dst { get; set; }
            public FieldInfoSrcDst(FieldInfo src, FieldInfo dst)
            {
                this.src = src;
                this.dst = dst;
            }
        }

        class MapFieldsInfo
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
        MapFieldsInfo fields;
        Dictionary<string, FieldInfoSrcDst> allFields = new Dictionary<string, FieldInfoSrcDst>();
        private object obj,objSrc;

        public MappingFields()
        {
        }

        public MappingFields(Type klasssrc, Type klassdst)
        {
            this.srcType = klasssrc;
            this.dstType = klassdst;
            fields = new MapFieldsInfo(klasssrc, klassdst);

        }

        
        public override object getMappedObject(object src)
        {
            objSrc = src;
 
                

            object ret = Activator.CreateInstance(fields.dst);
            foreach(string key in allFields.Keys)
            {
                FieldInfoSrcDst value = allFields[key];
                value.dst.SetValue(ret, value.src.GetValue(src));
            }
            obj = ret;
            return ret;
        }

        public override void MatchAttrib(string nameFrom, string nameDest)
        {
            foreach (FieldInfo i in fields.srcFieldInfo)
            {
                String name = i.Name.Split('<')[1].Split('>')[0];
                foreach (FieldInfo k in fields.dstFieldInfo)
                {
                    String name2 = k.Name.Split('<')[1].Split('>')[0];
                    if (name.Equals(nameFrom) && name2.Equals(nameDest))
                       {
                           allFields.Add(nameDest, new FieldInfoSrcDst(i, k));
                       }
                    
                }
            }
        }

        internal override void fillDictionary(Type klassSrc, Type klassDest)
        {
            fields = new MapFieldsInfo(klassSrc, klassDest);
            foreach (FieldInfo i in fields.srcFieldInfo)
            {
                foreach (FieldInfo k in fields.dstFieldInfo)
                {
                    if (i.FieldType.Equals(k.FieldType))
                    {
                        if (i.Name.Equals(k.Name))
                        {
                            if (!allFields.ContainsKey(k.Name))
                                allFields.Add(k.Name, new FieldInfoSrcDst(i,k));
                        }
                    }
                }
            }
        }
    }

    
}