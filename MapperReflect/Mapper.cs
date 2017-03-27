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
        private FieldInfo srcpropertie;
        public Mapper(Type klassSrc, Type klassDest)
        {
            src = klassSrc;
            dst = klassDest;
            //srcpropertie = klassSrc.GetProperties(BindingFlags.NonPublic|BindingFlags.Public);
            
        }

        public object[] Map(object[] src)
        {
            throw new NotImplementedException();
        }

        public object Map(object src)
        {
            throw new NotImplementedException();
        }

        public Mapper Bind(Mapping m)
        {
            return null;
        }

        public Mapper Match(string nameFrom,string nameDest)
        {
            return null;
        }
    }
}
