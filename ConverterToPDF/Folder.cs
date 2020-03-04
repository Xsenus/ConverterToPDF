namespace ConverterToPDF
{
    public class Folder
    {
        public Folder(string folderName, bool isRoot)
        {
            FolderName = folderName;
            IsRoot = isRoot;
        }

        public string FolderName { get; set; }
        public bool IsRoot { get; set; }
    }
}
