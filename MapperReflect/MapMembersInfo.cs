using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperReflect
{
    class MapMembersInfo 
    {
        
        public Type src { get; }
        public Type dst { get; }
        public Type toMap { get; }
        public List<int[]> listOfMembers = new List<int[]>();
        public MemberInfo[] srcMemberInfo { get; }
        public MemberInfo[] dstMemberInfo { get; }

        public MapMembersInfo(Type klasstoMap, Type klassSrc, Type klassDst)
        {
            toMap = klasstoMap;
            dst = klassDst;
            src = klassSrc;
            srcMemberInfo = src.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            dstMemberInfo = dst.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public void correspondentIndex()
        {


            for (int i = 0; i < srcMemberInfo.Length; i++)
            {
                ToMapAttribute toMapAttrSrc = (ToMapAttribute)srcMemberInfo[i].GetCustomAttribute(typeof(ToMapAttribute));
                if (toMapAttrSrc != null)
                {
                    for (int k = 0; k < dstMemberInfo.Length; k++)
                    {
                        ToMapAttribute toMapAttrDst = (ToMapAttribute)dstMemberInfo[i].GetCustomAttribute(typeof(ToMapAttribute));
                        if (toMapAttrDst != null)
                        {
                            if (srcMemberInfo[i].MemberType.Equals(dstMemberInfo[k].MemberType))
                            {
                                if (srcMemberInfo[i].Name.Equals(dstMemberInfo[k].Name))
                                    listOfMembers.Add(new int[] { i, k });
                            }
                        }
                    }
                }

            }
        }

        public void addCorrespondentIndex(string nameFrom, string nameDest)
        {

            for (int i = 0; i < srcMemberInfo.Length; i++)
            {
                String name = srcMemberInfo[i].Name.Split('<')[1].Split('>')[0];
                for (int k = 0; k < dstMemberInfo.Length; k++)
                {
                    String name2 = dstMemberInfo[k].Name.Split('<')[1].Split('>')[0];
                    if (name.Equals(nameFrom) && name2.Equals(nameDest))
                    {

                        listOfMembers.Add(new int[] { i, k });
                        return;
                    }
                }

            }

        }

    }
}
