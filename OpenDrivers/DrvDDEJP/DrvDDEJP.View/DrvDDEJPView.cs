using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Forms;
using Scada.Lang;

namespace Scada.Comm.Drivers.DrvDDEJP.View
{
    /// <summary>
    /// Implements the driver user interface.
    /// <para>Реализует пользовательский интерфейс драйвера.</para>
    /// </summary>
    public class DrvDDEJPView : DriverView
    {
        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public DrvDDEJPView()
        {
            CanCreateDevice = true;
        }

        /// <summary>
        /// Gets the driver name.
        /// <para>Возвращает имя драйвера.</para>
        /// </summary>
        public override string Name => DriverUtils.Name(Locale.IsRussian);

        /// <summary>
        /// Gets the driver description.
        /// <para>Возвращает описание драйвера.</para>
        /// </summary>
        public override string Descr => DriverUtils.Description(Locale.IsRussian);

        /// <summary>
        /// Loads language dictionaries.
        /// <para>Загружает словари языка.</para>
        /// </summary>
        public override void LoadDictionaries()
        {
            if (!Locale.LoadDictionaries(AppDirs.LangDir, DriverUtils.DriverCode, out string errMsg))
            {
                ScadaUiUtils.ShowError(errMsg);
            }

            CommonPhrases.Init();
        }

        /// <summary>
        /// Creates a new device user interface.
        /// <para>Создаёт новый пользовательский интерфейс устройства.</para>
        /// </summary>
        public override DeviceView CreateDeviceView(LineConfig lineConfig, DeviceConfig deviceConfig)
        {
            return new DevDDEJPView(this, lineConfig, deviceConfig);
        }

        #endregion Basic
    }
}
