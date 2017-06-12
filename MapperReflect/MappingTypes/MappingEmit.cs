using System;
using MapperReflect;


namespace MapperReflect
{
    public class MappingEmit : Mapping
    {
        MapEmitInfo emit { get; set; }
        ICopier cop;

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
            if(cop==null)
                cop= emit.Copy();
            return cop.CopyDynamically(src);
        }

        public override void MatchAttrib(string nameFrom, string nameDest)
        {
            emit.addCorrespondentIndex(nameFrom, nameDest);
        }

    }
}