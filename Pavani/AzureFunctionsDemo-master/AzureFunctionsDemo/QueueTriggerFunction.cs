using System;
using System.IO;
using System.Xml;
using Microsoft.Azure.WebJobs;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Text;
using CsvHelper;
using Data.Util;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;
using Data.Interoperability;
using Data.Container;

namespace AzureFunctionsDemo
{
    public static class QueueTriggerFunction
    {
        private const string FilePath = @"D:\Felcon\Pavani\MapDemo2.json";
        private const string Delimiter = "|";
        private const string FileExtension = "XML";//we can also get this by file name

        [FunctionName("QueueTriggerFunction")]
        public static void WriteLog([BlobTrigger("pavaniblog2/{name}.{ext}")] string logMessage, string name, string ext, TextWriter logger)
        {
            var XmlHelper = DocumenetFactory.GetDocument(FileExtension.ToLowerInvariant(), FilePath, logMessage, Delimiter);

            LookupTableSet lookupTables = new LookupTableSet();

            lookupTables.SetLookupTables<string>(XmlHelper.ConvertDataToString());

            XmlDocument configRulesXML = new XmlDocument();
            configRulesXML.LoadXml(XmlHelper.ConvertDataToString());

            MapSet maps = new MapSet();
            maps.SetMappingRules(configRulesXML);

            lookupTables.SetLookupTables(configRulesXML);

            var DocumentHelper = DocumenetFactory.GetDocument(ext.ToUpperInvariant(), FilePath, logMessage, Delimiter);

            var SourceDataTable = DocumentHelper.ConvertStringToDataTable();
            DataObject DataContainer = new DataObject(SourceDataTable);
            DataContainer.Map(maps, lookupTables);
            var TargetData = DocumentHelper.ConvertDataToString();

            WriteToBlob(TargetData, name);
        }

        private static void WriteToBlob(string delimtedText, string fileName)
        {
            //StorageConnectionString string specified in configuration file
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["AzureWebJobsStorage"]);
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            //specified container name
            CloudBlobContainer container = blobClient.GetContainerReference("pavanitarget");
            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();
            container.SetPermissions(
            new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            var currentDate = DateTime.Now;
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName + "_" + currentDate.ToString("MMddyyyyhhmmssfff"));
            byte[] ByteArray = Encoding.UTF8.GetBytes(delimtedText);
            MemoryStream stream = new MemoryStream(ByteArray);
            StreamReader reader = new StreamReader(stream);

            blockBlob.UploadFromStream(stream);
        }
    }
}