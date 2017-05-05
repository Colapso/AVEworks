using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MapperReflect
{

    public class MapFieldsInfo : MappingFields
    {
        public Type src { get; }
        public Type dst { get; }
        public FieldInfo[] srcFieldInfo { get; }
        public FieldInfo[] dstFieldInfo { get; }
        public List<MatchInfo> listOfFields = new List<MatchInfo>(); 

        public MapFieldsInfo(Type klasssrc, Type klassdst)
        {
            src = klasssrc;
            dst = klassdst;
            srcFieldInfo = klasssrc.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            dstFieldInfo = klassdst.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
        }

        private void AddField(int srci, int dsti)
        {
            MatchInfo m = new MatchInfo(srci, dsti);

            Type src = srcFieldInfo[srci].FieldType;
            Type dst = dstFieldInfo[dsti].FieldType;

            if (src.IsArray && dst.IsArray)
            {
                src = src.GetElementType();
                dst = dst.GetElementType();
            }

            if (!IsPrimitiveType(src))
            {
                m.MapperAux = AutoMapper.Build(src, dst).Bind(new MappingFields());
            }

            listOfFields.Add(m);
        }

        public void correspondentIndex()
        {
            for(int i = 0; i < srcFieldInfo.Length;i++ )
            {
                for (int k = 0; k < dstFieldInfo.Length; k++)
                {
                    if (srcFieldInfo[i].FieldType.Equals(dstFieldInfo[k].FieldType))
                    {
                        if (srcFieldInfo[i].Name.Equals(dstFieldInfo[k].Name))
                        {
                            AddField(i, k);
                        }
                    }

                }

            }
        }

        public void addCorrespondentIndex(string nameFrom, string nameDest)
        {

            for (int i = 0; i < srcFieldInfo.Length; i++)
            {
                string name = srcFieldInfo[i].Name;

                if(name.Contains("<") && name.Contains(">"))
                    name = name.Split('<')[1].Split('>')[0];

                for (int k = 0; k < dstFieldInfo.Length; k++)
                {
                    string name2 = dstFieldInfo[k].Name;

                    if (name2.Contains("<") && name2.Contains(">"))
                        name2 = name2.Split('<')[1].Split('>')[0];
                    
                    if (name.Equals(nameFrom) && name2.Equals(nameDest))
                    {
                        AddField(i, k);
                        return;
                    }
                }

            }

        }
    }
}
