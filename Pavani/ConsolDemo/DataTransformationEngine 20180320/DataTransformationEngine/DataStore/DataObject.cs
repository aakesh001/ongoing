using System;
using System.Data;
using System.Collections.Generic;
using AppConfigurations;

namespace DataStore
{
    //Base DataObject class for the transformation engine
    //Contains a DataTable to hold the data
    //Trying out a MetaInfo to store properties of column (may not be needed)
    public class DataObject
    {
        protected DataTable dataTable = new DataTable();
        protected List<DataObjectColumnMetaInfo> metaInfo = new List<DataObjectColumnMetaInfo>();

        protected String SOURCE_FIELD_NAME = "sourceFieldName";
        protected String SOURCE_FIELD_NAME_1 = "sourceFieldName1";
        protected String SOURCE_FIELD_NAME_2 = "sourceFieldName2";
        protected String TARGET_FIELD_NAME = "targetFieldName";
        protected String VALUE = "value";
        protected String SOURCE_FORMAT = "sourceFormat";
        protected String TARGET_FORMAT = "targetFormat";
        protected String DEFAULT_ACTION = "defaultAction";
        protected String LOOKUP_TABLE = "lookupTable";
        protected String SEPARATOR = "separator";

        /// <summary>
        /// Creating a new DataObject with a Name
        /// </summary>
        /// <param name="tableName"></param>
        public DataObject(String tableName)
        {
            dataTable = new DataTable(tableName);
            initializeMetaInfo();
        }

        /// <summary>
        /// Creating a new DataObject using a DataTable 
        /// </summary>
        /// <param name="dTbl"></param>
        public DataObject(DataTable dTbl)
        {
            dataTable = dTbl;
            dataTable.TableName = "Default";
            initializeMetaInfo();
        }

        /// <summary>
        /// Initializing MetaInfo
        /// </summary>
        private void initializeMetaInfo()
        {
            for (int column = 0; column < dataTable.Columns.Count; column++)
            {
                DataObjectColumnMetaInfo columnMetaInfo = new DataObjectColumnMetaInfo();
                columnMetaInfo.columnName = dataTable.Columns[column].ColumnName;
                metaInfo.Add(columnMetaInfo);
            }
        }

        /// <summary>
        /// Getting the DataTable
        /// </summary>
        public DataTable Data
        {
            get
            {
                return dataTable;
            }
        }

        //Adding a new column, no type defined, no data
        protected void AddColumn(String columnName)
        {
            //Add Column to DataTable
            DataColumn dataColumn = new DataColumn(columnName);
            dataTable.Columns.Add(dataColumn);
            //Add Column Meta Info
            DataObjectColumnMetaInfo columnMetaInfo = new DataObjectColumnMetaInfo();
            columnMetaInfo.columnName = columnName;
            metaInfo.Add(columnMetaInfo);
        }

        public void Map(MapSet mapSet, LookupTableSet lookupTableSet)
        {
            foreach (Map map in mapSet.Maps)
            {
                Console.WriteLine("Performing :" + map.Action + "\tAdding : " + map.GetParameter(TARGET_FIELD_NAME)); //REMOVE

                //TODO: Validate - Target Field Name must be present on the map parameters                 
                switch (map.Action)
                {
                    case "copy":
                        Copy(map.GetParameter(SOURCE_FIELD_NAME), map.Parameters[TARGET_FIELD_NAME], map.Parameters[VALUE]);
                        break;
                    case "assign":
                        Assign(map.GetParameter(TARGET_FIELD_NAME), map.GetParameter(VALUE));
                        break;
                    case "lookup":
                        Lookup(map.GetParameter(SOURCE_FIELD_NAME),
                              map.GetParameter(TARGET_FIELD_NAME),
                              lookupTableSet.LookupTables[map.GetParameter(LOOKUP_TABLE)],
                              map.GetParameter(DEFAULT_ACTION),
                              map.GetParameter(VALUE));
                        break;
                    case "formatDate":
                        FormatDate(map.GetParameter(SOURCE_FIELD_NAME),
                             map.GetParameter(TARGET_FIELD_NAME),
                             map.GetParameter(SOURCE_FORMAT),
                             map.GetParameter(TARGET_FORMAT));
                        break;
                    case "concat":
                        Concat(map.GetParameter(SOURCE_FIELD_NAME_1),
                             map.GetParameter(SOURCE_FIELD_NAME_2),
                             map.GetParameter(TARGET_FIELD_NAME),
                             map.GetParameter(VALUE));
                        break;
                }
            }
        }

