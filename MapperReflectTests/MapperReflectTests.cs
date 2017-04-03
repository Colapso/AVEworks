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
            Assert.AreEqual(s.Name, p.Name);
        }

        [Test]
        public void testAttributesBuildWithBind()

        {
            IMapper m = AutoMapper.Build(typeof(Student), typeof(Person)).Bind(Mapping.Fields);
            Student s = new Student { Nr = 27721, Name = "Ze Manel" };
            Person p = (Person)m.Map(s);
            Assert.AreEqual(s.Name, p.Name);
        }

        [Test]
        public void testAttributesBuildWithMatch()

        {
            IMapper m = AutoMapper.Build(typeof(Student), typeof(Person)).Bind(Mapping.Fields).Match("Nr","Id");
            Student s = new Student { Nr = 27721, Name = "Ze Manel" };
            Person p = (Person)m.Map(s);
            Assert.AreEqual(s.Nr, p.Id);
        }
    } 
}
