using System;
using System.IO;
namespace v4posme_library.Domain
{
    public class Logger
    {
        private const string Path = "C:\\logposme";
        public void Log(string logMessage)
        {
            CreateDirectory();
            using (StreamWriter w = File.AppendText(Path + "\\log.txt"))
            {
                w.Write("\r\nLog Entry : ");
                w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                w.WriteLine("  :");
                w.WriteLine($"  :{logMessage}");
                w.WriteLine("-------------------------------");
            }

        }
        private void CreateDirectory()
        {
            try
            {
                if (!Directory.Exists(Path)) Directory.CreateDirectory(Path);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
