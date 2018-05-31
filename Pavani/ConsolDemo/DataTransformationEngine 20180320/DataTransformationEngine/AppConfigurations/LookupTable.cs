using System;
using System.Data;
using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace AppConfigurations
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
        
        public void SetLookupTables(String JSON)
        {
            //TODO: JSON Implementation
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

        public LookupTable(String lookupTableName, Dictionary<String,String> rows, String defaultValue)
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
            if(Rows.ContainsKey(key))
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
