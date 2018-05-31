using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;
using Data.Container;
using System.Linq;
using Data.Interoperability;
using Data.Util;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ProcessContainerDemo
{
    public static class pavaniqueue
    {
        private const string FilePath = @"D:\Felcon\Pavani\MapDemo.XML";
        private const string Delimiter = "|";
        private const string FileExtension = "XML";//we can also get this by file name
        [FunctionName("pavaniqueue")]
        public static void Run([QueueTrigger("queuedemo", Connection = "AzureWebJobsStorage")]ContianerCls myQueueItem, TraceWriter log)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationSettings.AppSettings["AzureWebJobsStorage"]);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer ContainerRef = blobClient.GetContainerReference(myQueueItem.ContainerName);
            var BlobRef = ContainerRef.GetBlockBlobReference(myQueueItem.FileName);

            BlobRef.Properties.ContentType = "application/json";

            string logMessage = "";
            using (var memoryStream = new MemoryStream())
            {
                //downloads blob's content to a stream
                BlobRef.DownloadToStream(memoryStream);

                //puts the byte arrays to a string
                logMessage = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
            }

            var XmlHelper = DocumenetFactory.GetDocument(FileExtension.ToLowerInvariant(), FilePath, logMessage, Delimiter);

            LookupTableSet lookupTables = new LookupTableSet();

            XmlDocument configRulesXML = new XmlDocument();
            configRulesXML.LoadXml(XmlHelper.ConvertDataToString());

            MapSet maps = new MapSet();
            maps.SetMappingRules(configRulesXML);

            lookupTables.SetLookupTables(configRulesXML);
            var Ext = myQueueItem.FileName.Split('.');
            var DocumentHelper = DocumenetFactory.GetDocument(Ext.LastOrDefault().ToUpperInvariant(), FilePath, logMessage, Delimiter);

            var SourceDataTable = DocumentHelper.ConvertStringToDataTable();
            DataObject DataContainer = new DataObject(SourceDataTable);
            DataContainer.Map(maps, lookupTables);
            var TargetData = DocumentHelper.ConvertDataToString();

            WriteToBlob(TargetData, Ext.FirstOrDefault());
        }

        private static void WriteToBlob(string delimtedText, string fileName)
        {
            //StorageConnectionString string specified in configuration file
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationSettings.AppSettings["AzureWebJobsStorage"]);
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
    public class ContianerCls
    {
        public string ContainerName { get; set; }
        public string FileName { get; set; }
        public string BlobName { get; set; }
    }
}