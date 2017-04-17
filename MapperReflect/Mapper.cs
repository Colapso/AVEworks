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
       
        Mapping mapping = Mapping.Properties;
        private Type tSrc;
        private Type tDst;

        public Mapper(Type klassSrc, Type klassDest)
        {
            tSrc = klassSrc;
            tDst = klassDest;
            mapping = new MappingPropreties(klassSrc, klassDest);
            
        }

        public object[] Map(object[] src)
        {
            object[] ret = (object[])Array.CreateInstance(tDst,src.Length);
            for(int i = 0; i< ret.Length; i++)
            {
                ret[i] = mapping.getMappedObject(src[i]);
            }
            return ret;
        }
        
        public object Map(object src)
        {
            
            return mapping.getMappedObject(src);     
        }

        public Mapper Bind(Mapping m)
        {
            mapping = m;
            mapping.dstType = tDst;
            mapping.srcType = tSrc;
            mapping.actualizeInfo();
            return this;
        }

        public Mapper Match(string nameFrom,string nameDest)
        {   
            mapping.MatchAttrib(nameFrom,nameDest);
            return this;
        }
    }
}