        ///<summary>
        ///Copy field to target, assign a default value if source feld is unavailable or null.
        ///</summary>
        public void Copy(String sourceFieldName, String targetFieldName, String value)
        {
            DataColumn newColumn = new DataColumn(targetFieldName);
            if (dataTable.Columns.Contains(sourceFieldName))
            {
                String expression = "IIF([" + sourceFieldName + "]<>'', [" + sourceFieldName + "], \'" + value + "\')";
                newColumn.Expression = expression;
                dataTable.Columns.Add(newColumn);
            }
            else
            {
                Assign(targetFieldName, value);
            }
        }

        ///<summary>
        ///Assign a value to target field.
        ///</summary>
        public void Assign(String targetFieldName, String value)
        {
            DataColumn newColumn = new DataColumn(targetFieldName);
            String expression = "\'" + value + "\'";
            newColumn.Expression = expression;//TODO: Handling XML escape sequences, and \ " ' on value.
            dataTable.Columns.Add(newColumn);
        }

        ///<summary>
        ///Lookup a table to derive target field value from source.
        ///</summary>
        /// defaultAction:
        /// copy	  - Copies the source field into the target field if the lookup
        ///             yields no result.
        /// assign    - Assigns the value in <value> if specified if lookup returns
        ///             no result.Sets null if <value> is missing.
        /// null	  - Assigns null value to target.This is the default, if default 
        ///             is not specified.
        public void Lookup(String sourceFieldName, String targetFieldName, LookupTable lookupTable, String defaultAction, String value)
        {
            AddColumn(targetFieldName);
            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                String defaultValue = String.Empty;
                switch (defaultAction)
                {
                    case "copy":
                        defaultValue = dataTable.Rows[row][sourceFieldName].ToString();
                        break;
                    case "value":
                        defaultValue = value;
                        break;
                    default:
                        break;
                }
                dataTable.Rows[row][targetFieldName] = lookupTable.Lookup(dataTable.Rows[row][sourceFieldName].ToString(), defaultValue);

            }
        }

        public void Concat(String sourceFieldName1, String sourceFieldName2, String targetFieldName, String seprator)
        {
            DataColumn newColumn = new DataColumn(targetFieldName);
            String expression = "[" + sourceFieldName1 + "] + \' \' + [" + sourceFieldName2 + "]";
            newColumn.Expression = expression;
            dataTable.Columns.Add(newColumn);
        }

        public void FormatDate(String sourceFieldName, String targetFieldName, String sourceDateFormat, String targetDateFormat)
        {
            String sourceFieldValue = String.Empty;
            String targetFieldValue = String.Empty;
            String ccyy = String.Empty;
            String MM = String.Empty;
            String dd = String.Empty;
            if (sourceDateFormat == targetDateFormat)
            {
                Copy(sourceFieldName, targetFieldName, "");
                return;
            }
            else
            {
                AddColumn(targetFieldName);
                for (int row = 0; row < dataTable.Rows.Count; row++)
                {
                    sourceFieldValue = dataTable.Rows[row][sourceFieldName].ToString() + "          ";
                    switch (sourceDateFormat)
                    {
                        case "ccyyMMdd":
                            ccyy = sourceFieldValue.Substring(0, 4);
                            MM = sourceFieldValue.Substring(4, 2);
                            dd = sourceFieldValue.Substring(6, 2);
                            break;
                        case "ccyy-MM-dd":
                            ccyy = sourceFieldValue.Substring(0, 4);
                            MM = sourceFieldValue.Substring(5, 2);
                            dd = sourceFieldValue.Substring(8, 2);
                            break;
                        case "MM/dd/ccyy":
                        case "MM-dd-ccyy":
                            ccyy = sourceFieldValue.Substring(6, 4);
                            MM = sourceFieldValue.Substring(0, 2);
                            dd = sourceFieldValue.Substring(3, 2);
                            break;
                        case "dd/MM/ccyy":
                        case "dd-MM-ccyy":
                            ccyy = sourceFieldValue.Substring(6, 4);
                            MM = sourceFieldValue.Substring(3, 2);
                            dd = sourceFieldValue.Substring(0, 2);
                            break;
                        default:
                            //TODO: Action on bad source date format
                            break;
                    }

                    switch (targetDateFormat)
                    {
                        case "ccyyMMdd":
                            targetFieldValue = ccyy + MM + dd;
                            break;
                        case "ccyy-MM-dd":
                            targetFieldValue = ccyy + "-" + MM + "-" + dd;
                            break;
                        case "MM/dd/ccyy":
                            targetFieldValue = MM + "/" + dd + "/" + ccyy;
                            break;
                        case "MM-dd-ccyy":
                            targetFieldValue = MM + "-" + dd + "-" + ccyy;
                            break;
                        case "dd/MM/ccyy":
                            targetFieldValue = dd + "/" + MM + "/" + ccyy;
                            break;
                        case "dd-MM-ccyy":
                            targetFieldValue = dd + "-" + MM + "-" + ccyy;
                            break;
                        default:
                            //TODO: Action on bad source date format
                            break;
                    }
                    dataTable.Rows[row][targetFieldName] = targetFieldValue;
                }
            }
        }


