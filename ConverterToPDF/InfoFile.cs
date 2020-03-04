using System;
using System.IO;

namespace ConverterToPDF
{
    /// <summary>
    /// Информация о исполняемом файле.
    /// </summary>
    public class InfoFile
    {
        /// <summary>
        /// Информация о исполняемом файле.
        /// </summary>
        /// <param name="pathFile">Полный путь до файла.</param>
        public InfoFile(string pathFile, string pathEndFile)
        {
            if (string.IsNullOrWhiteSpace(pathFile))
            {
                throw new ArgumentException("Полный путь до файла не может быть пустым.", nameof(pathFile));
            }

            if (string.IsNullOrWhiteSpace(pathEndFile))
            {
                throw new ArgumentException("Значение конечного каталога на может быть пустым", nameof(pathEndFile));
            }

            PathFile = pathFile;
            PathEndFile = pathEndFile;
            Name = Path.GetFileNameWithoutExtension(pathFile);
            NameSplit = Name.Split('_');
            FullName = Path.GetFileName(pathFile);
            DirectoryFile = Path.GetDirectoryName(pathFile);
            ExpansionFile = Path.GetExtension(pathFile).Trim().ToLower();

            if (File.Exists(pathFile))
            {
                Date = File.GetCreationTime(pathFile);
            }
            else
            {
                Date = DateTime.Now;
            }
        }

        /// <summary>
        /// Имя файла (без расширения)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата файла.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Имя файла с расширением.
        /// </summary>
        public string FullName { get; }

        /// <summary>
        /// Путь до файла.
        /// </summary>
        public string PathFile { get; }

        /// <summary>
        /// Путь до папки вывода файла.
        /// </summary>
        public string PathEndFile { get; }

        /// <summary>
        /// Каталог в котором находится файл.
        /// </summary>
        public string DirectoryFile { get; }

        /// <summary>
        /// Расширение файла.
        /// </summary>
        public string ExpansionFile { get; set; }

        /// <summary>
        /// Массив созданный на основе имени файла Split('_')
        /// </summary>
        public string[] NameSplit { get; set; }

        /// <summary>
        /// Имя для PDF файла.
        /// </summary>
        public string FileNameForPDF => $"{NameSplit[0]}.pdf";

        public string FilePathForPDF => $"{PathEndFile}\\{FileNameForPDF}";

        /// <summary>
        /// Путь для разархивирования архивов.
        /// </summary>
        public string PathEndFileWithSplit0 => $"{PathEndFile}\\{NameSplit[0]}";

        public override string ToString()
        {
            return FullName;
        }
    }
}
