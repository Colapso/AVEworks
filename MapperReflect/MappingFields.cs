using System;
using System.Collections.Generic;
using System.Reflection;

namespace MapperReflect
{
    public class MappingFields : Mapping
    {

        public MapFieldsInfo fields { get; set; }
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
                foreach (FieldInfo k in fields.dstFieldInfo)
                {
                       if (i.Name.Equals(nameFrom))
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
                            allFields.Add(k.Name, new FieldInfoSrcDst(i,k));
                        }
                    }
                }
            }
        }
    }

    
}