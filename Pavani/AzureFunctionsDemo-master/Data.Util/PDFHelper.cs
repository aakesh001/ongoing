using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Util
{
    internal class PDFHelper : Document
    {
        public PDFHelper()
        {
        }

        public PDFHelper(string FilePath)
        {
        }

        public override DataTable ConvertStringToDataTable(String tableName)
        {
            FileData.TableName = tableName;
            return ConvertStringToDataTable();
        }

        public override string ConvertDataToString()
        {
            return string.Empty;
        }

        public override DataTable ConvertStringToDataTable()
        {
            return FileData;
        }
    }
}