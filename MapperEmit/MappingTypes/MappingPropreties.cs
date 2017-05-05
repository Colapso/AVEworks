using System;
using System.Collections.Generic;
using System.Reflection;

namespace MapperEmit
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
            properties.correspondentIndex();

        }

        public override object getMappedObject(object src)
        {
            List<MatchInfo> listOfFields = properties.listOfProperties;

            object ret = Activator.CreateInstance(dstType);

            foreach (MatchInfo indexs in listOfFields)
            {
                int indexOfSrcProperties = indexs.SrcIdx;
                int indexOfDstProperties = indexs.DstIdx;


                if (indexs.MapperAux == null)
                {
                    properties.dstPropertyInfo[indexOfDstProperties].SetValue(ret, properties.srcPropertyInfo[indexOfSrcProperties].GetValue(src));
                }
                else
                {
                    if (properties.srcPropertyInfo[indexOfSrcProperties].PropertyType.IsArray && properties.dstPropertyInfo[indexOfDstProperties].PropertyType.IsArray)
                    {
                        object[] srcO = (object[])properties.srcPropertyInfo[indexOfSrcProperties].GetValue(src);

                        object ins = indexs.MapperAux.Map(srcO);

                        properties.dstPropertyInfo[indexOfDstProperties].SetValue(ret, ins);
                    }
                    else
                    {
                        object srcO = properties.srcPropertyInfo[indexOfSrcProperties].GetValue(src);

                        object ins = indexs.MapperAux.Map(srcO);

                        properties.dstPropertyInfo[indexOfDstProperties].SetValue(ret, ins);
                    }
                }

            }

            return ret;
        }

        public override void MatchAttrib(string nameFrom, string nameDest)
        {
            properties.addCorrespondentIndex(nameFrom, nameDest);
        }
           
                
        public override void actualizeInfo()
        {
            if (properties == null)
                properties = new MapPropertiesInfo(srcType, dstType);
            properties.correspondentIndex();
        }
    }
}