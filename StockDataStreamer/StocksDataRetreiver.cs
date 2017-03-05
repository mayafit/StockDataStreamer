using System;
using System.IO;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;
using StockDataStreamer.FileHandlers;
using System.Threading;

namespace StockDataStreamer
{
    public class StocksDataRetreiver : ExThread
    {
        private DateTime m_start;
        private DateTime m_end;
        private string m_tempFolder;
        private FileTypes m_fileToParse;
        public event EventHandler Step;
        private void OnStep()
        {
            if(Step != null)
                Step(this , new EventArgs());
        }
        public StocksDataRetreiver(DateTime startDate,FileTypes filesToParse)
        {
            m_start = m_end = startDate;
            m_tempFolder = Path.GetTempPath();
            ThreadMethod = new ThreadStart(RunMethod);
            m_fileToParse = filesToParse;
        }
        public StocksDataRetreiver(DateTime startDate, DateTime endDate, FileTypes filesToParse)
        {
            m_start = startDate;
            m_end = endDate;
            m_tempFolder = Path.GetTempPath();
            ThreadMethod = new ThreadStart(RunMethod);
            m_fileToParse = filesToParse;
        }


        private void RunMethod()
        {
            HttpStreamer streamer = new HttpStreamer(m_tempFolder);
            for (DateTime i = m_start; i <= m_end; )
            {
                string fileName = string.Format("Full{0}{1:00}{2:00}0", i.Year, i.Month, i.Day);
                string fullFileName = fileName + ".zip";
                string fullPath = Path.Combine(m_tempFolder, fullFileName);
                if (streamer.GetStream(fullFileName))
                {
                    DirectoryInfo di = Directory.CreateDirectory(Path.Combine(m_tempFolder, fileName));
                    if (di != null)
                        HandleZipFile(di.FullName, fullPath);
                    ParseFilesInZip(di.FullName, i);
                }
                OnStep();
                i = i.AddDays(1);
            }
        }
        private void ParseFilesInZip(string dir, DateTime date)
        {
            DirectoryInfo di = new DirectoryInfo(dir);
            FileInfo[] files = di.GetFiles();
            TradeResultsFileParser fp = new TradeResultsFileParser((short)date.Year);
            for (int i = 0; i < files.Length; i++)
            {
                string fileName = Path.GetFileNameWithoutExtension(files[i].Name);
                if (fileName.StartsWith("0020") && (m_fileToParse & FileTypes.TradingData) != 0)
                    fp.Parse0020File(Path.Combine(dir, files[i].Name));
                if (fileName.StartsWith("0803") && (m_fileToParse & FileTypes.SecList) != 0)
                    fp.Parse0803File(Path.Combine(dir, files[i].Name));
                if(fileName.StartsWith("0068") && (m_fileToParse & FileTypes.TradingDataExt) != 0)
                    fp.Parse0068File(Path.Combine(dir, files[i].Name),date);
            }
            fp.Close();
        }

        private void HandleZipFile(string targetDir, string fileName)
        {
            FileStream fileStreamIn = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            ZipInputStream zipInStream = new ZipInputStream(fileStreamIn);
            ZipEntry entry = zipInStream.GetNextEntry();
            do
            {
                FileStream fileStreamOut = new FileStream(Path.Combine(targetDir, entry.Name), FileMode.Create,
                                                          FileAccess.Write);
                int size;
                byte[] buffer = new byte[1000];
                do
                {
                    size = zipInStream.Read(buffer, 0, buffer.Length);
                    fileStreamOut.Write(buffer, 0, size);
                } while (size > 0);
                fileStreamOut.Close();
            } while ((entry = zipInStream.GetNextEntry()) != null);
            zipInStream.Close();
            fileStreamIn.Close();
        }

    }
    [Flags]
    public enum FileTypes
    {
        None            = 0,
        TradingData     = 1,
        SecList         = 2,
        TradingDataExt  = 4,
        All             = 4194303
    }

}
