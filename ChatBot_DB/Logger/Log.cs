using System;
using System.IO;
using System.Runtime.CompilerServices;


namespace ChatBot_DB
{
    public class Log
    {

        static string _filePath = "C";
        static int _fileCount;

        public static void LogDebug(string message, [CallerMemberName] string method = "")
        {
            DateTime t = DateTime.Now;
            WriteToFile($"DEBUG | Время: {t.TimeOfDay}. Метод: {method}. Событие: {message}");
        }

        public static void LogInfo(string message, Exception ex, [CallerMemberName] string method = "")
        {
            DateTime t = DateTime.Now;
            WriteToFile($"INFO | Время: {t.TimeOfDay}. Метод: {method}. Событие: {message}. Исключение: {ex.Message}");
        }

        public static void LogError(string message, Exception ex, [CallerMemberName] string method = "")
        {
            DateTime t = DateTime.Now;
            WriteToFile($"ERROR | Время: {t.TimeOfDay}. Метод: {method}. Событие: {message}. Исключение: {ex.Message}");
        }

        static void WriteToFile(string message)
        {
            StreamWriter writer = GetFileInfo().AppendText();
            writer.WriteLine(message);
            writer.Close();
        }

        static FileInfo GetFileInfo()
        {
            FileInfo fileInfo = new(_filePath);

            if (_fileCount == 0 || fileInfo.Length >= 30000)
            {
              return CreateNewFile();
            }
            return fileInfo;
        }

        static FileInfo CreateNewFile()
        {
            DateTime time = DateTime.Now;
            _fileCount++;
            _filePath = @$"C:\Users\Admin\source\repos\ChatBot_DB\Log\log_{time.Day}{time.Month}{time.Year}_{_fileCount}.txt";
            FileInfo fileInfo = new(_filePath);
            FileStream fileStream = fileInfo.Create();
            fileStream.Close();
            return fileInfo;
        }
    }
}
