using System;
using System.IO;
using System.Text;

namespace EppKey
{
    public static class Logger
    {
        public static void Log(string content)
        {
            try
            {
                File.AppendAllText($"EppKeyLogs-{DateTime.Today:yyyyMMdd}.txt", $"[{DateTime.Today:yyyyMMdd_HHmmss}]: {content}{Environment.NewLine}" , Encoding.UTF8);
            }
            catch (Exception)
            {
                Console.WriteLine(content);
            }
        }
    }
}
