using System;
using System.Collections.Generic;
using System.Reflection;

namespace MapperEmit
{
    public class MappingCustomAttributes : Mapping
    {
        MapMembersInfo allcustomAttributes ;
        Type toMapAttributeType;

        public MappingCustomAttributes()
        {

        }

        public MappingCustomAttributes(Type type)
        {
            toMapAttributeType = type;
            
        }
       

        
        public override object getMappedObject(object src)
        {
            allcustomAttributes = new MapMembersInfo(toMapAttributeType, srcType, dstType);
            allcustomAttributes.correspondentIndex();
            List<MatchInfo> listOfMembers = allcustomAttributes.listOfMembers;

            object ret = Activator.CreateInstance(dstType);


            foreach (MatchInfo indexs in listOfMembers)
            {
                int indexOfSrcFields = indexs.SrcIdx;
                int indexOfDstFields = indexs.DstIdx;

                
                if (indexs.MapperAux == null)
                {
                    SetMemberValue(allcustomAttributes.dstMemberInfo[indexOfDstFields], ret, GetMemberValue(allcustomAttributes.srcMemberInfo[indexOfSrcFields], src));
                }
                else
                {
                    if (GetUnderlyingType(allcustomAttributes.srcMemberInfo[indexOfSrcFields]).IsArray && GetUnderlyingType(allcustomAttributes.dstMemberInfo[indexOfDstFields]).IsArray)
                    {
                        object[] srcO = (object[])GetMemberValue(allcustomAttributes.srcMemberInfo[indexOfSrcFields], src);

                        object ins = indexs.MapperAux.Map(srcO);

                        SetMemberValue(allcustomAttributes.dstMemberInfo[indexOfDstFields], ret, ins);
                    }
                    else
                    {
                        object srcO = GetMemberValue(allcustomAttributes.srcMemberInfo[indexOfSrcFields], src);

                        object ins = indexs.MapperAux.Map(srcO);

                        SetMemberValue(allcustomAttributes.dstMemberInfo[indexOfDstFields], ret, ins);
                    }
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
                default:
                    throw new ArgumentException
                    (
                     "Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo"
                    );
            }
        }

        public override void MatchAttrib(string nameFrom, string nameDest)
        {
            allcustomAttributes.addCorrespondentIndex(nameFrom, nameDest);
        }

        public override void actualizeInfo()
        {
            if (allcustomAttributes == null)
                allcustomAttributes = new MapMembersInfo(toMapAttributeType, srcType, dstType);
            allcustomAttributes.correspondentIndex();
        }
    }
}