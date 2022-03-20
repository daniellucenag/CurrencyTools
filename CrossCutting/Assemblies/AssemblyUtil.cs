using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace CrossCutting.Assemblies
{
    [ExcludeFromCodeCoverage]
    public static class AssemblyUtil
    {
        public static IEnumerable<Assembly> GetCurrentAssemblies()
        {
            return new Assembly[]
            {
                Assembly.Load("Api"),
                Assembly.Load("Application"),
                Assembly.Load("Domain"),
                Assembly.Load("Infrastructure"),
                Assembly.Load("CrossCutting")
            };
        }

    }
}
