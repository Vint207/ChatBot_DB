using System;
using System.IO;
using System.Runtime.CompilerServices;


namespace ChatBot_DB
{
    public class Log
    {

        static string _filePath;
        static int _fileCount;

        public static void LogDebug(string message, Exception ex, [CallerMemberName] string method = "")
        {
            WriteToFile("DEBUG", ex, message, method);
        }

        public static void LogInfo(string message, Exception ex, [CallerMemberName] string method = "")
        {
            WriteToFile("Info", ex, message, method);
        }

        public static void LogError(string message, Exception ex, [CallerMemberName] string method = "")
        {
            WriteToFile("Error", ex, message, method);
        }

        static void WriteToFile(string logLevel, Exception ex, string message, string method)
        {
            DateTime dateTime = DateTime.Now;         

            if (_filePath==null)
            {
                _filePath = @$"C:\Users\Admin\source\repos\ChatBot_DB\Log\log_{dateTime.Year}{dateTime.Month}{dateTime.Day}_{_fileCount}.txt";
            }
            FileInfo fileInfo = new(_filePath);

            if (!fileInfo.Exists || fileInfo.Length >= 30000)
            {
                _fileCount++;
                fileInfo = new(_filePath);
                FileStream fileStream = fileInfo.Create();
                fileStream.Close();
            }
            StreamWriter writer = fileInfo.AppendText();
            writer.WriteLine($"{logLevel}: Время {dateTime.TimeOfDay}. Метод {method}. {ex.Message} {message}");
            writer.Close();
        }
    }
}
