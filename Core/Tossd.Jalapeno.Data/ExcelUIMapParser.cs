using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using ExcelParser.Model;
using Tossd.Jalapeno.Common;
using Tossd.Jalapeno.Data.Interfaces;

namespace Tossd.Jalapeno.Data
{
    public class ExcelUIMapParser : IUIMapParser
    {
        private readonly IDataSourceParser _dataSourceParser;

        protected string SheetUIMap = "UIMap";
        protected string ColumnTestComponent = "TestComponent";
        protected string ColumnLocatorName = "LocatorName";
        protected string ColumnLocatorValue = "LocatorValue";

        /// <summary>
        /// Constructor to build the IDataSourceParser interface implementation
        /// </summary>
        public ExcelUIMapParser()
        {
            _dataSourceParser = ObjectFactory.Build<IDataSourceParser>("Excel");
        }

        /// <summary>
        /// Parses a file containing the UI Map locator information and returns a string dictionary of the same
        /// </summary>
        /// <param name="fileName">The fully qualified path of the file containing the UI Map</param>
        /// <returns>A String Dictionary of the locator information</returns>
        public virtual StringDictionary ParseUIMap(string fileName)
        {
            var uiMap = new StringDictionary();
            var parsedWorkbook = _dataSourceParser.Parse(fileName);
            if(parsedWorkbook == null)
            {
                throw new Exception();
            }
            var workBook = (Workbook)parsedWorkbook;
            foreach (Row row in workBook.Worksheets[SheetUIMap].Rows)
            {
                var key = ConstructKey(row);
                var value = row.Cells[ColumnLocatorValue].Value;
                uiMap.Add(key, value);
            }
            return uiMap;
        }

        private string ConstructKey(Row row)
        {
            var key = row.Cells[ColumnTestComponent].Value.Trim() + "." + row.Cells[ColumnLocatorName].Value.Trim();
            return key;
        }
    }
}