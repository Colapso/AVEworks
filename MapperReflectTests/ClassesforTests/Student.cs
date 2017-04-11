using MapperReflect;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperReflectTests.ClassesforTests
{
    
   public class Student
    {
        [ToMap(DefaultValueToInt = 0)]
        public int Nr { get; set; }

        [ToMap(DefaultValue = "Undefined")]
        public string Name { get; set; }

        public Course[] Courses { get; set; }

        public Student()
        {
            Courses = new Course[1];
        }
    }
}
