using System;
using System.Collections.Generic;
using System.Text;

namespace Scada.Comm.Drivers.DrvFtpJP
{
    internal class DriverTagReturn
    {
        public DriverTagReturn()
        {

        }

        //Получение тегов
        public static DebugData OnDebug;
        public delegate void DebugData(List<DriverTag> tags);
        //Передача на форму
        internal void TagReturn(List<DriverTag> tags)
        {
            if (OnDebug == null)
            {
                return;
            }

            OnDebug(tags);
        }

        public void Return(List<DriverTag> tags)
        {
            TagReturn(tags);
        }
    }
}
