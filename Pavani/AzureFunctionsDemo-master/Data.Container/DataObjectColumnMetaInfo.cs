using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Container
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