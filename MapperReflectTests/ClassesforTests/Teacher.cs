﻿using MapperReflect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperReflectTests.ClassesforTests
{
    class Teacher
    {
        Teacher(int nr, string name) { }

        [ToMap(DefaultValue = "Undefined")]
        public string Name { get; set; }

        [ToMap(DefaultValueToInt = 0)]
        public int Id { get; set; }
    }
}
