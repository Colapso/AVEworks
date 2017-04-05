using System;
using System.Collections.Generic;
using System.Reflection;

namespace MapperReflect
{
    public class MapPropertiesInfo : MappingPropreties
    {
        public Type src { get; }
        public Type dst { get; }
        public PropertyInfo[] srcPropertyInfo { get; }
        public PropertyInfo[] dstPropertyInfo { get; }
        public List<int[]> listOfProperties = new List<int[]>();

        public MapPropertiesInfo(Type klasssrc, Type klassdst)
        {
            src = klasssrc;
            dst = klassdst;
            srcPropertyInfo = klasssrc.GetProperties();
            dstPropertyInfo = klassdst.GetProperties();
        }

        public void correspondentIndex()
        {


            for (int i = 0; i < srcPropertyInfo.Length; i++)
            {
                for (int k = 0; k < dstPropertyInfo.Length; k++)
                {
                    if (srcPropertyInfo[i].PropertyType.Equals(dstPropertyInfo[k].PropertyType))
                    {
                        if (srcPropertyInfo[i].Name.Equals(dstPropertyInfo[k].Name))
                            listOfProperties.Add(new int[] { i, k });
                    }

                }

            }
        }

        public void addCorrespondentIndex(string nameFrom, string nameDest)
        {

            for (int i = 0; i < srcPropertyInfo.Length; i++)
            {
                String name = srcPropertyInfo[i].Name.Split('<')[1].Split('>')[0];
                for (int k = 0; k < dstPropertyInfo.Length; k++)
                {
                    String name2 = dstPropertyInfo[k].Name.Split('<')[1].Split('>')[0];
                    if (name.Equals(nameFrom) && name2.Equals(nameDest))
                    {

                        listOfProperties.Add(new int[] { i, k });
                        return;
                    }
                }

            }

        }
    }
}