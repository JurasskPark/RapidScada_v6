using Scada.Lang;

namespace Scada.Comm.Drivers.DrvPingJP
{
    public static class DriverPhrases
    {
        public static string ConfigDirRequired { get; private set; }

        public static void Init()
        {
            LocaleDict dictionary = Locale.GetDictionary("Scada.Comm.Drivers.DrvPingJP.View.Forms.FrmHostSearch");
            ConfigDirRequired = dictionary["DrvPingJPDictionaries"];
        }
    }
}
