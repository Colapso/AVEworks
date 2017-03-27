using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MapperReflect
{
    public class Mapper : IMapper
    {
        private Type src;
        private Type dst;
        private PropertyInfo[] srcPropertie, dstPropertie;
        public Mapper(Type klassSrc, Type klassDest)
        {
            src = klassSrc;
            dst = klassDest;
            srcPropertie = klassSrc.GetProperties();
            dstPropertie = klassDest.GetProperties();


        }

        public object[] Map(object[] src)
        {
            throw new NotImplementedException();
        }

        public object Map(object src)
        {
            object ret = Activator.CreateInstance(dst);
            FieldInfo[] f= src.GetType().GetFields(BindingFlags.Public|BindingFlags.Instance|BindingFlags.NonPublic);
            FieldInfo[] d = ret.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            for (int i = 0; i< f.Length; i++)    
            {
                for (int k = 0; k < d.Length; k++)
                {
                    if (f[i].FieldType.Equals(d[k].FieldType))
                    {
                        if(f[i].Name.Equals(d[k].Name))
                            d[i].SetValue(ret, f[i].GetValue(src));
                        
                    }
                }
            }
            return ret;     
        }

        public Mapper Bind(Mapping m)
        {
            return null;
        }

        public Mapper Match(string nameFrom,string nameDest)
        {
            for(int i = 0;i< srcPropertie.Length; i++)
            {
                if (srcPropertie[i].Name.Equals(nameFrom))
                {
                   // srcPropertie[i].Name = nameDest;
                }
            }
            return null;
        }
    }
}
