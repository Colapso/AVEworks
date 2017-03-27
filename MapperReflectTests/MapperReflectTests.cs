using MapperReflect;
using MapperReflectTests.ClassesforTests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperReflectTests
{

    public class MapperReflectTests
    {
        [Test]
        public void testAttributesBuild()

        {
            IMapper m = AutoMapper.Build(typeof(Student), typeof(Person));
            Student s = new Student { Nr = 27721, Name = "Ze Manel" };
            Person p = (Person)m.Map(s);
            Assert.Equals(s.Name, p.Name);

        }
    } 
}
