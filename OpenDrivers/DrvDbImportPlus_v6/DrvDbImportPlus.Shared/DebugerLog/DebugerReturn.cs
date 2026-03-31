namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    internal class DebugerReturn
    {
        public DebugerReturn()
        {

        }

        //Получение логов
        public static DebugData OnDebug;
        public delegate void DebugData(string msg, bool writeDateTime = true);
        //Передача на форму и в файл в папку Log
        internal void DebugerLog(string text, bool writeDateTime = true)
        {
            if (OnDebug == null)
            {
                return;
            }

            OnDebug(text);
        }

        public void Log(string text, bool writeDateTime = true)
        {
            DebugerLog(text, writeDateTime);
        }

    }
}
