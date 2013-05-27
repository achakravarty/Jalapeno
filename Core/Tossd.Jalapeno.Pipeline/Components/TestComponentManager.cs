using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Tossd.Jalapeno.Pipeline.Attributes;

namespace Tossd.Jalapeno.Pipeline.Components
{
    public static class TestComponentManager
    {
        public static List<Type> ComponentTypes { get; set; }

        private static IEnumerable<Assembly> GetTestAssemblies()
        {
            //var allAssemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            string binPath = AppDomain.CurrentDomain.BaseDirectory;
            var loadedAssemblies = new List<string>();
            foreach (string dll in Directory.GetFiles(binPath, "*.dll"))
            {
                try
                {
                    loadedAssemblies.Add(dll.Substring(dll.LastIndexOf("\\") + 1));
                }
                catch (FileLoadException ex)
                { }
                catch (BadImageFormatException ex)
                { }
            }
            return loadedAssemblies.Select(assemblyName => Assembly.Load(assemblyName.Replace(".dll", string.Empty))).ToList();
        }

        public static void LoadComponentTypes()
        {
            var testTypes = new List<Type>();
            foreach (var assembly in GetTestAssemblies())
            {
                testTypes.AddRange(assembly.GetTypes().Where(t => t.IsDefined(typeof(TestComponentAttribute), true)).ToList());
            }
            ComponentTypes = testTypes;
        }
    }
}