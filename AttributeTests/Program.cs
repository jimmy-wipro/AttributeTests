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
            
            var type = typeof(MyClass);

            var aName = new System.Reflection.AssemblyName("SomeNamespace");
            var ab =  AssemblyBuilder.DefineDynamicAssembly(aName, AssemblyBuilderAccess.Run);
            var mb = ab.DefineDynamicModule(aName.Name);
            var tb = mb.DefineType(type.Name + "Proxy", System.Reflection.TypeAttributes.Public, type);


            var attrCtorParams = new Type[] {};
            var attrCtorInfo = typeof(MyCustomAttribute).GetConstructor(attrCtorParams);
            var attrBuilder = new CustomAttributeBuilder(attrCtorInfo, new object[] { });
            tb.SetCustomAttribute(attrBuilder);

            //Dynamic Type -- MyClassProxy
            var newType = tb.CreateType();

            //Instance of Dynamic Type -- MyClassProxy
            var instance = Activator.CreateInstance(newType);

           
          

            var customAttributeCountOnInstance = instance.GetType().GetCustomAttributes().Count();

            var customAttributeCountOnDynamicType = newType.GetCustomAttributes(typeof(MyCustomAttribute), false);

            bool hasCustomAttribute = Attribute.IsDefined(newType, typeof(MyCustomAttribute));

            Console.WriteLine(hasCustomAttribute);

            
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
