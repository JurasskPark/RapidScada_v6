using System;

namespace Scada.Comm.Drivers.DrvFSTJP
{
    /// <summary>
    /// Provides common driver constants and naming helpers.
    /// <para>Предоставляет общие константы драйвера и вспомогательные методы именования.</para>
    /// </summary>
    public static class DriverUtils
    {
        public const string DriverCode = "DrvFSTJP";

        // Shared display metadata for the driver UI and generated release README.
        public const string NameRu = "Газоанализаторы ФСТ-03х";
        public const string NameEn = "FST-03x gas analyzers";
        public const string DescriptionRu = "Опрос газоанализаторов ФСТ-03х по RS-232/RS-485 и передача команд устройствам.";
        public const string DescriptionEn = "Polls FST-03x gas analyzers over RS-232/RS-485 and sends commands to the devices.";
        public static string Version => typeof(DriverUtils).Assembly.GetName().Version.ToString();

        public static string GetFileName(int deviceNum)
        {
            return deviceNum == 0
                ? $"{DriverCode}.xml"
                : $"{DriverCode}_{deviceNum:D3}.xml";
        }

        public static string Name(bool isRussian = false)
        {
            return isRussian ? NameRu : NameEn;
        }

        public static string Description(bool isRussian = false)
        {
            return isRussian ? DescriptionRu : DescriptionEn;
        }

        public static int NormalizeAddress(int address)
        {
            return Math.Max(0, Math.Min(15, address));
        }

        public static int NormalizeChannelNo(int channelNo)
        {
            return Math.Max(1, Math.Min(8, channelNo));
        }
    }
}
