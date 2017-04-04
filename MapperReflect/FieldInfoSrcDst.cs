using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MapperReflect
{
    class FieldInfoSrcDst
    {
        public FieldInfo src { get; set; }
        public FieldInfo dst { get; set; }
        public FieldInfoSrcDst(FieldInfo src, FieldInfo dst)
        {
            this.src = src;
            this.dst = dst;
        }
    }
}
