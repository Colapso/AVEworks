using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapperReflect;
using MapperReflectTests.ClassesforTests;
using NUnit.Framework;
using System.Diagnostics;

namespace MapperReflectTests
{
    class PerformanceTests
    {
        private int NUM_ITERATIONS = 10000000;
        //Mapping by properties with ilGenerator
        [Test]
        public void testMapUsingEmit()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int iter = NUM_ITERATIONS;
            
            IMapper m = AutoMapper.Build(typeof(Student), typeof(Person)).Bind(new MappingEmit());
            Student s = new Student { Nr = 27721, Name = "Ze Manel" };
            Person p = null;
            while (iter > 0)
            {
                p = (Person)m.Map(s);
                --iter;
            }
            Assert.AreEqual(s.Name, p.Name);
            sw.Stop();
            Console.WriteLine("Elapsed={0}", sw.Elapsed);
        }

        //Mapping by properties with reflection
        [Test]
        public void testMapUsingMappingProperties()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int iter = NUM_ITERATIONS;

            IMapper m = AutoMapper.Build(typeof(Student), typeof(Person));
            Student s = new Student { Nr = 27721, Name = "Ze Manel" };
            Person p = null;
            while (iter > 0)
            {
                p = (Person)m.Map(s);
                --iter;
            }
            Assert.AreEqual(s.Name, p.Name);
            sw.Stop();
            Console.WriteLine("Elapsed={0}", sw.Elapsed);
        }

        //Mapping by properties with direct copy
        [Test]
        public void testDirectMap()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int iter = NUM_ITERATIONS;
            StudentToPersonCopier map = new StudentToPersonCopier();
            Student s = new Student { Nr = 27721, Name = "Ze Manel" };
            Person p = null;
            while (iter > 0)
            {
                p = map.Copy(s);
                --iter;
            }
            Assert.AreEqual(s.Name, p.Name);
            sw.Stop();
            Console.WriteLine("Elapsed={0}", sw.Elapsed);
        }
    }
}
