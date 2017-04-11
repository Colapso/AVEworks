using MapperReflect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperReflectTests.ClassesforTests
{
    class School
    {
        [ToMap(DefaultValue = "Undefined")]
        string Location { get; set; }

        [ToMap(DefaultValue = "Undefined")]
        string Name { get; set; }

        int[] MembersId { get; set; }
    }
}
