using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Data.Interoperability
{
    public class LookupTableSet
    {
        private Dictionary<String, LookupTable> lookupTables;

        public Dictionary<String, LookupTable> LookupTables
        {
            get
            {
                return lookupTables;
            }
        }

        public LookupTableSet()
        {
            lookupTables = new Dictionary<String, LookupTable>();
        }

        public LookupTable GetLookupTable(String lookupTableName)
        {
            return LookupTables[lookupTableName];
        }

        public static List<string> InvalidJsonElements;

        public void SetLookupTables<T>(String JSON)
        {
            var obj = JObject.Parse(JSON);
            var result = JObject.Parse(JSON)["copy"].Select(x => x.Children().Cast<JProperty>().ToDictionary(p => p.Name, p => (string)p.Value)).ToList();
            InvalidJsonElements = null;
            var array = JArray.Parse(JSON);
            List<T> objectsList = new List<T>();
            foreach (var item in array)
            {
                try
                {
                    // CorrectElements
                    objectsList.Add(item.ToObject<T>());
                }
                catch (Exception ex)
                {
                    InvalidJsonElements = InvalidJsonElements ?? new List<string>();
                    InvalidJsonElements.Add(item.ToString());
                }
            }
        }

        public void SetLookupTables(XmlDocument XML)
        {
            foreach (XmlNode lookupTableNodes in XML.GetElementsByTagName("lookupTables"))
            {
                foreach (XmlNode lookupTable in lookupTableNodes)
                {
                    LookupTable lookupTableObject = new LookupTable(lookupTable.Name);
                    foreach (XmlNode lookupTableChildNode in lookupTable)
                    {
                        if (lookupTableChildNode.Name == "row")
                        {
                            lookupTableObject.AddRow(lookupTableChildNode.SelectSingleNode("key").InnerText,
                                lookupTableChildNode.SelectSingleNode("value").InnerText);
                        }
                        else if (lookupTableChildNode.Name == "default")
                        {
                            lookupTableObject.SetDefaultValue(lookupTableChildNode.InnerText);
                        }
                    }
                    lookupTables.Add(lookupTable.Name, lookupTableObject);
                    Console.WriteLine("Lookup Table: " + lookupTable.Name + lookupTableObject.Rows.Count);
                }
            }
        }
    }

    public class LookupTable
    {
        protected String lookupTableName;
        protected Dictionary<String, String> rows;
        protected String defaultValue;

        public LookupTable(String lookupTableName, Dictionary<String, String> rows, String defaultValue)
        {
            this.lookupTableName = lookupTableName;
            this.rows = rows;
            this.defaultValue = defaultValue;
        }

        public LookupTable(String lookupTableName, Dictionary<String, String> rows)
        {
            this.lookupTableName = lookupTableName;
            this.rows = rows;
            this.defaultValue = String.Empty;
        }

        public LookupTable(String lookupTableName)
        {
            this.lookupTableName = lookupTableName;
            this.rows = new Dictionary<String, String>();
            this.defaultValue = String.Empty;
        }

        public String Name
        {
            get
            {
                return lookupTableName;
            }
        }

        public Dictionary<String, String> Rows
        {
            get
            {
                return rows;
            }
        }

        public String DefaultValue
        {
            get
            {
                return defaultValue;
            }
        }

        public void SetParameters(Dictionary<String, String> rows)
        {
            this.rows = rows;
        }

        public void SetDefaultValue(String defaultValue)
        {
            this.defaultValue = defaultValue;
        }

        public void AddRow(String key, String value)
        {
            Rows.Add(key, value);
        }

        public String Lookup(String key, String defaultValue)
        {
            if (Rows.ContainsKey(key))
            {
                return Rows[key];
            }
            else if (defaultValue.Equals(String.Empty))
            {
                return this.defaultValue;
            }
            else
            {
                return defaultValue;
            }
        }
    }
}