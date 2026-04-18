using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvDDEJP;
using Scada.Comm.Drivers.DrvDDEJP.View.Forms;
using Scada.Data.Const;
using Scada.Forms;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Scada.Comm.Drivers.DrvDDEJP.View
{
    /// <summary>
    /// Implements the device user interface.
    /// <para>Реализует пользовательский интерфейс устройства.</para>
    /// </summary>
    internal class DevDDEJPView : DeviceView
    {
        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        /// <param name="parentView">The parent driver view.</param>
        /// <param name="lineConfig">The line configuration.</param>
        /// <param name="deviceConfig">The device configuration.</param>
        public DevDDEJPView(DriverView parentView, LineConfig lineConfig, DeviceConfig deviceConfig)
            : base(parentView, lineConfig, deviceConfig)
        {
            CanShowProperties = true;
        }

        /// <summary>
        /// Shows a modal dialog box for editing data source properties.
        /// <para>Отображает модальное диалоговое окно для редактирования свойств источника данных.</para>
        /// </summary>
        public override bool ShowProperties()
        {
            if (new FrmProject(AppDirs, LineConfig, DeviceConfig, DeviceNum).ShowDialog() == DialogResult.OK)
            {
                LineConfigModified = true;
                DeviceConfigModified = true;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the default polling options for the device.
        /// <para>Возвращает параметры опроса устройства по умолчанию.</para>
        /// </summary>
        public override PollingOptions GetPollingOptions()
        {
            return PollingOptions.CreateDefault();
        }

        /// <summary>
        /// Gets the channel prototypes for the device.
        /// <para>Возвращает прототипы каналов для устройства.</para>
        /// </summary>
        public override ICollection<CnlPrototype> GetCnlPrototypes()
        {
            List<CnlPrototype> cnlPrototypes = new List<CnlPrototype>();
            Project project = new Project();
            string configFileName = Path.Combine(AppDirs.ConfigDir, Project.GetFileName(DeviceNum));

            if (File.Exists(configFileName) && !project.Load(configFileName, out string errMsg))
            {
                ScadaUiUtils.ShowError(errMsg);
                return cnlPrototypes;
            }

            int tagNum = 1;
            foreach (ProjectTag tag in project.Tags.OrderBy(t => t.Order))
            {
                CnlPrototype prototype = new CnlPrototype
                {
                    Active = tag.Enabled,
                    Name = tag.Name,
                    Code = GetTagCode(tag),
                    TagCode = GetTagCode(tag),
                    TagNum = tagNum++,
                    CnlTypeID = CnlTypeID.InputOutput,
                    DataLen = tag.DataLength,
                    DeviceNum = DeviceNum
                };

                prototype.DataTypeID = GetCnlDataTypeID(tag.DataFormat);
                cnlPrototypes.Add(prototype);
            }

            return cnlPrototypes;
        }

        #endregion Basic

        #region Private Methods

        /// <summary>
        /// Gets the tag code for the specified tag.
        /// <para>Получает код тега для указанного тега.</para>
        /// </summary>
        private static string GetTagCode(ProjectTag tag)
        {
            return string.IsNullOrWhiteSpace(tag.Name)
                ? $"tag_{tag.Channel}_{tag.Id:N}"
                : tag.Name.Trim().Replace(" ", "_");
        }

        /// <summary>
        /// Gets the channel data type ID for the specified format.
        /// <para>Получает ID типа данных канала для указанного формата.</para>
        /// </summary>
        private static int GetCnlDataTypeID(TagDataFormat format)
        {
            return format switch
            {
                TagDataFormat.Ascii => (int)TagDataType.ASCII,
                TagDataFormat.Unicode => (int)TagDataType.Unicode,
                TagDataFormat.Int64 => (int)TagDataType.Int64,
                TagDataFormat.UInt64 => (int)TagDataType.Int64,
                _ => (int)TagDataType.Double
            };
        }

        #endregion Private Methods
    }
}
