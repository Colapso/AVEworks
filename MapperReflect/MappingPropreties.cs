using System;
using System.Collections.Generic;
using System.Reflection;

namespace MapperReflect
{
    public class MappingPropreties : Mapping
    {
        public MapPropertiesInfo properties { get; set; }
        Dictionary<string, PropertyInfoSrcDst> allFields = new Dictionary<string, PropertyInfoSrcDst>();

        public MappingPropreties()
        {
        }

        public MappingPropreties(Type klasssrc, Type klassdst)
        {
            this.srcType = klasssrc;
            this.dstType = klassdst;
            properties = new MapPropertiesInfo(klasssrc, klassdst);

        }

        public override object getMappedObject(object src)
        {


            object ret = Activator.CreateInstance(properties.dst);
            foreach (string key in allFields.Keys)
            {
                PropertyInfoSrcDst value = allFields[key];
                value.dst.SetValue(ret, value.src.GetValue(src));
            }
            return ret;
        }

        public override void MatchAttrib(string nameFrom, string nameDest)
        {
            foreach (PropertyInfo i in properties.srcPropertyInfo)
            {
                String name = i.Name.Split('<')[1].Split('>')[0];
                foreach (PropertyInfo k in properties.dstPropertyInfo)
                {
                    String name2 = k.Name.Split('<')[1].Split('>')[0];
                    if (name.Equals(nameFrom) && name2.Equals(nameDest))
                    {
                        allFields.Add(nameDest, new PropertyInfoSrcDst(i, k));
                    }

                }
            }
        }

        internal override void fillDictionary(Type klassSrc, Type klassDest)
        {
            properties = new MapPropertiesInfo(klassSrc, klassDest);
            foreach (PropertyInfo i in properties.srcPropertyInfo)
            {
                foreach (PropertyInfo k in properties.dstPropertyInfo)
                {
                    if (i.PropertyType.Equals(k.PropertyType))
                    {
                        if (i.Name.Equals(k.Name))
                            allFields.Add(k.Name, new PropertyInfoSrcDst(i, k));
                    }
                }
            }
        }
    }
}