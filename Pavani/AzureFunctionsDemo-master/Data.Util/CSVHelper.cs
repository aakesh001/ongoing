using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Util
{
    public class CSVHelper : Document
    {
        private string Delimiter { get; set; }

        public CSVHelper()
        {
        }

        public CSVHelper(string fileString, string delimeter)
        {
            FileString = fileString;
            Delimiter = delimeter;
        }

        public override DataTable ConvertStringToDataTable(String tableName)
        {
            FileData.TableName = tableName;
            return ConvertStringToDataTable();
        }

        public override DataTable ConvertStringToDataTable()
        {
            bool IsColumnCreated = false;
            string[] Rowlines = ConvertFileContentToArray();

            foreach (var line in Rowlines)
            {
                if (!IsColumnCreated)
                {
                    CreateColumns(line);
                    IsColumnCreated = true;
                }
                else
                {
                    CreateRows(line);
                }
            }
            return FileData;
        }

        public override string ConvertDataToString()
        {
            StringWriter csvString = new StringWriter();
            using (var csv = new CsvWriter(csvString))
            {
                csv.Configuration.Delimiter = Delimiter;
                using (var dt = FileData)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        csv.WriteField(column.ColumnName);
                    }
                    csv.NextRecord();
                    foreach (DataRow row in dt.Rows)
                    {
                        for (var i = 0; i < dt.Columns.Count; i++)
                        {
                            csv.WriteField(row[i]);
                        }
                        csv.NextRecord();
                    }
                }
            }
            return csvString.ToString();
        }

        private void CreateColumns(string line)
        {
            var columnLines = line.Split('|');
            foreach (string column in columnLines)
            {
                DataColumn Datecolumn = new DataColumn(column);
                Datecolumn.AllowDBNull = true;
                FileData.Columns.Add(Datecolumn);
            }
        }

        private void CreateRows(string line)
        {
            String[] lineData = line.Split('|');
            DataRow fileDataRow = FileData.NewRow();
            for (int i = 0; i < lineData.Length; i++)
            {
                fileDataRow[i] = lineData[i];
            }
            FileData.Rows.Add(fileDataRow);
        }

        private string[] ConvertFileContentToArray()
        {
            byte[] ByteArray = Encoding.UTF8.GetBytes(FileString);

            MemoryStream stream = new MemoryStream(ByteArray);
            StreamReader reader = new StreamReader(stream);
            string[] Rowlines = FileString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            return Rowlines;
        }
    }
}