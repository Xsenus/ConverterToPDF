using iTextSharp.text;
using iTextSharp.text.pdf;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace ConverterToPDF
{
    public partial class ConverterToPDF : Form
    {
        /// <summary>
        /// Список файлов и информации о них.
        /// </summary>
        private List<InfoFile> ListInfoFile { get; set; }

        public ConverterToPDF()
        {
            InitializeComponent();

            txtStartPath.Text = Properties.Settings.Default.StartPath;
            txtEndPath.Text = Properties.Settings.Default.EndPath;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async Task StartAsync(string startPath, string endPath)
        {
            var tokenSource = new CancellationTokenSource();
            CancellationToken ct = tokenSource.Token;
            await Task.Run(() => Convert(startPath, endPath)).ConfigureAwait(false);
        }

        /// <summary>
        /// Получить список файлов и информацию по ним.
        /// </summary>
        /// <param name="startPath"></param>
        public List<InfoFile> GetInfoFile(string startPath, string endPath, bool isSearchAllDirectories = false)
        {
            Message($"Запущен процесс получения списка файлов из каталога: {startPath}.");

            if (string.IsNullOrWhiteSpace(startPath))
            {
                throw new ArgumentException("Значение начального каталога на может быть пустым", nameof(startPath));
            }

            if (string.IsNullOrWhiteSpace(endPath))
            {
                throw new ArgumentException("Значение конечного каталога на может быть пустым", nameof(endPath));
            }

            var result = new List<InfoFile>();

            try
            {
                var filesCollection = default(string[]);

                if (isSearchAllDirectories)
                {
                    filesCollection = Directory.GetFiles(startPath, "*.*", SearchOption.AllDirectories);
                }
                else
                {
                    filesCollection = Directory.GetFiles(startPath);
                }


                foreach (var file in filesCollection)
                {
                    var infoFile = new InfoFile(file, endPath);
                    result.Add(infoFile);
                }

                Message($"Успешно получен список из {filesCollection.Count()} файлов из каталога: {startPath}.");
            }
            catch (Exception)
            {
                Message($"Ошибка получения списка файлов из каталога: {startPath}.");
            }

            return result;
        }

        /// <summary>
        /// Очистка выходного каталога перед конвертацией.
        /// </summary>
        /// <param name="endPath"></param>
        public void ClearDirectory(string endPath)
        {
            try
            {
                Message($"Начало очистки каталога: {endPath}, перед конвертацией.");

                var directorys = Directory.GetDirectories(endPath);
                ProgressBarSetting(directorys.Count());
                foreach (var directory in directorys)
                {
                    Directory.Delete(directory, true);
                    ProgressBarStep();
                }

                var files = Directory.GetFiles(endPath);
                ProgressBarSetting(files.Count());
                foreach (var file in files)
                {
                    File.Delete(file);
                    ProgressBarStep();
                }

                Message($"Успешно произведена очистка каталога: {endPath}.");
            }
            catch (Exception)
            {
                Message($"Ошибка очистки каталога: {endPath}.");
            }
        }

        private async void btnStart_ClickAsync(object sender, EventArgs e)
        {
            txtMessage.Text = string.Empty;
            Message("Начата конвертация файлов.");
            ProgressBarVisible(true);

            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.StartPath))
            {
                MessageBox.Show("Укажите каталог с файлами", "Ошибка каталога");
                txtStartPath.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.EndPath))
            {
                Properties.Settings.Default.EndPath = Directory.GetCurrentDirectory();
                Message($"Путь к конечно директории не был задан. Конечной директорией каталога считается {Properties.Settings.Default.EndPath}.");
            }

            if (!Directory.Exists(Properties.Settings.Default.EndPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.EndPath);
                Message($"Создан каталог: {Properties.Settings.Default.EndPath}.");
            }
            else
            {
                ClearDirectory(Properties.Settings.Default.EndPath);
            }
            Properties.Settings.Default.Save();


            if (checkDirectory.Checked == true)
            {
                try
                {
                    await StartAsync(Properties.Settings.Default.StartPath, Properties.Settings.Default.EndPath).ConfigureAwait(false);
                }
                catch (Exception) { }
            }
            else
            {
                var catalog = Directory.GetDirectories(Properties.Settings.Default.StartPath);

                Message($"В конечном каталоге найдено [{catalog.Count()}] подкаталога.");

                foreach (var item in catalog)
                {
                    var lastFolder = item.Split('\\').Last();
                    var newEndPath = $"{Properties.Settings.Default.EndPath}\\{lastFolder}";

                    if (!Directory.Exists(newEndPath))
                    {
                        Directory.CreateDirectory(newEndPath);
                        Message($"Создан каталог: {newEndPath}.");
                    }

                    try
                    {
                        await StartAsync(item, newEndPath).ConfigureAwait(false);
                    }
                    catch (Exception) { }
                }
            }

            Message("Закончена конвертация файлов.");
            MessageBox.Show("Закончена конвертация файлов", "Конвертация окончена", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ProgressBarVisible(false);
        }

        /// <summary>
        /// Разархивирования ZIP архивов по указанному пути
        /// </summary>
        /// <param name="pathFile">Файл с архивом.</param>
        /// <param name="pathEndFileWithSplit0">Путь до папки распаковки.</param>
        void UnarchivingZIP(string pathFile, string pathEndFileWithSplit0)
        {
            try
            {
                SevenZipNET.SevenZipExtractor zz = new SevenZipNET.SevenZipExtractor(pathFile);
                zz.ExtractAll(pathEndFileWithSplit0);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Разархивирования RAR архивов по указанному пути
        /// </summary>
        /// <param name="pathFile">Файл с архивом.</param>
        /// <param name="pathFileOut">Путь до папки распаковки.</param>
        void UnarchivingRAR(string pathFile, string pathFileOut)
        {
            try
            {
                using (var archive = RarArchive.Open(pathFile))
                {
                    foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                    {
                        entry.WriteToDirectory(pathFileOut, new ExtractionOptions()
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });
                    }
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Обработка архивированных файлов.
        /// </summary>
        /// <param name="listInfoFile"></param>
        private void ProcessingArchivedFiles(List<InfoFile> listInfoFile)
        {
            var workListInfoFile = listInfoFile.Where(w => w.ExpansionFile.Contains("rar") ||
                                                            w.ExpansionFile.Contains("zip") ||
                                                            w.ExpansionFile.Contains("7z")).ToList();

            RenameInfoFileWithSameName(workListInfoFile);

            foreach (var workFile in workListInfoFile)
            {
                if (workFile.ExpansionFile.Contains("7z") || workFile.ExpansionFile.Contains("zip"))
                {
                    UnarchivingZIP(workFile.PathFile, workFile.PathEndFileWithSplit0);
                }
                else if (workFile.ExpansionFile.Contains("rar"))
                {
                    UnarchivingRAR(workFile.PathFile, workFile.PathEndFileWithSplit0);
                }

                var listInfoFileOfAtchive = GetInfoFile(workFile.PathEndFileWithSplit0, workFile.PathEndFileWithSplit0, true);
                ProcessingNonArchivedFiles(listInfoFileOfAtchive, false);
                DeleteUnarchivingFileWithExceptionPDF(listInfoFileOfAtchive);
                ConcatenatePDF(listInfoFileOfAtchive, workFile.FilePathForPDF);
                RenameOutFile(workFile.FilePathForPDF, workFile.Date, workFile.NameSplit[0]);

                try
                {
                    Message($"Удаление временного каталога {workFile.PathEndFileWithSplit0}.");
                    Directory.Delete(workFile.PathEndFileWithSplit0, true);
                }
                catch (Exception)
                {
                    Message($"Удаление временного каталога {workFile.PathEndFileWithSplit0} закончилась с ошибкой.");
                }
            }
        }

        /// <summary>
        /// Удаление разархивированных файлов, за исключением PDF.
        /// </summary>
        /// <param name="listInfoFileOfAtchive">Список информации о обработанных файлах.</param>
        private void DeleteUnarchivingFileWithExceptionPDF(List<InfoFile> listInfoFileOfAtchive)
        {
            Message($"Начался процесс удаления разархивированных файлов. Количество файлов: {listInfoFileOfAtchive.Count()}");
            foreach (var infoFile in listInfoFileOfAtchive)
            {
                try
                {
                    if (File.Exists(infoFile.PathFile))
                    {
                        File.Delete(infoFile.PathFile);
                    }
                }
                catch (Exception)
                {
                    Message($"Ошибка удаления файла: {infoFile.PathFile}");
                }
            }
            Message($"Завершен процесс удаления разархивированных файлов");
        }

        /// <summary>
        /// Обработка не архивированных файлов.
        /// </summary>
        /// <param name="listInfoFile"></param>
        private void ProcessingNonArchivedFiles(List<InfoFile> listInfoFile, bool isRename = true)
        {
            Message($"Начат процесс обработки файлов, не являющимися архивом.");
            var workListInfoFile = listInfoFile.Where(w => !w.ExpansionFile.Contains("rar") &&
                                                            !w.ExpansionFile.Contains("zip") &&
                                                            !w.ExpansionFile.Contains("7z")).ToList();

            RenameInfoFileWithSameName(workListInfoFile);

            ProgressBarSetting(workListInfoFile.Count());

            foreach (var workFile in workListInfoFile)
            {
                if (workFile.ExpansionFile.Contains("docx") || workFile.ExpansionFile.Contains("doc"))
                {
                    WordToPdf(workFile.PathFile, workFile.FilePathForPDF);
                    while (WordApplicationOpen)
                    {
                        break;
                    }
                }
                else if (workFile.ExpansionFile.Contains("jpg") ||
                    workFile.ExpansionFile.Contains("gif") ||
                    workFile.ExpansionFile.Contains("bmp") ||
                    workFile.ExpansionFile.Contains("tif") ||
                    workFile.ExpansionFile.Contains("jpeg") || 
                    workFile.ExpansionFile.Contains("png"))
                {
                    ImagesToPdf(workFile.PathFile, workFile.FilePathForPDF);
                }
                else if (workFile.ExpansionFile.Contains("txt"))
                {
                    TxtToPdf(workFile.PathFile, workFile.FilePathForPDF);
                }
                else if (workFile.ExpansionFile.Contains("pdf"))
                {
                    Copy(workFile.PathFile, workFile.FilePathForPDF);
                }
                else if (workFile.ExpansionFile.Contains("djvu"))
                {
                    DjvuToPdf(workFile.PathFile, workFile.FilePathForPDF);
                }
                else
                {
                    var pathFileOut = $"{workFile.PathEndFile}\\{workFile.FullName}";
                    Copy(workFile.PathFile, pathFileOut);
                    continue;
                }

                if (isRename)
                {
                    RenameOutFile(workFile.FilePathForPDF, workFile.Date, workFile.NameSplit[0]);
                }

                ProgressBarStep();
            }
        }

        /// <summary>
        /// Конвертирование DJVU в PDF.
        /// </summary>
        /// <param name="pathFile">Путь до файла изображения.</param>
        /// <param name="filePathForPDF">Путь к файлу PDF.</param>
        private void DjvuToPdf(string pathFile, string filePathForPDF)
        {
            try
            {
                Message($"Конвертация DJVU файла {pathFile} может занять продолжительное время. Ожидайте.");
                using (var img = Aspose.Imaging.Image.Load(pathFile))
                {
                    img.Save(filePathForPDF, new Aspose.Imaging.ImageOptions.PdfOptions());
                }
                Message($"Конвертация успешно завершена.");
            }
            catch (Exception)
            {
                Message($"Ошибка конвертации DJVU в PDF -> {pathFile}");
            }

            //TODO: 2 метод конвертирования DJVU через платное API
            //try
            //{
            //    var girectoryDJVU = Path.GetDirectoryName(file);
            //    var convertApi = new ConvertApi("dbG9oM2Zh8AM3wnL");
            //    convertApi.ConvertAsync("djvu", "pdf",
            //        new ConvertApiFileParam(file),
            //        new ConvertApiParam("PdfVersion", "1.7")
            //    ).Result.SaveFilesAsync(girectoryDJVU);
            //    File.Move(file.Replace("djvu", "pdf"), $"{girectoryDJVU}\\{newFileName}");
            //}
            //catch (Exception) { }
        }

        /// <summary>
        /// Копирование файла в конечный каталог.
        /// </summary>
        /// <param name="pathFile">Путь до начального файла.</param>
        /// <param name="pathFileOut">Путь до конечного файла.</param>
        private void Copy(string pathFile, string pathFileOut)
        {
            try
            {
                File.Copy(pathFile, pathFileOut, true);
            }
            catch (Exception)
            {
                Message($"Ошибка копирования файла -> {pathFile}");
            }
        }

        /// <summary>
        /// Переименование совпадающей части распарсенного наименования.
        /// </summary>
        /// <param name="workListInfoFile"></param>
        private void RenameInfoFileWithSameName(List<InfoFile> workListInfoFile)
        {
            foreach (var item in workListInfoFile)
            {
                var findFileSameName = workListInfoFile.FindAll(f => string.Compare(f.NameSplit[0], item.NameSplit[0], StringComparison.Ordinal) == 0);

                if (findFileSameName.Count > 1)
                {
                    var i = 1;

                    foreach (var findFile in findFileSameName)
                    {
                        findFile.NameSplit[0] = $"{findFile.NameSplit[0]}{i}";
                        i++;
                    }
                }
            }
        }

        private void Message(string message)
        {
            Invoke((Action)delegate
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(txtMessage.Text))
                    {
                        txtMessage.AppendText($"{message}");
                    }
                    else
                    {
                        txtMessage.AppendText($"{Environment.NewLine}{message}");
                    }
                }
                catch (Exception) { }                
            });
        }

        private void ProgressBarVisible(bool isVisible)
        {
            Invoke((Action)delegate
            {
                try
                {
                    progBar.Visible = isVisible;
                    btnStart.Enabled = !isVisible;
                }
                catch (Exception) { }
            });
        }

        private void ProgressBarSetting(int allRecord)
        {
            Invoke((Action)delegate
            {
                try
                {
                    progBar.Text = $"Всего записей: {allRecord}";
                    progBar.Value = 0;
                    progBar.Step = 1;
                    progBar.Maximum = allRecord;
                    progBar.Minimum = 0;
                }
                catch (Exception) { }
            });
        }

        private void ProgressBarStep()
        {
            Invoke((Action)delegate
            {
                try
                {
                    progBar.PerformStep();
                    progBar.Update();
                }
                catch (Exception) { }
            });
        }

        /// <summary>
        /// При вызове запускает процесс конвертации файлов в PDF.
        /// </summary>
        /// <param name="startPath">Входная директория.</param>
        /// <param name="endPath">Выходная директория.</param>
        private void Convert(string startPath, string endPath)
        {
            ListInfoFile = GetInfoFile(startPath, endPath);
            ProcessingNonArchivedFiles(ListInfoFile);
            ProcessingArchivedFiles(ListInfoFile);
        }

        /// <summary>
        /// Чтение файла PDF, получение количества страниц и переименование конечного файла PDF.
        /// </summary>
        /// <param name="pathFilePDF">Путь до файла PDF.</param>
        public void RenameOutFile(string pathFilePDF, DateTime dateTime, string fileName)
        {
            try
            {
                Message($"Запустился процесс подсчета листов файла: [{pathFilePDF}].");

                if (string.IsNullOrWhiteSpace(pathFilePDF))
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(fileName))
                {
                    return;
                }

                if (!File.Exists(pathFilePDF))
                {
                    return;
                }

                var result = $"{fileName} от {dateTime.ToShortDateString()} (количество листов не определено)";

                var countList = default(string);

                using (PdfReader pdfReader = new PdfReader(pathFilePDF))
                {
                    countList = pdfReader.NumberOfPages.ToString();
                    result = $"{fileName} от {dateTime.ToShortDateString()} на {countList}л";
                }

                var fileReplaceName = pathFilePDF.Replace(fileName, result);

                File.Move(pathFilePDF, fileReplaceName);
                Message($"Окончен процесс подсчета листов файла: [{pathFilePDF}]. Количество: [{countList}].");
            }
            catch (Exception)
            {
                Message($"Ошибка чтения файла PDF -> [{pathFilePDF}].");
            }
        }

        private bool WordApplicationOpen;
        /// <summary>
        /// Конвертирование файла Word в PDF.
        /// </summary>
        /// <param name="wordPath">Путь до файла Word.</param>
        /// <param name="pdfPath">Путь к файлу PDF.</param>
        public void WordToPdf(string wordPath, string pdfPath)
        {
            Message($"Конвертация Word: {wordPath} в PDF.");
            Word.Application appWord = new Word.Application();
            ((Word.ApplicationEvents4_Event)appWord).Quit += WordToPdf_Quit;
            try
            {
                var wordDocument = appWord.Documents.Open(wordPath);
                wordDocument.ExportAsFixedFormat(pdfPath, Word.WdExportFormat.wdExportFormatPDF);
                appWord.Quit();
                Message($"Конвертация успешно завершена.");
            }
            catch (Exception)
            {
                appWord.Quit();
                Message($"Ошибка конвертации Word: {wordPath}.");
            }
        }

        private void WordToPdf_Quit()
        {
            WordApplicationOpen = false;
        }

        /// <summary>
        /// Конвертирование изображений в PDF.
        /// </summary>
        /// <param name="imagePath">Путь до файла изображения.</param>
        /// <param name="pdfPath">Путь к файлу PDF.</param>
        public void ImagesToPdf(string imagePath, string pdfPath)
        {
            try
            {
                Message($"Конвертация изображения: {imagePath} в PDF.");
                iTextSharp.text.Rectangle pageSize = null;

                using (var srcImage = new Bitmap(imagePath))
                {
                    pageSize = new iTextSharp.text.Rectangle(0, 0, srcImage.Width, srcImage.Height);
                }

                using (var ms = new MemoryStream())
                {
                    var document = new Document(pageSize, 0, 0, 0, 0);
                    PdfWriter.GetInstance(document, ms).SetFullCompression();
                    document.Open();
                    var image = iTextSharp.text.Image.GetInstance(imagePath);
                    document.Add(image);
                    document.Close();

                    File.WriteAllBytes(pdfPath, ms.ToArray());
                }
                Message($"Конвертация успешно завершена.");
            }
            catch (Exception)
            {
                Message($"Ошибка конвертации изображения: {imagePath}");
            }
        }

        /// <summary>
        /// Конвертирование TXT в PDF.
        /// </summary>
        /// <param name="txtPath">Путь до файла изображения.</param>
        /// <param name="pdfPath">Путь к файлу PDF.</param>
        public void TxtToPdf(string txtPath, string pdfPath)
        {

            var document = new Document();
            try
            {
                using (var ms = new StreamReader(txtPath))
                {
                    var fs = new FileStream(pdfPath, FileMode.Create);
                    BaseFont baseFont = BaseFont.CreateFont("arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                    iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
                    PdfWriter.GetInstance(document, fs).SetFullCompression();
                    document.Open();
                    document.Add(new Paragraph(ms.ReadToEnd(), font));
                    document.Close();
                }
                Message($"Конвертация успешно завершена.");
            }
            catch (Exception)
            {
                document.Close();
                Message($"Ошибка конвертации TXT в PDF -> {txtPath}");
            }
        }


        /// <summary>
        /// Объединение PDF файлов.
        /// </summary>
        /// <param name="infoFile"></param>
        /// <param name="nameFileOut"></param>
        public void ConcatenatePDF(List<InfoFile> infoFile, string nameFileOut)
        {
            try
            {
                Message($"Начался процесс объединения PDF файлов. Всего файлов: {infoFile.Count()}.");
                using (var ms = new MemoryStream())
                {
                    var outputDocument = new Document();
                    var writer = new PdfCopy(outputDocument, ms);
                    outputDocument.Open();

                    foreach (var file in infoFile)
                    {
                        try
                        {
                            var reader = new PdfReader(file.FilePathForPDF);
                            for (var i = 1; i <= reader.NumberOfPages; i++)
                            {
                                writer.AddPage(writer.GetImportedPage(reader, i));
                            }
                            writer.FreeReader(reader);
                            reader.Close();
                            File.Delete(file.FilePathForPDF);
                        }
                        catch (Exception) { }
                    }

                    writer.Close();
                    outputDocument.Close();
                    var allPagesContent = ms.GetBuffer();
                    ms.Flush();

                    File.WriteAllBytes(nameFileOut, allPagesContent);
                    Message($"Получен итоговый PDF файл: {nameFileOut}.");
                }
            }
            catch (Exception)
            {
                Message($"Ошибка процесса объединения PDF файлов");
            }
        }

        private void btnStartPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog browserDialog = new FolderBrowserDialog())
            {
                if (browserDialog.ShowDialog() == DialogResult.OK)
                {
                    txtStartPath.Text = browserDialog.SelectedPath;
                    Properties.Settings.Default.StartPath = browserDialog.SelectedPath;
                }
            }
        }

        private void btnEndPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog browserDialog = new FolderBrowserDialog())
            {
                if (browserDialog.ShowDialog() == DialogResult.OK)
                {
                    txtEndPath.Text = browserDialog.SelectedPath;
                    Properties.Settings.Default.EndPath = browserDialog.SelectedPath;
                }
            }
        }

        private void txtStartPath_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.StartPath = txtStartPath.Text;
        }

        private void txtEndPath_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.EndPath = txtEndPath.Text;
        }
    }
}
