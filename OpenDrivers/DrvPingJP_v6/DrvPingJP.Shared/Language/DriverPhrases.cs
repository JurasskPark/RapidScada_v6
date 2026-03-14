using Scada.Lang;

namespace Scada.Comm.Drivers.DrvPingJP
{
    /// <summary>
    /// Provides driver phrases.
    /// <para>Представление фраз драйвера.</para>
    /// </summary>
    public static class DriverPhrases
    {
        /// <summary>
        /// Gets the phrase that indicates the required configuration directory.
        /// </summary>
        public static string ConfigDirRequired { get; private set; }

        /// <summary>
        /// Initializes phrases from dictionaries.
        /// </summary>
        public static void Init()
        {
            LocaleDict dictionary = Locale.GetDictionary("Scada.Comm.Drivers.DrvPingJP.View.Forms.FrmHostSearch");
            ConfigDirRequired = dictionary["DrvPingJPDictionaries"];
        }
    }
}
