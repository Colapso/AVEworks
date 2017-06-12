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
            IMapper m = AutoMapper.Build(typeof(Student), typeof(Person))
                .Bind(Mapping.Fields)
                .Match("Nr", "Id");

            Student s = new Student { Nr = 27721, Name = "Ze Manel" };
            Person p = (Person)m.Map(s);
            Assert.AreEqual(s.Nr, p.Id);
            Assert.AreEqual(s.Name, p.Name);
        }

        [Test]
        public void testMappingArray()
        {
            Mapper m = AutoMapper
            .Build(typeof(Student), typeof(Person))
            .Bind(Mapping.Fields)
            .Match("Nr", "Id")
            .Match("Courses", "Subjects");
            Student[] stds = { new Student { Nr = 42929, Name = "Gonçalo Barros" }, new Student { Nr = 42144, Name = "Nuno Cardoso" }, new Student { Nr = 42140, Name = "Marco Agostinho" } };
            stds[0].Courses = new Course []{ new Course("AVE"), new Course("LS") };
            stds[1].Courses = new Course[] { new Course("LS") };
            stds[2].Courses = new Course[] { new Course("POO") };
            object[] ps = m.Map(stds);
            Person[] pso = new Person[ps.Length];

            for (int i = 0; i < stds.Length; i++)
            {
                pso[i] = (Person)ps[i];
                Assert.AreEqual(stds[i].Nr, pso[i].Id);
            }
        }

        [Test]
        public void testMappingCustomAttr()
        {
            IMapper m = AutoMapper.Build(typeof(Student), typeof(Person))
                .Bind(new MappingCustomAttributes(typeof(ToMapAttribute)));
            Student s = new Student { Nr = 27721, Name = "Ze Manel" };
            Person p = (Person)m.Map(s);
            Assert.AreEqual(s.Name, p.Name);
        }

        [Test]
        public void testMatchWithDifferentTypes()
        {
            Mapper m = AutoMapper
            .Build(typeof(Student), typeof(Person))
            .Bind(Mapping.Fields)
            .Match("Nr", "Id")
            .Match("Course", "Subject");
            Student stds = new Student { Nr = 42929, Name = "Gonçalo Barros" };
            stds.Course = new Course("AVE");


            Person pso = (Person)m.Map(stds);

            Assert.AreEqual(stds.Nr, pso.Id);
            Assert.AreEqual(stds.Course.name, pso.Subject.name);
        }



        [Test]
        public void testMapUsingEmit()
        {
            IMapper m = AutoMapper.Build(typeof(Student), typeof(Person)).Bind(new MappingEmit());
            Student s = new Student { Nr = 27721, Name = "Ze Manel" };
            Person p = (Person)m.Map(s);
            Assert.AreEqual(s.Name, p.Name);
        }

        [Test]
        public void testAttributesBuildWithMatchAndEmit()
        {
            IMapper m = AutoMapper.Build(typeof(Student), typeof(Person))
                .Bind(new MappingEmit())
                .Match("Nr", "Id");

            Student s = new Student { Nr = 27721, Name = "Ze Manel" };
            Person p = (Person)m.Map(s);
            Assert.AreEqual(s.Nr, p.Id);
            Assert.AreEqual(s.Name, p.Name);
        }

    }
}
