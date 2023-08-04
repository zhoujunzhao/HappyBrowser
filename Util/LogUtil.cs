using System.IO;
using System.Text;

namespace HappyBrowser
{
    internal class LogUtil
    {
        public static void Info(Exception e)
        {
            WriteLog("INFO ", e.Message);
        }
        public static void Info(String log)
        {
            WriteLog("INFO ",log);
        }
        public static void Error(Exception e)
        {
            WriteLog("ERROR", e.Message);
        }
        public static void Error(String log)
        {
            WriteLog("ERROR", log);
        }
        public static void WriteLog(string sType,string log)
        {
            try
            {
                string newLog = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} {sType.ToUpper()} {log}";
                string logFile = GetLogPath();
                File.AppendAllLines(logFile,new string[] { newLog, "\r\n" },Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static string GetLogPath()
        {
            string fileName = $"log_{DateTime.Now.ToString("yyyy-MM-dd")}.log";
            string logPath = $"{Application.StartupPath}logs\\{fileName}";
            
            if (!File.Exists(logPath))
            {
                File.CreateText(logPath);
            }
            return logPath;
        }
    }
}
