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
       
        MixedIfo fieldInfoandPropertiesOfSrcDst;            //instance to add to map
        Dictionary<String,MixedIfo> map = new Dictionary<String,MixedIfo>(); //map key = type of soucer

        public Mapper(Type klassSrc, Type klassDest)
        {
            fieldInfoandPropertiesOfSrcDst = new MixedIfo(klassSrc, klassDest);
            map.Add(klassSrc.Name,fieldInfoandPropertiesOfSrcDst);
        }

        public object[] Map(object[] src)
        {
            throw new NotImplementedException();
        }
        
        public object Map(object src)
        {
            MixedIfo aux = map[src.GetType().Name];                 //get the name of srcObject, thats the key;
            object ret = aux.MapField(src);
            return ret;     
        }

        public Mapper Bind(Mapping m)
        {
            return null;
        }

        public Mapper Match(string nameFrom,string nameDest)
        {   
           /* FieldInfo argumentFrom = src.GetField(nameFrom, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo argumentTo = dst.GetField(nameDest, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            argumentTo.SetValue(dst, argumentFrom.GetValue(src));*/

            return null;
        }
    }
}
