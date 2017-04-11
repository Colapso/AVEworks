using MapperReflect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperReflectTests.ClassesforTests
{
    class Organization
    {
        int[] MembersId { get; set; }

        [ToMap(DefaultValue = "Undefined")]
        string Name { get; set; }
    }
}
