using System.Collections.Specialized;
using Tossd.Jalapeno.Common;
using Tossd.Jalapeno.Data.Interfaces;

namespace Tossd.Jalapeno.Data
{
    public abstract class UIMapParser : IUIMapParser
    {
        protected readonly IDataSourceParser DataSourceParser;

        protected UIMapParser(string parserType)
        {
            DataSourceParser = ObjectFactory.Build<IDataSourceParser>(parserType);
        }

        public abstract StringDictionary ParseUIMap(string fileName);
    }
}