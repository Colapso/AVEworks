using System;
using System.Reflection;

namespace MapperReflect
{
    public class MappingPropreties : Mapping
    {
        public MapPropertiesInfo properties { get; set; }
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
            if (properties == null)
                properties = new MapPropertiesInfo(srcType, dstType);

            object ret = Activator.CreateInstance(properties.dst);
            foreach (PropertyInfo i in properties.srcPropertyInfo)
            {
                foreach (PropertyInfo k in properties.dstPropertyInfo)
                {
                    if (i.PropertyType.Equals(k.PropertyType))
                    {
                        if (i.Name.Equals(k.Name))
                            k.SetValue(ret, i.GetValue(src));
                    }
                }
            }
            return ret;
        }

        public override void MatchAttrib(string nameFrom, string nameDest)
        {
            throw new NotImplementedException();
        }
    }
}