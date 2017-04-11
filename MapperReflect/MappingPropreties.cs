using System;
using System.Collections.Generic;
using System.Reflection;

namespace MapperReflect
{
    public class MappingPropreties : Mapping
    {
        public MapPropertiesInfo properties { get; set; }
        Dictionary<string, MapPropertiesInfo> allFields = new Dictionary<string, MapPropertiesInfo>();

        public MappingPropreties()
        {
        }

        public MappingPropreties(Type klasssrc, Type klassdst)
        {
            this.srcType = klasssrc;
            this.dstType = klassdst;
            MapPropertiesInfo properties = new MapPropertiesInfo(klasssrc, klassdst);
            allFields.Add(klasssrc.Name, properties);
            allFields[srcType.Name].correspondentIndex();

        }

        public override object getMappedObject(object src)
        {
            MapPropertiesInfo value;
            List<int[]> listOfFields = allFields[src.GetType().Name].listOfProperties;

            object ret = Activator.CreateInstance(dstType);

            value = allFields[src.GetType().Name];

            foreach (int[] indexs in listOfFields)
            {
                int indexOfSrcFields = indexs[0];
                int indexOfDstFields = indexs[1];
                

                if (value.dstPropertyInfo[indexOfDstFields].GetType().Equals(value.srcPropertyInfo[indexOfSrcFields].GetType()))
                    value.dstPropertyInfo[indexOfDstFields].SetValue(ret, value.srcPropertyInfo[indexOfSrcFields].GetValue(src));
                else
                {
                    value.dstPropertyInfo[indexOfDstFields].SetValue(ret, AutoMapper.Build(value.dstPropertyInfo[indexOfDstFields].GetType(), value.srcPropertyInfo[indexOfSrcFields].GetType())
                        .Bind(Mapping.Properties).Map(value.srcPropertyInfo[indexOfSrcFields].GetValue(src)));
                }

            }

            return ret;
        }

        public override void MatchAttrib(string nameFrom, string nameDest)
        {


            allFields[srcType.Name].addCorrespondentIndex(nameFrom, nameDest);
        }
           
                
        public override void fillDictionary()
        {
            if (!allFields.ContainsKey(srcType.Name)) { 
                allFields.Add(srcType.Name, new MapPropertiesInfo(srcType,dstType));
                allFields[srcType.Name].correspondentIndex();
            }
        }
    }
}