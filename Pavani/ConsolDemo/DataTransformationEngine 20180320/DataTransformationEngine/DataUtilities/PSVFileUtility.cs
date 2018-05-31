using System;
using System.IO;
using System.Data;

namespace DataUtilities
{
    public class PSVFileUtility : FileUtility
    {
        public PSVFileUtility(String filePath)
        {
            _filePath = filePath;
        }

        public override DataTable readFileIntoDataTable()
        {
            Boolean isHeader = true;
            using (StreamReader sR = new StreamReader(_filePath))
            {
                String line = String.Empty;
                while ((line = sR.ReadLine()) != null)
                {
                    if (isHeader == true)
                    {
                        isHeader = false;
                        createDataColumns(line);
                    }
                    else
                    {
                        addDataRow(line);
                    }
                }
            }
            return _fileData;
        }

        public override DataTable readFileIntoDataTable(String tableName)
        {
            _fileData.TableName = tableName;
            return readFileIntoDataTable();
        }

        public override String readFileIntoString()
        {
            String fileData = String.Empty;
            using (StreamReader sR = new StreamReader(_filePath))
            {
                String line = String.Empty;
                while ((line = sR.ReadLine()) != null)
                {
                    if (fileData != String.Empty)
                    {
                        fileData += Environment.NewLine + line;
                    }
                    else
                    {
                        fileData = line;
                    }
                }
            }
            return fileData;
        }

        private void createDataColumns(String line)
        {
            String[] lineData = line.Split("|");
            for (int i = 0; i < lineData.Length; i++)
            {
                DataColumn column = new DataColumn(lineData[i]);
                _fileData.Columns.Add(column);
            }
        }

        private void addDataRow(string line)
        {
            String[] lineData = line.Split("|");
            DataRow fileDataRow = _fileData.NewRow();
            for (int i = 0; i < lineData.Length; i++)
            {
                fileDataRow[i] = lineData[i];
            }
            _fileData.Rows.Add(fileDataRow);
        }

    }
}
