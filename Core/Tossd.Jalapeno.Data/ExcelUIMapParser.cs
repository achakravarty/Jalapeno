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

        public ExcelUIMapParser()
        {
            _dataSourceParser = ObjectFactory.Build<IDataSourceParser>("Excel");
        }

        public virtual StringDictionary ParseUIMap(string fileName)
        {
            var uiMap = new StringDictionary();
            var workBook = (Workbook)_dataSourceParser.Parse(fileName);
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
