﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MapperReflect
{
    public class AutoMapper
    {


        public static IMapper Build(Type klassSrc, Type klassDest)
        {
             return new Mapper(klassSrc,klassDest);
        }
    }
}
