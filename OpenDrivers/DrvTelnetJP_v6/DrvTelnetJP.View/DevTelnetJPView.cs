using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvTelnetJP.View.Forms;
using Scada.Data.Models;
using Scada.Forms;

namespace Scada.Comm.Drivers.DrvTelnetJP.View
{
    /// <summary>
    /// Implements the device user interface.
    /// <para>Реализует пользовательский интерфейс КП.</para>
    /// </summary>
    internal class DevTelnetJPView : DeviceView
    {
        #region Variable

        private readonly DrvTelnetJPConfig config = new DrvTelnetJPConfig(); // device configuration

        #endregion Variable

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public DevTelnetJPView(DriverView parentView, LineConfig lineConfig, DeviceConfig deviceConfig)
            : base(parentView, lineConfig, deviceConfig)
        {
            CanShowProperties = true;
        }

        /// <summary>
        /// Shows a modal dialog box for editing device properties.
        /// <para>Показывает модальное окно редактирования свойств КП.</para>
        /// </summary>
        public override bool ShowProperties()
        {
            if (new FrmConfig(AppDirs, DeviceNum).ShowDialog() == DialogResult.OK)
            {
                LineConfigModified = true;
                DeviceConfigModified = true;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the default polling options for the device.
        /// <para>Возвращает параметры опроса КП по умолчанию.</para>
        /// </summary>
        public override PollingOptions GetPollingOptions()
        {
            return new PollingOptions(0, 0) { Period = new TimeSpan(0, 0, 0, 1) };
        }

        /// <summary>
        /// Gets the channel prototypes for the device.
        /// <para>Возвращает прототипы каналов КП.</para>
        /// </summary>
        public override ICollection<CnlPrototype> GetCnlPrototypes()
        {
            string configFileName = Path.Combine(AppDirs.ConfigDir, DrvTelnetJPConfig.GetFileName(DeviceNum));
            if (File.Exists(configFileName) && !config.Load(configFileName, out string errMsg))
            {
                ScadaUiUtils.ShowError(errMsg);
            }

            List<CnlPrototype> cnlPrototypes = CnlPrototypeFactory.GetCnlPrototypeGroups(config.DeviceTags).GetCnlPrototypes();
            for (int i = 0; i < cnlPrototypes.Count; i++)
            {
                cnlPrototypes[i].TagNum = i + 1;
            }

            return cnlPrototypes;
        }

        #endregion Basic
    }
}
