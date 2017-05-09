using System;
using MapperReflectTests.ClassesforTests;

namespace MapperReflectTests
{
    internal class StudentToPersonCopier
    {
        internal Person Copy(Student s)
        {
            Person p = new Person();
            p.Name = s.Name;
            return p;
        }
    }
}