using FluentFTP;

namespace Scada.Comm.Drivers.DrvFtpJP
{
    internal class DebugerFilesReturn
    {
        public DebugerFilesReturn()
        {

        }

        //Получение логов
        public static DebugData OnDebug;
        public delegate void DebugData(FtpProgress progress, string direction);
        //Передача на форму и в файл в папку Log
        public void Log(FtpProgress progress, string direction)
        {
            DebugerFilesLog(progress, direction);
        }

        internal void DebugerFilesLog(FtpProgress progress, string direction)
        {
            if (OnDebug == null)
            {
                return;
            }

            OnDebug(progress, direction);
        }



    }
}
