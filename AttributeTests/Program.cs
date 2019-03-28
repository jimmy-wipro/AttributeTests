using System;
using System.Reflection.Emit;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace AttributeTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            var type = typeof(MyClass);

            var aName = new System.Reflection.AssemblyName("SomeNamespace");
            var ab =  AssemblyBuilder.DefineDynamicAssembly(aName, AssemblyBuilderAccess.Run);
            var mb = ab.DefineDynamicModule(aName.Name);
            var tb = mb.DefineType(type.Name + "Proxy", System.Reflection.TypeAttributes.Public, type);


            var attrCtorParams = new Type[] {};
            var attrCtorInfo = typeof(MyCustomAttribute).GetConstructor(attrCtorParams);
            var attrBuilder = new CustomAttributeBuilder(attrCtorInfo, new object[] { });
            tb.SetCustomAttribute(attrBuilder);

            var newType = tb.CreateType();
            var instance = Activator.CreateInstance(newType);



            Console.WriteLine(newType.GetType().Namespace);
          

            var res0 = instance.GetType().GetCustomAttributes().Count();

            var res = newType.GetCustomAttributes(typeof(MyCustomAttribute), false);

            bool b = Attribute.IsDefined(newType, typeof(MyCustomAttribute));

            Console.WriteLine(b);

            
        }
    }


    public class MyClass
    {

    }

    public class MyCustomAttribute : System.Attribute
    {
        public MyCustomAttribute()
        {
                
        }
    }
}
