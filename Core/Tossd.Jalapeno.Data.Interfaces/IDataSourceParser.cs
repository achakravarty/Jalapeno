using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tossd.Jalapeno.Data.Interfaces
{
    public interface IDataSourceParser
    {
        /// <summary>
        /// Parses an external file to the specified type
        /// </summary>
        /// <param name="dataSource">The fully qualified path of the external file</param>
        /// <returns>A parsed object of the specified type</returns>
        object Parse(string dataSource);

        /// <summary>
        /// Parses the external file into the type of object specified
        /// </summary>
        /// <param name="dataSource">The fully qualified path of the external file</param>
        /// <returns>An object of type T as specified</returns>
        T ParseExact<T>(string dataSource) where T : class, new();
    }
}
