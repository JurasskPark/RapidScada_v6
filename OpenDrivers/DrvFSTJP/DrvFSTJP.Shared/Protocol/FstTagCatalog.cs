using ProjectDriver;
using System.Collections.Generic;
using System.Linq;

namespace Scada.Comm.Drivers.DrvFSTJP.Protocol
{
    /// <summary>
    /// Builds stable tag definitions from the XML project.
    /// <para>Формирует стабильные определения тегов из XML-проекта.</para>
    /// </summary>
    public static class FstTagCatalog
    {
        public static IReadOnlyList<FstTagDefinition> Build(Project project)
        {
            List<FstTagDefinition> tags = new List<FstTagDefinition>
            {
                new FstTagDefinition("DeviceType", "Device type"),
                new FstTagDefinition("GlobalErrors", "Global errors")
            };

            foreach (FstChannelConfig channel in (project.Channels ?? new List<FstChannelConfig>())
                .Where(channel => channel != null && channel.Enabled)
                .OrderBy(channel => channel.Order))
            {
                string prefix = channel.CodePrefix;
                string name = channel.Name;
                tags.Add(new FstTagDefinition($"{prefix}_Concentration", $"{name} concentration"));
                tags.Add(new FstTagDefinition($"{prefix}_MessageCode", $"{name} message code"));
                tags.Add(new FstTagDefinition($"{prefix}_AlarmCode", $"{name} alarm code"));
                tags.Add(new FstTagDefinition($"{prefix}_SensorType", $"{name} sensor type"));
                tags.Add(new FstTagDefinition($"{prefix}_CalibrationRequired", $"{name} calibration required"));
                tags.Add(new FstTagDefinition($"{prefix}_Threshold1", $"{name} threshold 1"));
                tags.Add(new FstTagDefinition($"{prefix}_Threshold2", $"{name} threshold 2"));
                tags.Add(new FstTagDefinition($"{prefix}_Disabled", $"{name} disabled"));
            }

            foreach (FstRelayDeviceConfig relayDevice in (project.RelayDevices ?? new List<FstRelayDeviceConfig>())
                .Where(relayDevice => relayDevice != null && relayDevice.Enabled)
                .OrderBy(relayDevice => relayDevice.Order))
            {
                tags.Add(new FstTagDefinition($"{relayDevice.CodePrefix}_StateLo", $"{relayDevice.Name} relays 1-8"));
                tags.Add(new FstTagDefinition($"{relayDevice.CodePrefix}_StateHi", $"{relayDevice.Name} relays 9-10"));
                tags.Add(new FstTagDefinition($"{relayDevice.CodePrefix}_Errors", $"{relayDevice.Name} errors"));
            }

            return tags;
        }
    }

    public class FstTagDefinition
    {
        public FstTagDefinition(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public string Code { get; }

        public string Name { get; }
    }
}
