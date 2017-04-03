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
        //Dictionary<String,MapFieldsInfo> map = new Dictionary<String,MapFieldsInfo>(); //map key = type of soucer
        Mapping mapping = Mapping.Properties;
        private Type tSrc;
        private Type tDst;

        public Mapper(Type klassSrc, Type klassDest)
        {
            tSrc = klassSrc;
            tDst = klassDest;
        }

        public object[] Map(object[] src)
        {
            throw new NotImplementedException();
        }
        
        public object Map(object src)
        {
            mapping.dstType = tDst;
            mapping.srcType = tSrc;
            return mapping.getMappedObject(src);     
        }

        public Mapper Bind(Mapping m)
        {
            mapping = m;
            return this;
        }

        public Mapper Match(string nameFrom,string nameDest)
        {   
            mapping.MatchAttrib(nameFrom,nameDest);
            return this;
        }
    }
}
