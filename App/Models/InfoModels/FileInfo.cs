namespace rp.Accounting.App.Models.InfoModels
{
    public class FileInfo
    {
        public string FileName { get; set; }
        public string Url { get; set; }

        public FileInfo(string fileName, string url)
        {
            FileName = fileName;
            Url = url;
        }
    }
}
