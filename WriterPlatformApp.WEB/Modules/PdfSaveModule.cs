using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using WriterPlatformApp.WEB.ViewModels;

namespace WriterPlatformApp.WEB.Modules
{
    public class PdfSaveModule
    {
        public HttpPostedFileBase file;
        private static readonly string blobStorage = ConfigurationManager.ConnectionStrings["BlobStorage"].ConnectionString;
        private static readonly CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blobStorage);
        private const string CONTENT_TYPE = "application/pdf";
        private const string CONTAINER_NAME = "pdfcontainer";

        public PdfSaveModule(HttpPostedFileBase file)
        {
            this.file = file;   
        }

     
        public void Upload(TitleViewModel title)
        {
           // Get name of file from ViewModel
           string fileName = title.TitleName;

           // Accessing Azure Container
           CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
           CloudBlobContainer blobContainer = blobClient.GetContainerReference(CONTAINER_NAME);
            
           // Get Permissions For Actions in Container
           blobContainer.SetPermissions(
           new BlobContainerPermissions
           {
               PublicAccess = BlobContainerPublicAccessType.Blob
           });

           // Getting list of files in container
           var list = blobContainer.ListBlobs();

           // Setting a name for file 
           string fileToUpload = Path.GetFileName(fileName + MimeTypes.MimeTypeMap.GetExtension(CONTENT_TYPE));

           // Creating a new file and uploading to container
           CloudBlockBlob blob = blobContainer.GetBlockBlobReference(fileToUpload);
           blob.Properties.ContentType = file.ContentType;
           blob.UploadFromStream(file.InputStream);

           // Get a path to store in db
           string filePath = list.OfType<CloudBlockBlob>().Where(x => x.Name == fileToUpload).Select(x => x.StorageUri.PrimaryUri).FirstOrDefault().ToString();
           title.ContentPath = filePath;
        }

        public static byte[] ReadFile(string fileName)
        {
            // Accessing Azure Container
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(CONTAINER_NAME);

            // Getting file from container
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(fileName + MimeTypes.MimeTypeMap.GetExtension(CONTENT_TYPE));
            blob.FetchAttributes();

            // Getting blob length
            long fileByteLength = blob.Properties.Length;
            
            // Creating byte array
            byte[] result = new byte[fileByteLength];

            // Downloading blob to byte array
            blob.DownloadToByteArray(result, 0);

            return result;
        }

    }
}