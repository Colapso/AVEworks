﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace MapperReflect
{
    public class MappingFields : Mapping
    {
        //public MapFieldsInfo fields { get; set; }
        Dictionary<string, MapFieldsInfo> allFields = new Dictionary<string, MapFieldsInfo>();
       

        public MappingFields()
        {
        }

        public MappingFields(Type klasssrc, Type klassdst)
        {
            this.srcType = klasssrc;
            this.dstType = klassdst;
            MapFieldsInfo fields = new MapFieldsInfo(klasssrc, klassdst);
            allFields.Add(klasssrc.Name, fields);
            allFields[srcType.Name].correspondentIndex();

        }


        public override object getMappedObject(object src)
        {
            MapFieldsInfo value;
            List<int[]> listOfFields = allFields[src.GetType().Name].listOfFields;

            object ret = Activator.CreateInstance(dstType);

            value = allFields[src.GetType().Name];
            
            foreach(int[] indexs in listOfFields)
            {
                int indexOfSrcFields = indexs[0];
                int indexOfDstFields = indexs[1];
                object xa;
                

                if (value.dstFieldInfo[indexOfDstFields].FieldType.Equals(value.srcFieldInfo[indexOfSrcFields].FieldType))
                {
                    
                    value.dstFieldInfo[indexOfDstFields].SetValue(ret, value.srcFieldInfo[indexOfSrcFields].GetValue(src));
                }
                else
                {
                    Mapper m = AutoMapper.Build(value.dstFieldInfo[indexOfDstFields].FieldType, value.srcFieldInfo[indexOfSrcFields].FieldType)
                        .Bind(Mapping.Fields);
                    xa = value.srcFieldInfo[indexOfSrcFields].GetValue(src);
                    value.dstFieldInfo[indexOfDstFields].SetValue(ret, m.Map(value.srcFieldInfo[indexOfSrcFields].GetValue(src)));
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
            if(!allFields.ContainsKey(srcType.Name)) { 
                allFields.Add(srcType.Name, new MapFieldsInfo(srcType, dstType));
                allFields[srcType.Name].correspondentIndex();
            }
        }
          }

    
}