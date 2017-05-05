using System;
using System.Reflection;
using System.Reflection.Emit;

namespace MapperEmit
{
    public abstract class Mapping
    {

        public static Mapping Properties = new MappingPropreties();
        public static Mapping Fields = new MappingFields();

        public Type srcType { get; set; }
        public Type dstType { get; set; }

        public Mapping()
        {

        }

        public abstract object getMappedObject(object src);

        public abstract void MatchAttrib(string nameFrom, string nameDest);
        public abstract void actualizeInfo();

        public static bool IsPrimitiveType(Type fieldType)
        {
            return fieldType.IsPrimitive || fieldType.Namespace.Equals("System");
        }







        private object CreateInstanceWithAssembly(Type tSrc,Type tDst)
        {
            const string asmName = "DynamicCreate";
            AssemblyBuilder asm = CreateAsm(asmName);
            ModuleBuilder moduleBuilder = asm.DefineDynamicModule(asmName + ".dll");

            TypeBuilder typeBuilder = moduleBuilder.DefineType("DynamicCreate",TypeAttributes.Public,typeof(object));
            MethodBuilder methodBuilder = typeBuilder.DefineMethod("CreateInstance", MethodAttributes.Public | MethodAttributes.ReuseSlot,typeof(object),new Type[]{tDst});

            ILGenerator ilGenerator = methodBuilder.GetILGenerator();
            








            ilGenerator.Emit(OpCodes.Ret);

            Type dinamicCreateType = typeBuilder.CreateType();
            object dinamicCreate = Activator.CreateInstance(dinamicCreateType);

            asm.Save(asmName + ".dll");

            return dinamicCreate;
        }

        private AssemblyBuilder CreateAsm(string name)
        {
            AssemblyName nameAsm = new AssemblyName(name);
            AssemblyBuilder builderAsm = AssemblyBuilder.DefineDynamicAssembly(nameAsm, AssemblyBuilderAccess.RunAndSave);
            return builderAsm;
        }







    }
}