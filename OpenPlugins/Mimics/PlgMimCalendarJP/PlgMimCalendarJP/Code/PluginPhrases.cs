using Scada.Lang;

namespace Scada.Web.Plugins.PlgMimCalendarJP.Code
{
    /// <summary>
    /// Provides access to localized phrases of the plugin.
    /// <para>Предоставляет доступ к локализованным фразам плагина.</para>
    /// </summary>
    internal class PluginPhrases
    {
        /// <summary>
        /// Gets the name of the calendar component group.
        /// <para>Возвращает название группы компонентов календаря.</para>
        /// </summary>
        public static string CalendarGroup { get; private set; }

        /// <summary>
        /// Gets the name of the auto calendar component.
        /// <para>Возвращает название компонента автоматического календаря.</para>
        /// </summary>
        public static string CalendarAutoComponent { get; private set; }

        /// <summary>
        /// Gets the name of the input calendar component.
        /// <para>Возвращает название компонента ввода даты.</para>
        /// </summary>
        public static string CalendarInputComponent { get; private set; }

        /// <summary>
        /// Gets the name of the button calendar component.
        /// <para>Возвращает название компонента календаря с кнопкой.</para>
        /// </summary>
        public static string CalendarButtonComponent { get; private set; }

        /// <summary>
        /// Gets the name of the range calendar component.
        /// <para>Возвращает название компонента диапазона дат.</para>
        /// </summary>
        public static string CalendarRangeComponent { get; private set; }

        /// <summary>
        /// Gets the name of the range bottom calendar component.
        /// <para>Возвращает название компонента диапазона дат с кнопкой снизу.</para>
        /// </summary>
        public static string CalendarRangeBottomComponent { get; private set; }

        /// <summary>
        /// Gets the name of the range side calendar component.
        /// <para>Возвращает название компонента диапазона дат с кнопкой сбоку.</para>
        /// </summary>
        public static string CalendarRangeSideComponent { get; private set; }

        /// <summary>
        /// Initializes the phrases from the locale dictionary.
        /// <para>Инициализирует фразы из словаря локализации.</para>
        /// </summary>
        public static void Init()
        {
            LocaleDict dict = Locale.GetDictionary(
                "Scada.Web.Plugins.PlgMimCalendarJP.Code.CalendarComponentGroup");

            CalendarGroup = dict[nameof(CalendarGroup)];
            CalendarAutoComponent = dict[nameof(CalendarAutoComponent)];
            CalendarInputComponent = dict[nameof(CalendarInputComponent)];
            CalendarButtonComponent = dict[nameof(CalendarButtonComponent)];
            CalendarRangeComponent = dict[nameof(CalendarRangeComponent)];
            CalendarRangeBottomComponent = dict[nameof(CalendarRangeBottomComponent)];
            CalendarRangeSideComponent = dict[nameof(CalendarRangeSideComponent)];
        }
    }
}