        public void FormatDateDeprecated(String sourceFieldName, String targetFieldName, String sourceDateFormat, String targetDateFormat)
        {
            int ccyyPosition = 0;
            int MMPosition = 0;
            int ddPosition = 0;
            switch (sourceDateFormat)
            {
                case "ccyyMMdd":
                    ccyyPosition = 0;
                    MMPosition = 4;
                    ddPosition = 6;
                    break;

                case "ccyy-MM-dd":
                    ccyyPosition = 0;
                    MMPosition = 5;
                    ddPosition = 8;
                    break;

                case "MM/dd/ccyy":
                case "MM-dd-ccyy":
                    ccyyPosition = 6;
                    MMPosition = 0;
                    ddPosition = 3;
                    break;

                case "dd/MM/ccyy":
                case "dd-MM-ccyy":
                    ccyyPosition = 6;
                    MMPosition = 3;
                    ddPosition = 0;
                    break;
            }
            String expression = "\'\'";
            switch (targetDateFormat)
            {
                case "ccyyMMdd":
                    expression = "SUBSTRING([" + sourceFieldName + "]+\'          \'," + ccyyPosition + ",4) + " +
                                 "SUBSTRING([" + sourceFieldName + "]+\'          \'," + MMPosition + ",2) + " +
                                 "SUBSTRING([" + sourceFieldName + "]+\'          \'," + ddPosition + ",2)";
                    break;
                case "ccyy-MM-dd":
                    expression = "SUBSTRING([" + sourceFieldName + "]+\'          \'," + ccyyPosition + ",4) + \'-\' + " +
                                 "SUBSTRING([" + sourceFieldName + "]+\'          \'," + MMPosition + ",2) + \'-\' + " +
                                 "SUBSTRING([" + sourceFieldName + "]+\'          \'," + ddPosition + ",2)";
                    break;
                case "MM/dd/ccyy":
                    expression = "SUBSTRING([" + sourceFieldName + "]+\'          \'," + MMPosition + ",2) + \'\\\' + " +
                                 "SUBSTRING([" + sourceFieldName + "]+\'          \'," + ddPosition + ",2) + \'\\\' + " +
                                 "SUBSTRING([" + sourceFieldName + "]+\'          \'," + ccyyPosition + ",4)";
                    break;
                case "MM-dd-ccyy":
                    expression = "SUBSTRING([" + sourceFieldName + "]+\'          \'," + MMPosition + ",2) + \'-\' + " +
                                 "SUBSTRING([" + sourceFieldName + "]+\'          \'," + ddPosition + ",2) + \'-\' + " +
                                 "SUBSTRING([" + sourceFieldName + "]+\'          \'," + ccyyPosition + ",4)";
                    break;
                case "dd/MM/ccyy":
                    expression = "SUBSTRING([" + sourceFieldName + "]+\'          \'," + ddPosition + ",2) + \'\\\' + " +
                                 "SUBSTRING([" + sourceFieldName + "]+\'          \'," + MMPosition + ",2) + \'\\\' + " +
                                 "SUBSTRING([" + sourceFieldName + "]+\'          \'," + ccyyPosition + ",4)";
                    break;
                case "dd-MM-ccyy":
                    expression = "SUBSTRING([" + sourceFieldName + "]+\'          \'," + ddPosition + ",2) + \'-\' + " +
                                 "SUBSTRING([" + sourceFieldName + "]+\'          \'," + MMPosition + ",2) + \'-\' + " +
                                 "SUBSTRING([" + sourceFieldName + "]+\'          \'," + ccyyPosition + ",4)";
                    break;
                default:
                    //TODO: Action on bad source date format
                    break;
            }
            Console.WriteLine(expression);
            DataColumn newColumn = new DataColumn(targetFieldName);
            newColumn.Expression = expression;
            try
            {
                dataTable.Columns.Add(newColumn);
            }
            catch (ArgumentOutOfRangeException argOutOfRange){
                Console.WriteLine(argOutOfRange);
            }
        }


        public virtual void Transform()
        {

        }
    }

}