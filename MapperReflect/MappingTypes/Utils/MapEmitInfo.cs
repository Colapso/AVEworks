using MapperReflect;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace MapperReflect
{
    internal class MapEmitInfo : MappingEmit
    {
        public Type src { get; }
        public Type dst { get; }
        public PropertyInfo[] srcPropertyInfo { get; }
        public PropertyInfo[] dstPropertyInfo { get; }
        public List<MatchInfo> listOfProperties = new List<MatchInfo>();

        public MapEmitInfo(Type klasssrc, Type klassdst)
        {
            src = klasssrc;
            dst = klassdst;
            srcPropertyInfo = klasssrc.GetProperties();
            dstPropertyInfo = klassdst.GetProperties();
        }

        public ICopier Copy()
        {
            const string asmName = "DynamicCopy";
            AssemblyBuilder asm = CreateAsm(asmName);
            ModuleBuilder moduleBuilder = asm.DefineDynamicModule(asmName,asmName + ".dll");
            
            TypeBuilder typeBuilder = moduleBuilder.DefineType("DynamicCopy", TypeAttributes.Public, typeof(object), new Type[] { typeof(ICopier)});
            MethodBuilder methodBuilder = typeBuilder.DefineMethod("CopyDynamically", MethodAttributes.Virtual | MethodAttributes.Public | MethodAttributes.ReuseSlot, typeof(object), new Type[] { typeof(object) });
            
            ILGenerator ilGenerator = methodBuilder.GetILGenerator();
            ConstructorInfo constr = dst.GetConstructor(Type.EmptyTypes);
            LocalBuilder result = ilGenerator.DeclareLocal(dst);
            ilGenerator.Emit(OpCodes.Newobj, constr);
            ilGenerator.Emit(OpCodes.Stloc_0);
            ilGenerator.Emit(OpCodes.Ldloc_0);
            foreach (MatchInfo m in listOfProperties)
            {
                MethodInfo gSrc= srcPropertyInfo[m.SrcIdx].GetMethod;
                MethodInfo sDst = dstPropertyInfo[m.DstIdx].SetMethod;
                ilGenerator.Emit(OpCodes.Ldarg_1);
                ilGenerator.Emit(OpCodes.Callvirt, gSrc);
                ilGenerator.Emit(OpCodes.Callvirt, sDst);
                ilGenerator.Emit(OpCodes.Ldloc_0);
            }
            ilGenerator.Emit(OpCodes.Ret);
            
            Type dinamicCreateType = typeBuilder.CreateType();
            ICopier dinamicCreate = (ICopier)Activator.CreateInstance(dinamicCreateType);
            
            asm.Save(asmName + ".dll");
            return dinamicCreate;
        }

       

        private AssemblyBuilder CreateAsm(string name)
         {
             AssemblyName nameAsm = new AssemblyName(name);
             AssemblyBuilder builderAsm = AssemblyBuilder.DefineDynamicAssembly(nameAsm, AssemblyBuilderAccess.RunAndSave);
             return builderAsm;
         }

    public void correspondentIndex()
        {
            for (int i = 0; i < srcPropertyInfo.Length; i++)
            {
                for (int k = 0; k < dstPropertyInfo.Length; k++)
                {
                    if (srcPropertyInfo[i].PropertyType.Equals(dstPropertyInfo[k].PropertyType))
                    {
                        if (srcPropertyInfo[i].Name.Equals(dstPropertyInfo[k].Name))
                            AddProperty(i, k);
                    }

                }

            }
        }

        public void addCorrespondentIndex(string nameFrom, string nameDest)
        {

            for (int i = 0; i < srcPropertyInfo.Length; i++)
            {
                string name = srcPropertyInfo[i].Name;

                if (name.Contains("<") && name.Contains(">"))
                    name = name.Split('<')[1].Split('>')[0];

                for (int k = 0; k < dstPropertyInfo.Length; k++)
                {
                    string name2 = dstPropertyInfo[k].Name;

                    if (name2.Contains("<") && name2.Contains(">"))
                        name2 = name2.Split('<')[1].Split('>')[0];

                    if (name.Equals(nameFrom) && name2.Equals(nameDest))
                    {
                        AddProperty(i, k);
                        return;
                    }
                }

            }

        }


        private void AddProperty(int srci, int dsti)
        {
            MatchInfo m = new MatchInfo(srci, dsti);

            Type src = srcPropertyInfo[srci].PropertyType;
            Type dst = dstPropertyInfo[dsti].PropertyType;

            if (src.IsArray && dst.IsArray)
            {
                src = src.GetElementType();
                dst = dst.GetElementType();
            }

            if (!IsPrimitiveType(src))
            {
                m.MapperAux = AutoMapper.Build(src, dst).Bind(new MappingPropreties());
            }

            listOfProperties.Add(m);
        }
    }

    public interface ICopier
    {
        object CopyDynamically(object t);
    }


}