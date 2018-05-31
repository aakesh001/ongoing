using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Util
{
    public class XMLHelper : Document
    {
        public XMLHelper()
        {
        }

        public XMLHelper(string filePath)
        {
            FilePath = filePath;
        }

        public override DataTable ConvertStringToDataTable(String tableName)
        {
            FileData.TableName = tableName;
            return ConvertStringToDataTable();
        }

        public override DataTable ConvertStringToDataTable()
        {
            return FileData;
        }

        public override string ConvertDataToString()
        {
            try
            {
                String data = File.ReadAllText(FilePath);
                FileString = data.Replace(System.Environment.NewLine, String.Empty).Replace(@"\", String.Empty);
            }
            catch (Exception e)
            {
                return String.Empty;
            }
            return FileString;
        }
    }
}