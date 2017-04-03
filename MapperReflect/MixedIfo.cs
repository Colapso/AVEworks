using System;
using System.Reflection;

namespace MapperReflect
{
    internal class MixedIfo
    {
        private Type src;
        private Type dst;
        //private PropertyInfo[] srcPropertie, dstPropertie;
        private FieldInfo[] srcFieldInfo;
        private FieldInfo[] dstFieldInfo;
     
        public MixedIfo(Type klasssrc, Type klassdst)
        {
            src = klasssrc;
            dst = klassdst;
            srcFieldInfo = klasssrc.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            dstFieldInfo = klassdst.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            // srcPropertie = src.GetProperties();
            // dstPropertie = dst.GetProperties();

        }

        public object MapField(object src)
        {
            object ret = Activator.CreateInstance(dst);
            foreach (FieldInfo i in srcFieldInfo)
            {
                foreach (FieldInfo k in dstFieldInfo)
                {
                    if (i.FieldType.Equals(k.FieldType))
                    {
                        if (i.Name.Equals(k.Name))
                            k.SetValue(ret, i.GetValue(src));
                    }
                }
            }
            return ret;
        }


        public FieldInfo[] fieldInfoSrc
        {
            get { return srcFieldInfo; }
        }

        public FieldInfo[] fieldInfoDst
        {
            get { return dstFieldInfo; }
        }

        public Type destType
        {
            get { return dst;}
        }
        public Type srcType
        {
            get { return src; }

        }

    }
}