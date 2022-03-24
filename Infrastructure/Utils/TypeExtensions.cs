using System;
using System.Linq;

namespace Infrastructure.Utils
{
    public static class TypeExtensions
    {
        public static bool IsInstanceOfGenericType(this Type genericType, Type type)
        {
            return type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericType);
        }
    }
}
