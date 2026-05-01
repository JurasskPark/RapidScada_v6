using Scada.Lang;
using Scada.Web.Lang;
using Scada.Web.Plugins.PlgMimCalendarJP.Code;
using Scada.Web.Plugins.PlgMimic.Components;
using Scada.Web.Services;

namespace Scada.Web.Plugins.PlgMimCalendarJP
{
    /// <summary>
    /// Implements the calendar mimic plugin logic.
    /// <para>Реализует логику плагина календаря для мнемосхем.</para>
    /// </summary>
    public class PlgMimCalendarJPLogic : PluginLogic, IComponentPlugin
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public PlgMimCalendarJPLogic(IWebContext webContext)
            : base(webContext)
        {
            Info = new PluginInfo();
        }

        /// <summary>
        /// Loads the plugin dictionaries.
        /// </summary>
        public override void LoadDictionaries()
        {
            if (!Locale.LoadDictionaries(AppDirs.LangDir, Code, out string errMsg))
            {
                Log.WriteError(WebPhrases.PluginMessage, Code, errMsg);
            }

            PluginPhrases.Init();
        }

        /// <summary>
        /// Gets the component specification used by the mimic plugin.
        /// </summary>
        public IComponentSpec GetComponentSpec(bool editMode)
        {
            return new CalendarComponentSpec();
        }
    }
}
