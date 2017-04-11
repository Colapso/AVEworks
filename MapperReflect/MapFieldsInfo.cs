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
        public List<int[]> listOfFields = new List<int[]>(); 

        public MapFieldsInfo(Type klasssrc, Type klassdst)
        {
            src = klasssrc;
            dst = klassdst;
            srcFieldInfo = klasssrc.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            dstFieldInfo = klassdst.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
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
                            listOfFields.Add(new int[] { i, k });
                    }

                }

            }
        }

        public void addCorrespondentIndex(string nameFrom,string nameDest)
        {

            for (int i = 0; i < srcFieldInfo.Length; i++)
            {
                String name = srcFieldInfo[i].Name.Split('<')[1].Split('>')[0];
                for (int k = 0; k < dstFieldInfo.Length; k++)
                {
                    String name2 = dstFieldInfo[k].Name.Split('<')[1].Split('>')[0];
                    if (name.Equals(nameFrom) && name2.Equals(nameDest)) { 
                    
                        listOfFields.Add(new int[] { i, k });
                        return;
                    }
                }

            }
            
        }

    
    }
}
