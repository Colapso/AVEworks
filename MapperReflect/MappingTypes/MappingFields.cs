using System;
using System.Collections.Generic;
using System.Reflection;

namespace MapperReflect
{
    public class MappingFields : Mapping
    {
        public MapFieldsInfo allFields { get; set; }

        public MappingFields()
        {
        }

        public MappingFields(Type klasssrc, Type klassdst)
        {
            this.srcType = klasssrc;
            this.dstType = klassdst;
            allFields = new MapFieldsInfo(klasssrc, klassdst);
            allFields.correspondentIndex();

        }


        public override object getMappedObject(object src)
        {
            List<MatchInfo> listOfFields = this.allFields.listOfFields;

            object ret = Activator.CreateInstance(dstType);


            
            foreach(MatchInfo indexs in listOfFields)
            {
                int indexOfSrcFields = indexs.SrcIdx;
                int indexOfDstFields = indexs.DstIdx;
                
                if (indexs.MapperAux==null)
                {
                    allFields.dstFieldInfo[indexOfDstFields].SetValue(ret, allFields.srcFieldInfo[indexOfSrcFields].GetValue(src));
                }
                else
                {
                    if (allFields.srcFieldInfo[indexOfSrcFields].FieldType.IsArray && allFields.dstFieldInfo[indexOfDstFields].FieldType.IsArray)
                    {
                        object[] srcO = (object[])allFields.srcFieldInfo[indexOfSrcFields].GetValue(src);

                        object ins = indexs.MapperAux.Map(srcO);
                        
                        allFields.dstFieldInfo[indexOfDstFields].SetValue(ret, ins);
                    }
                    else
                    {
                        object srcO = allFields.srcFieldInfo[indexOfSrcFields].GetValue(src);

                        object ins = indexs.MapperAux.Map(srcO);

                        allFields.dstFieldInfo[indexOfDstFields].SetValue(ret, ins);
                    }
                }
            }
            
            return ret;
        }

        public override void MatchAttrib(string nameFrom, string nameDest)
        {
            allFields.addCorrespondentIndex(nameFrom, nameDest);
        }

        public override void actualizeInfo()
        {
            if(allFields==null)
                allFields = new MapFieldsInfo(srcType, dstType);
            allFields.correspondentIndex();
        }
    }

    
}