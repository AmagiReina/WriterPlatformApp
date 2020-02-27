using System.IO;
using System.Web;
using WriterPlatformApp.WEB.ViewModels;

namespace WriterPlatformApp.WEB.Modules
{
    public class PdfSaveModule
    {
        public HttpPostedFileBase file;
        
        public PdfSaveModule(HttpPostedFileBase file)
        {
            this.file = file;
        }

        public void SaveFileInFolder(string filePath)
        {
            try
            {
                if (file != null && file.ContentLength > 0 
                    && file.ContentType == "application/pdf")
                {
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        file.SaveAs(filePath + GetExtension(file.ContentType));
                    }                 
                } else {
                    filePath = null;
                }               
            }
            catch (IOException ex)
            {
                throw ex;
            }
            
        }

        private string GetExtension(string contentType)
        {
            return MimeTypes.MimeTypeMap.GetExtension(contentType);
        }

        public string GenerateFileName(TitleViewModel title)
        {
            string fileName = title.TitleName;
            return fileName;
        }

        public string GenerateFilePath(string fileName, string pdfSaveDir)
        {
            string path = pdfSaveDir + fileName;
            return path;
        }

        public static byte[] ReadFile(string path)
        {
            return File.ReadAllBytes(path);
        }

        public string GetPath(string pdfSaveDir, string fileName)
        {
            fileName = fileName + GetExtension(file.ContentType);
            return Path.Combine(pdfSaveDir, fileName);
        }
    }
}