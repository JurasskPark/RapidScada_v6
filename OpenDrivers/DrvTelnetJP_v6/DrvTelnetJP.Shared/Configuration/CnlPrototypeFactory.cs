using Scada.Comm.Devices;
using Scada.Data.Const;
using Scada.Lang;

namespace Scada.Comm.Drivers.DrvTelnetJP
{
    /// <summary>
    /// Creates channel prototypes for a device.
    /// <para>Создает прототипы каналов устройства.</para>
    /// </summary>
    internal static class CnlPrototypeFactory
    {
        #region Basic

        /// <summary>
        /// Gets the grouped channel prototypes.
        /// <para>Возвращает сгруппированные прототипы каналов.</para>
        /// </summary>
        public static List<CnlPrototypeGroup> GetCnlPrototypeGroups(List<Tag> deviceTags)
        {
            List<CnlPrototypeGroup> groups = new List<CnlPrototypeGroup>();
            string tagGroupName = Locale.IsRussian ? "Теги" : "Tags";
            CnlPrototypeGroup group = new CnlPrototypeGroup(tagGroupName);

            if (deviceTags != null)
            {
                foreach (Tag deviceTag in deviceTags)
                {
                    if (deviceTag == null)
                    {
                        continue;
                    }

                    group.AddCnlPrototype(deviceTag.TagCode, deviceTag.TagName)
                        .SetFormat(FormatCode.OffOn)
                        .Configure(cnl => cnl.Active = deviceTag.TagEnabled);
                }
            }

            groups.Add(group);
            return groups;
        }

        #endregion Basic
    }
}
