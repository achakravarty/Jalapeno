using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tossd.Jalapeno.Data.Interfaces;
using Tossd.Jalapeno.Common;

namespace Tossd.Jalapeno.Core
{
    internal static class InterfaceImplementationInstantiator
    {
        internal static IDataSourceParser InstantiateDataSourceParser(string parserType)
        {
            if (string.IsNullOrEmpty(parserType))
            {
                throw new ArgumentNullException("parserType", "Cannot create a datasource parser of null type");
            }
            return ObjectFactory.Build<IDataSourceParser>(parserType);
        }

        internal static IUIMapParser InstantiateUIMapParser()
        {
            return ObjectFactory.Build<IUIMapParser>();
        }
    }
}
