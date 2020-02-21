using System.IO;

namespace WriterPlatformApp.WEB.Modules
{
    public class PdfSaveModule
    {
        public readonly string pdfExtension = ".pdf";
        public string fileName;
        public string pdfSaveDir;

        public PdfSaveModule(string fileName, string pdfSaveDir)
        {
            this.fileName = fileName + pdfExtension;
            this.pdfSaveDir = pdfSaveDir;
        }

        public static byte[] ReadFile(string path)
        {
            return File.ReadAllBytes(path);
        }

        public string GetPath()
        {
            return Path.Combine(pdfSaveDir, fileName);
        }
    }
}