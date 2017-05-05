using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperReflect
{
    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Field, AllowMultiple = true)]
    public class ToMapAttribute : Attribute
    {

        public string DefaultValue { get; set; }
        public int DefaultValueToInt { get; set; }
    }
}
