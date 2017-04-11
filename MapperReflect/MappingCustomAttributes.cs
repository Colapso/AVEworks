using System;
using System.Collections.Generic;
using System.Reflection;

namespace MapperReflect
{
    public class MappingCustomAttributes : Mapping
    {
        Dictionary<string, MapMembersInfo> allcustomAttributes = new Dictionary<string, MapMembersInfo>();
        Type toMapAttributeType;

        public MappingCustomAttributes(Type type)
        {
            toMapAttributeType = type;
        }
       

        
        public override object getMappedObject(object src)
        {
            MapMembersInfo value;
            List<int[]> listOfFields = allcustomAttributes[src.GetType().Name].listOfMembers;

            object ret = Activator.CreateInstance(dstType);

            value = allcustomAttributes[src.GetType().Name];

            foreach (int[] indexs in listOfFields)
            {
                int indexOfSrcFields = indexs[0];
                int indexOfDstFields = indexs[1];

                

                if (value.dstMemberInfo[indexOfDstFields].GetType().Equals(value.srcMemberInfo[indexOfSrcFields].GetType()))

                    SetMemberValue(value.dstMemberInfo[indexOfDstFields], ret, GetMemberValue(value.srcMemberInfo[indexOfSrcFields], src));
                else
                {
                   // value.dstMemberInfo[indexOfDstFields].SetValue(ret, AutoMapper.Build(value.dstMemberInfo[indexOfDstFields].GetType(), value.srcMemberInfo[indexOfSrcFields].GetType())
                      //  .Bind(Mapping.Properties).Map(value.srcMemberInfo[indexOfSrcFields].GetValue(src)));
                }
            }

            return ret;
        }

        public void SetMemberValue(MemberInfo member, object target, object value)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    ((FieldInfo)member).SetValue(target, value);
                    break;
                case MemberTypes.Property:
                    ((PropertyInfo)member).SetValue(target, value, null);
                    break;
                default:
                    throw new ArgumentException("MemberInfo must be if type FieldInfo or PropertyInfo", "member");
            }
        }

        public object GetMemberValue(MemberInfo member, object target)
        {
            object ret;

            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    ret = ((FieldInfo)member).GetValue(target);
                    break;
                case MemberTypes.Property:
                    ret = ((PropertyInfo)member).GetValue(target);
                    break;
                default:
                    throw new ArgumentException("MemberInfo must be if type FieldInfo or PropertyInfo", "member");
            }
            return ret;
        }

        public override void MatchAttrib(string nameFrom, string nameDest)
        {
            allcustomAttributes[srcType.Name].addCorrespondentIndex(nameFrom, nameDest);
        }

        public override void fillDictionary()
        {
            if (!allcustomAttributes.ContainsKey(srcType.Name))
            {
                allcustomAttributes.Add(srcType.Name, new MapMembersInfo(toMapAttributeType, srcType, dstType));
                allcustomAttributes[srcType.Name].correspondentIndex();
            }
        }
    }
}