using System;
using System.IO;
using System.Windows.Forms;

namespace Scada.Comm.Drivers.DrvPing
{
    public class Debuger
    {
        static int LogDays = 10;

        public static void Debug(string text)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\Logs\";

            try
            {
                Directory.CreateDirectory(path);
                StreamWriter streamWriter = File.AppendText(path + DateTime.Now.ToString("yyyy-MM-dd") + "_Debug" + ".txt");
                streamWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " - " + text);
                streamWriter.Close();
            }
            catch { }

            //Очистка истории логов
            try
            {
                //Проверим сколько файлов в каталоге
                string[] ListFilesLog = Directory.GetFiles(path, "*.txt");
                if (ListFilesLog.Length > LogDays) //Если файлов больше 10
                {
                    Clear(LogDays);
                }
            }
            catch { }
        }


        public static void LogException(string text)
        {

            string path = AppDomain.CurrentDomain.BaseDirectory + @"\Logs\";

            try
            {
                Directory.CreateDirectory(path);
                StreamWriter streamWriter = File.AppendText(path + DateTime.Now.ToString("yyyy-MM-dd") + "_Exception" + ".txt");
                streamWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " - " + text);
                streamWriter.Close();
            }
            catch { }

            //Очистка истории логов
            try
            {
                //Проверим сколько файлов в каталоге
                string[] ListFilesLog = Directory.GetFiles(path, "*.txt");
                if (ListFilesLog.Length > LogDays) //Если файлов больше 10
                {
                    Clear(LogDays);
                }
            }
            catch { }
        }

        public static void Log(string text, Guid channel_id)
        {
            string id = channel_id.ToString();
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\Logs\";

            try
            {
                Directory.CreateDirectory(path);
                StreamWriter streamWriter = File.AppendText(path + DateTime.Now.ToString("yyyy-MM-dd") + "_"+ id + ".txt");
                streamWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " - " + text);
                streamWriter.Close();
            }
            catch { }

            //Очистка истории логов
            try
            {
                //Проверим сколько файлов в каталоге
                string[] ListFilesLog = Directory.GetFiles(path, "*.txt");
                if (ListFilesLog.Length > LogDays) //Если файлов больше 10
                {
                    Clear(LogDays);
                }
            }
            catch { }
        }

        public static void Clear(int days)
        {
            string path = string.Empty;
            string[] files;

            path = AppDomain.CurrentDomain.BaseDirectory + @"\Logs\";
            if (!Directory.Exists(path))
            {
                return;
            }

            files = Directory.GetFiles(path);

            try
            {
                for (int index1 = 0; index1 < files.Length; ++index1)
                {
                    bool flag = true;

                    for (int index2 = 0; index2 < days; ++index2)
                    {
                        if (files[index1] == path + DateTime.Now.AddDays((double)-index2).ToString("yyyy-MM-dd") + ".txt")
                        {
                            flag = false;
                        }
                    }

                    if (flag)
                    {
                        File.Delete(files[index1]);
                    }
                }
            }
            catch
            {

            }
        }
    }
}
