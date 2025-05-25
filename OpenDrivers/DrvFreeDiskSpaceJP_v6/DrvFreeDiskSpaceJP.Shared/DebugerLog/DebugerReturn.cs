namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP
{
    internal class DebugerReturn
    {
        public DebugerReturn()
        {

        }

        //Получение логов
        public static DebugData OnDebug;
        public delegate void DebugData(string msg);
        //Передача на форму и в файл в папку Log
        internal void DebugerLog(string text)
        {
            if (OnDebug == null)
            {
                return;
            }

            OnDebug(text);
        }

        public void Log(string text)
        {
            DebugerLog(text);
        }

    }
}
