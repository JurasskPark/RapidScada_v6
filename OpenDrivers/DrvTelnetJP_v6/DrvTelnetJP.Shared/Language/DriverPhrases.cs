using Scada.Lang;

namespace Scada.Comm.Drivers.DrvTelnetJP
{
    public static class DriverPhrases
    {
        public static string ConfigDirRequired { get; private set; }

        public static void Init()
        {
            LocaleDict dictionary = Locale.GetDictionary("Scada.Comm.Drivers.DrvTelnetJP.View.Forms.FrmConfig");
            ConfigDirRequired = dictionary["DrvTelnetJPDictionaries"];
        }
    }
}
