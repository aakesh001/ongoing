using System;
using System.IO;
using System.Data;

namespace DataUtilities
{
    public class XmlFileUtility : FileUtility
    {
        public XmlFileUtility()
        {
        }

        public XmlFileUtility(String filePath)
        {
            _filePath = filePath;
        }

        public override String readFileIntoString()
        {
            try
            {
                String data = File.ReadAllText(_filePath);
                _fileString = data.Replace(System.Environment.NewLine, String.Empty).Replace(@"\", String.Empty);
            }
            catch (Exception e)
            {
                return String.Empty;
            }
            return _fileString;
        }

        public override DataTable readFileIntoDataTable()
        {
            return _fileData;
        }

        public override DataTable readFileIntoDataTable(String tableName)
        {
            _fileData.TableName = tableName;
            return readFileIntoDataTable();
        }
    }
 }
