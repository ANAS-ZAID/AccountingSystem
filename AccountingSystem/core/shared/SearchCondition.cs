using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.core.shared
{
  
    public class SearchCondition

    {

        public string ColumnName { get; set; }

        public object SearchValue { get; set; }

        public DataType DataType { get; set; }

        public ComparisonType ComparisonType { get; set; }



        // قاموس لتخزين القيم المحولة

        public Dictionary<string, object> cachedValues = new Dictionary<string, object>();

    }

    public enum DataType

    {

        String,

        Int,

        DateTime,

        // ... أنواع بيانات أخرى

    }

    public enum ComparisonType

    {

        Contains,

        Equals,

        // ... أنواع مقارنة أخرى

    }
}
