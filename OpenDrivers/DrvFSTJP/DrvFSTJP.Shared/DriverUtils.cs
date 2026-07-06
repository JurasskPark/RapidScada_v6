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
        public const string Version = "6.0.0.1";

        public static string GetFileName(int deviceNum)
        {
            return deviceNum == 0
                ? $"{DriverCode}.xml"
                : $"{DriverCode}_{deviceNum:D3}.xml";
        }

        public static string Name(bool isRussian = false)
        {
            return isRussian ? "ФСТ-03х JP" : "FST-03x JP";
        }

        public static string Description(bool isRussian = false)
        {
            return isRussian
                ? "Драйвер опроса газоанализаторов ФСТ-03х по RS232/RS485."
                : "RS232/RS485 polling driver for FST-03x gas analyzers.";
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
