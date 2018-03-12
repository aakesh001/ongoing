using System;
using System.IO;
using System.Xml;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

using System.Data;

namespace AzureFunctionsDemo
{
    public static class QueueTriggerFunction
    {
        private const string Conn = "Server=tcp:usaathubdbserver.database.windows.net,1433;Initial Catalog =UsatHubDB;Persist Security Info = False; User ID = usathub; Password =Isango_01; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30";

        [FunctionName("QueueTriggerFunction")]
        public static void WriteLog([BlobTrigger("pavaniblog2/{name}")] string logMessage, string name, TextWriter logger)
        {
            string XMLContent = string.Empty;
            bool Result = IsValidXmlString(logMessage);
            XMLContent = logMessage.Replace(System.Environment.NewLine, String.Empty);
            XMLContent = XMLContent.Replace(@"\", String.Empty);
            if (!Result)
            {
                XMLContent = RemoveInvalidXmlChars(logMessage);
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(XMLContent);
            SaveDataInXML(doc);
        }

        /// <summary>
        /// Remove Characters like \n and \r
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string RemoveInvalidXmlChars(string text)
        {
            var validXmlChars = text.Where(ch => XmlConvert.IsXmlChar(ch)).ToArray();
            return new string(validXmlChars);
        }

        /// <summary>
        /// MEthod to chech is XML in ProperFormatted
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static bool IsValidXmlString(string text)
        {
            try
            {
                XmlConvert.VerifyXmlChars(text);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        //Extract Data from XML
        /// </summary>
        /// <param name="xml"></param>
        private static void SaveDataInXML(XmlDocument xml)
        {
            foreach (XmlNode item in xml.GetElementsByTagName("note"))
            {
                string To = item.SelectSingleNode("to").InnerText;
                string From = item.SelectSingleNode("from").InnerText;
                string Heading = item.SelectSingleNode("heading").InnerText;
                string Body = item.SelectSingleNode("body").InnerText;

                bool SuccessFull = SaveInSql(To, From, Heading, Body);
                if (!SuccessFull)
                {
                    //we can this field to show user which field is incorrect
                }
            }
        }

        /// <summary>
        /// Save Data in SQL or XML
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="heading"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        private static bool SaveInSql(string to, string from, string heading, string body)
        {
            bool successful = false;
            try
            {
                using (SqlConnection con = new SqlConnection(Conn))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_ins_note", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@to", SqlDbType.VarChar).Value = to;
                        cmd.Parameters.Add("@from", SqlDbType.VarChar).Value = from;
                        cmd.Parameters.Add("@heading", SqlDbType.VarChar).Value = heading;
                        cmd.Parameters.Add("@body", SqlDbType.VarChar).Value = body;

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return successful;
            }
            catch (Exception ex)
            {
                return successful = false;
            }
        }
    }
}