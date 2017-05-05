using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperEmit
{
    public interface IMapper
    {
       // Mapper Bind(Mapping m);
        //Mapper Match(string nameFrom, string nameDest);
        Object Map(Object src);
        Object[] Map(Object[] src);
    }

  
}
