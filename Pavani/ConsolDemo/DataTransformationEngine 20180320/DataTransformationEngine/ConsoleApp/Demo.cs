using DataStore;
using DataUtilities;
using AppConfigurations;
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

namespace ConsoleApp
{
    // Console application to test Data Utilities and Data Store
    // and try out some C# features.
    internal class Demo
    {
        private static void Main(string[] args)
        {
            String configFilePath = "C:\\Work\\MapDemo.xml";
            String PSVFilePath = "C:\\Work\\test.txt";

            FileUtility xmlUtil = new XmlFileUtility(configFilePath); //TODO: Utility should be redone with Factory pattern
            XmlDocument configRulesXML = new XmlDocument(); //TODO: Move to Config Class
            configRulesXML.LoadXml(xmlUtil.readFileIntoString()); //TODO: Move to Config Class

            MapSet maps = new MapSet();
            maps.SetMappingRules(configRulesXML);

            LookupTableSet lookupTables = new LookupTableSet();
            lookupTables.SetLookupTables(configRulesXML);

            FileUtility util = new PSVFileUtility(PSVFilePath);
            DataObject dObj = new DataObject(util.readFileIntoDataTable());

            ShowDataObjectAsJSON(dObj, "PSV File"); //REMOVE
            dObj.Map(maps, lookupTables);
            ShowDataObjectAsJSON(dObj, "Final Data"); //REMOVE

            Console.WriteLine("\nPress any key to exit."); //REMOVE
            Console.ReadKey(); //REMOVE
        }

        /// <summary>
        /// Display the DataTable within the DataObject as an XML
        /// </summary>
        /// <param name="dObj">Data Object</param>
        /// <param name="caption">Caption to Show</param>
        /// <returns></returns>
        private static void ShowDataObjectAsXML(DataObject dObj, String caption)
        {
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dObj.Data.WriteXml(writer);

            Console.WriteLine("\n==============================");
            Console.WriteLine(caption);
            Console.WriteLine("==============================");
            Console.WriteLine(writer.ToString());
        }

        /// <summary>
        /// Display the DataTable within the DataObject as an JSON
        /// </summary>
        /// <param name="dObj">Data Object</param>
        /// <param name="caption">Caption to Show</param>
        /// <returns></returns>
        private static void ShowDataObjectAsJSON(DataObject dObj, String caption)
        {
            var JSONString = new StringBuilder();
            if (dObj.Data.Rows.Count > 0)
            {
                JSONString.Append("{"
                    + "\""
                    + dObj.Data.TableName
                    + "\":");
                JSONString.Append("[");
                for (int i = 0; i < dObj.Data.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < dObj.Data.Columns.Count; j++)
                    {
                        if (j < dObj.Data.Columns.Count - 1)
                        {
                            JSONString.Append("\""
                                + dObj.Data.Columns[j].ColumnName.ToString()
                                + "\":"
                                + "\""
                                + dObj.Data.Rows[i][j].ToString()
                                + "\",");
                        }
                        else if (j == dObj.Data.Columns.Count - 1)
                        {
                            JSONString.Append("\""
                                + dObj.Data.Columns[j].ColumnName.ToString()
                                + "\":"
                                + "\""
                                + dObj.Data.Rows[i][j].ToString()
                                + "\"");
                        }
                    }
                    if (i == dObj.Data.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
                JSONString.Append("}");
            }

            Console.WriteLine("\n==============================");
            Console.WriteLine(caption);
            Console.WriteLine("==============================");
            Console.WriteLine(JSONString.ToString());
        }
    }
}