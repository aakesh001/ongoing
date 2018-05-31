using System;
using System.Data;
using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace AppConfigurations
{
    public class MapSet
    {
        private List<Map> maps;

        public List<Map> Maps
        {
            get
            {
                return maps;
            }
        }

        public MapSet()
        {
            maps = new List<Map>();
        }

        public void SetMappingRules(String JSON)
        {
            //TODO: JSON Implementation
        }

        public void SetMappingRules(XmlDocument XML)
        {
            foreach (XmlNode mapNodes in XML.GetElementsByTagName("map"))
            {
                foreach (XmlNode map in mapNodes.ChildNodes)
                {
                    if (map.Name != "#comment")
                    {
                        Map mapObject = new Map(map.Name);
                        Dictionary<String, String> parameters = new Dictionary<String, String>();
                        foreach (XmlNode mapParameter in map.ChildNodes)
                        {
                            parameters.Add(mapParameter.Name, mapParameter.InnerText);
                        }
                        mapObject.SetParameters(parameters);
                        maps.Add(mapObject);
                    }
                }
                break; //We will take only the first <map>
            }
        }
    }

    public class Map
    {
        protected String mapAction;
        protected Dictionary<String, String> parameters;

        public Map(String mapAction)
        {
            this.mapAction = mapAction;
        }

        public Map(String mapAction, Dictionary<String, String> parameters)
        {
            this.mapAction = mapAction;
            SetParameters(parameters);
        }

        public String Action
        {
            get
            {
                return mapAction;
            }
        }

        public Dictionary<String, String> Parameters
        {
            get
            {
                return parameters;
            }
        }

        public void SetParameters(Dictionary<String, String> parameters)
        {
            this.parameters = parameters;
        }

        public bool HasParameter(String parameterName)
        {
            if (Parameters.ContainsKey(parameterName))
            {
                String parameterValue = String.Empty;
                parameterValue = Parameters[parameterName];
                if (parameterValue.Equals(String.Empty))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public String GetParameter(String parameterName)
        {
            if (HasParameter(parameterName))
            {
                return Parameters[parameterName];
            }
            return String.Empty;
        }

        public String GetParameter(String parameterName, String defaultValue)
        {
            if (HasParameter(parameterName))
            {
                return Parameters[parameterName];
            }
            return defaultValue;
        }
    }
}