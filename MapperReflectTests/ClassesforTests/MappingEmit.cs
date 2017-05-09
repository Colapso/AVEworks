using System;
using MapperReflect;
using MapperReflectTests.ClassesforTests;

namespace MapperReflectTests
{
    internal class MappingEmit : Mapping
    {
        MapEmitInfo emit { get; set; }

        public MappingEmit()
        {
        }
        public MappingEmit(Type klasssrc, Type klassdst)
        {
            this.srcType = klasssrc;
            this.dstType = klassdst;
            emit = new MapEmitInfo(klasssrc, klassdst);
            emit.correspondentIndex();
        }
        

        public override void actualizeInfo()
        {
            if (emit == null)
                emit = new MapEmitInfo(srcType, dstType);
            emit.correspondentIndex();
        }

        public override object getMappedObject(object src)
        {
            /*Student s = (Student)src;
            StudentToPersonCopier sc = new StudentToPersonCopier();
            return sc.Copy(s);*/
            return emit.Copy(src);
        }

        public override void MatchAttrib(string nameFrom, string nameDest)
        {
            throw new NotImplementedException();
        }
    }
}