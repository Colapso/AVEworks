using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MapperReflect
{
    class PropertyInfoSrcDst
    {
        public PropertyInfo src { get; set; }
        public PropertyInfo dst { get; set; }
        public PropertyInfoSrcDst(PropertyInfo src, PropertyInfo dst)
        {
            this.src = src;
            this.dst = dst;
        }
    }
}
