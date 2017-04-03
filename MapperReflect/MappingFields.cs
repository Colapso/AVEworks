using System;
using System.Collections.Generic;
using System.Reflection;

namespace MapperReflect
{
    public class MappingFields : Mapping
    {

        public MapFieldsInfo fields { get; set; }
        Dictionary<string, FieldInfo> allFields = new Dictionary<string, FieldInfo>();
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
            if(fields==null)
                fields = new MapFieldsInfo(srcType, dstType);

            object ret = Activator.CreateInstance(fields.dst);
            foreach (FieldInfo i in fields.srcFieldInfo)
            {
                foreach (FieldInfo k in fields.dstFieldInfo)
                {
                    if (i.FieldType.Equals(k.FieldType))
                    {
                        if (i.Name.Equals(k.Name))
                        {
                            k.SetValue(ret, i.GetValue(src));
                            allFields.Add(i.Name, k);
                        }
                            
                    }
                }
            }
            obj = ret;
            return ret;
        }

        public override void MatchAttrib(string nameFrom, string nameDest)
        {
            //Não funciona porque o allFields ainda não está construído
            //Solução: Quando é criado o Mapping, construir logo o dicionário (no set dos types)
            //e assim o Match e o Map podem basear-se apenas no dicionário
            foreach (KeyValuePair<string, FieldInfo> entry in allFields)
            {
                if (entry.Value.Name.Equals(nameFrom))
                {
                    allFields.Add(nameDest,entry.Value);
                    return;
                }
            }
            
        }
    }

    
}