using MapperReflect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperReflectTests.ClassesforTests
{

    class Person
    {
        [ToMap(DefaultValue = "Undefined")]
        public string Name { get; set; }

        [ToMap(DefaultValueToInt = 0)]
        public int Id { get; set; }

        public Subject[] Subjects {get; set; }


        public Subject Subject { get; set; }
    }
}
