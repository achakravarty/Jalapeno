using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Tossd.Jalapeno.Data.Interfaces
{
    public interface IUIMapParser
    {
        /// <summary>
        /// Parses a file containing the UI Map locator information and returns a string dictionary of the same
        /// </summary>
        /// <param name="fileName">The fully qualified path of the file containing the UI Map</param>
        /// <returns>A String Dictionary of the locator information</returns>
        StringDictionary ParseUIMap(string fileName);
    }
}
