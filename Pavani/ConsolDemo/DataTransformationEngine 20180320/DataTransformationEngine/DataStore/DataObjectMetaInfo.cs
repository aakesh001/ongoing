using System;
using System.Collections.Generic;
using System.Text;

namespace DataStore
{
    public class DataObjectColumnMetaInfo
    {
        public String columnName;
        public Type type;
        public List<String> tags;

        public DataObjectColumnMetaInfo()
        {
            columnName = String.Empty;
            type = typeof(System.String);
            tags = new List<String>();
        }
    }
}
