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
        public List<MatchInfo> listOfMembers = new List<MatchInfo>();
        public MemberInfo[] srcMemberInfo { get; }
        public MemberInfo[] dstMemberInfo { get; }


        public MapMembersInfo(Type klasstoMap, Type klassSrc, Type klassDst)
        {
            toMap = klasstoMap;
            dst = klassDst;
            src = klassSrc;

            dstMemberInfo = dst.GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            srcMemberInfo = src.GetMembers(BindingFlags.Public  | BindingFlags.Instance| BindingFlags.NonPublic);
            
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
                                    AddMember(i, k);
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
                string name = srcMemberInfo[i].Name.Split('<')[1].Split('>')[0];
                for (int k = 0; k < dstMemberInfo.Length; k++)
                {
                    string name2 = dstMemberInfo[k].Name.Split('<')[1].Split('>')[0];
                    if (name.Equals(nameFrom) && name2.Equals(nameDest))
                    {
                        AddMember(i, k);
                        return;
                    }
                }

            }
        }
           
        private void AddMember(int srci, int dsti)
        {
            MatchInfo m = new MatchInfo(srci, dsti);

            Type src = GetUnderlyingType(srcMemberInfo[srci]);
            Type dst = GetUnderlyingType(dstMemberInfo[dsti]);
           
            if (src.IsArray && dst.IsArray)
            {
                src = src.GetElementType();
                dst = dst.GetElementType();
            }

            if (!IsPrimitiveType(src))
            {
                m.MapperAux = AutoMapper.Build(src, dst).Bind(new MappingFields());
            }

            listOfMembers.Add(m);
        }

        public Type GetUnderlyingType(MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Event:
                    return ((EventInfo)member).EventHandlerType;
                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;
                case MemberTypes.Method:
                    return ((MethodInfo)member).ReturnType;
                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;
                case MemberTypes.Constructor:
                    return ((ConstructorInfo)member).GetType();
                default:
                    throw new ArgumentException
                    (
                     "Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo"
                    );
            }
        }

        public static bool IsPrimitiveType(Type fieldType)
        {
            return fieldType.IsPrimitive || fieldType.Namespace.Equals("System");
        }


    }
}
