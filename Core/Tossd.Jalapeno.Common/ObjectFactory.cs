using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Tossd.Jalapeno.Common
{
    public static class ObjectFactory
    {
        public static T Build<T>() where T : class
        {
            return Build(typeof(T), null) as T;
        }

        public static T Build<T>(string genericTypeName) where T : class
        {
            return Build(typeof(T), genericTypeName) as T;
        }

        private static object Build(Type interfaceType, string genericTypeName)
        {
            try
            {
                if(interfaceType == null)
                {
                    throw new ArgumentNullException("interfaceType");
                }
                var key = ConstructKey(interfaceType.Name, genericTypeName);
                var typeName = ConfigurationManager.AppSettings[key];
                if (string.IsNullOrWhiteSpace(typeName))
                {
                    return null;
                }
                var settings = Activator.CreateInstance(Type.GetType(typeName));
                return settings;
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot construct object when no type is specified", ex);
            }
        }

        private static string ConstructKey(string interfaceName, string genericTypeName)
        {
            if (string.IsNullOrWhiteSpace(interfaceName))
            {
                throw new ArgumentNullException("interfaceName", "Interface name cannot be empty");
            }
            return string.IsNullOrWhiteSpace(genericTypeName) ? interfaceName : string.Format("{0}|{1}", interfaceName, genericTypeName);
        }
    }
}
