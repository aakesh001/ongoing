using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Data.Util
{
    public abstract class Document
    {
        protected String FilePath = String.Empty;
        protected String FileString = String.Empty;
        protected DataTable FileData = new DataTable();

        public Document()
        {
        }

        public void setFilePath(String filePath)
        {
            FilePath = filePath;
        }

        public void setFileString(String fileString)
        {
            FileString = fileString;
        }

        public abstract DataTable ConvertStringToDataTable(string tableName);

        public abstract DataTable ConvertStringToDataTable();

        public abstract string ConvertDataToString();

        //public abstract String ReadFileIntoString();
    }
}