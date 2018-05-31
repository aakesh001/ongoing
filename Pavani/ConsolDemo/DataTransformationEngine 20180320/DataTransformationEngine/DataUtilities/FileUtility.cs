using System;
using System.IO;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace DataUtilities
{
    public abstract class FileUtility
    {
        protected String _filePath = String.Empty;
        protected String _fileString = String.Empty;
        protected DataTable _fileData = new DataTable();

        public void setFilePath(String filePath)
        {
            _filePath = filePath;
        }

        public abstract String readFileIntoString();
        public abstract DataTable readFileIntoDataTable();
        public abstract DataTable readFileIntoDataTable(String tableName);
    }
}
