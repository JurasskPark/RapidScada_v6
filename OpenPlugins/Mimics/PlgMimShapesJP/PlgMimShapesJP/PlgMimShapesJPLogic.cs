using Scada.Lang;
using Scada.Web.Lang;
using Scada.Web.Plugins.PlgMimic.Components;
using Scada.Web.Plugins.PlgMimShapesJP.Code;
using Scada.Web.Services;

namespace Scada.Web.Plugins.PlgMimShapesJP
{
    /// <summary>
    /// Implements the shape mimic plugin logic.
    /// <para>Реализует логику плагина фигур мнемосхем.</para>
    /// </summary>
    public class PlgMimShapesJPLogic : PluginLogic, IComponentPlugin
    {
        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public PlgMimShapesJPLogic(IWebContext webContext)
            : base(webContext)
        {
            Info = new PluginInfo();
        }

        /// <summary>
        /// Loads the plugin dictionaries.
        /// <para>Загружает словари плагина.</para>
        /// </summary>
        public override void LoadDictionaries()
        {
            if (!Locale.LoadDictionaries(AppDirs.LangDir, Code, out string errMsg))
                Log.WriteError(WebPhrases.PluginMessage, Code, errMsg);

            PluginPhrases.Init();
        }

        /// <summary>
        /// Gets the component specification used by the mimic plugin.
        /// <para>Возвращает спецификацию компонентов, используемую плагином мнемосхем.</para>
        /// </summary>
        public IComponentSpec GetComponentSpec(bool editMode)
        {
            return new ShapesComponentSpec();
        }

        #endregion Basic
    }
}
