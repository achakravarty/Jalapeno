using System;
using ExcelParser;
using ExcelParser.Model;
using Tossd.Jalapeno.Data.Interfaces;

namespace Tossd.Jalapeno.Data
{
    public class ExcelDataSourceParser : IDataSourceParser
    {
        /// <summary>
        /// Parses the external excel file into an excel workbook object
        /// </summary>
        /// <param name="dataSource">The fully qualified path of the excel file</param>
        /// <returns>A workbook object of the excel file</returns>
        public object Parse(string dataSource)
        {
            try
            {
                var provider = new ExcelProvider(dataSource);
                return provider.GetWorkbook();
            }
            catch (Exception ex)
            {
                ex.Data.Add("GetWorkBook Exception", "Failed to load excel workbook - " + dataSource);
                throw;
            }
        }

        /// <summary>
        /// Parses the external excel file into the type of object specified
        /// </summary>
        /// <param name="dataSource">The fully qualified path of the excel file</param>
        /// <returns>An object of type T as specified</returns>
        public T ParseExact<T>(string dataSource) where T : class, new()
        {
            return Parse(dataSource) as T;
        }
    }
}