using System;

namespace MapperReflect
{
    public class MappingCustomAttributes : Mapping
    {
        Type type;

        public MappingCustomAttributes(Type t)
        {
            type = t;
        }
        public override object getMappedObject(object src)
        {
            throw new NotImplementedException();
        }

        public override void MatchAttrib(string nameFrom, string nameDest)
        {
            throw new NotImplementedException();
        }

        public override void fillDictionary()
        {
       throw new NotImplementedException();
      }
    }
}