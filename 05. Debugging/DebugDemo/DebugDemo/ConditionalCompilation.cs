
#define development

using System.Reflection;

namespace DebugDemo
{
    public class MyType : MemberInfo
    {
        public override Type? DeclaringType => throw new NotImplementedException();

        public override MemberTypes MemberType => throw new NotImplementedException();

        public override string Name => throw new NotImplementedException();

        public override Type? ReflectedType => throw new NotImplementedException();

        public override object[] GetCustomAttributes(bool inherit)
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }
    }

    internal class ConditionalCompilation
    {
        public static void Main()
        {
#if development
                Console.WriteLine("In development version");
#elif production
                Console.WriteLine("In production version");
#else
                Console.WriteLine("Typo");
#endif
            int x = 5;
            Console.WriteLine(x.GetType().HasSameMetadataDefinitionAs(MyType));

        }
    }
}
