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
        public List<MatchInfo> listOfProperties = new List<MatchInfo>();

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
                            AddProperty(i,k);
                    }

                }

            }
        }

        public void addCorrespondentIndex(string nameFrom, string nameDest)
        {

            for (int i = 0; i < srcPropertyInfo.Length; i++)
            {
                string name = srcPropertyInfo[i].Name;

                if (name.Contains("<") && name.Contains(">"))
                    name = name.Split('<')[1].Split('>')[0];

                for (int k = 0; k < dstPropertyInfo.Length; k++)
                {
                    string name2 = dstPropertyInfo[k].Name;

                    if (name2.Contains("<") && name2.Contains(">"))
                        name2 = name2.Split('<')[1].Split('>')[0];

                    if (name.Equals(nameFrom) && name2.Equals(nameDest))
                    {
                        AddProperty(i, k);
                        return;
                    }
                }

            }

        }
   

        private void AddProperty(int srci, int dsti)
        {
            MatchInfo m = new MatchInfo(srci, dsti);

            Type src = srcPropertyInfo[srci].PropertyType;
            Type dst = dstPropertyInfo[dsti].PropertyType;

            if (src.IsArray && dst.IsArray)
            {
                src = src.GetElementType();
                dst = dst.GetElementType();
            }

            if (!IsPrimitiveType(src))
            {
                m.MapperAux = AutoMapper.Build(src, dst).Bind(new MappingPropreties());
            }

            listOfProperties.Add(m);
        }
    }
}