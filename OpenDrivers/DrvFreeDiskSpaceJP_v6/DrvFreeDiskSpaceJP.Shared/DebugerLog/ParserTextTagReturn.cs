using System;
using System.Collections.Generic;
using System.Text;

namespace Scada.Comm.Drivers.DrvTextParserInDatabaseJP
{
    internal class ParserTextTagReturn
    {
        public ParserTextTagReturn()
        {

        }

        //Получение тегов
        public static DebugData OnDebug;
        public delegate void DebugData(List<ParserTextTag> tags);
        //Передача на форму
        internal void TagReturn(List<ParserTextTag> tags)
        {
            if (OnDebug == null)
            {
                return;
            }

            OnDebug(tags);
        }

        public void Return(List<ParserTextTag> tags)
        {
            TagReturn(tags);
        }
    }
}
