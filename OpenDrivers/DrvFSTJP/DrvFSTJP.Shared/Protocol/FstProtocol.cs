using System;

namespace Scada.Comm.Drivers.DrvFSTJP.Protocol
{
    /// <summary>
    /// Contains FST-03x command codes and packet builders.
    /// <para>Содержит коды команд ФСТ-03х и методы построения пакетов.</para>
    /// </summary>
    public static class FstProtocol
    {
        public const byte CmdLinkCheck = 0x00;
        public const byte CmdStatus = 0x01;
        public const byte CmdReset = 0x04;
        public const byte CmdRelayOn = 0x21;
        public const byte CmdRelayOff = 0x22;
        public const byte CmdRelayState = 0x23;

        public const byte DeviceTypeFst03V = 0x01;
        public const byte DeviceTypeFst03M = 0x02;
        public const byte DeviceTypeRelayBlock = 0x03;
        public const byte ControlForbidden = 0xFF;

        public static byte[] BuildLinkCheck(int masterAddress, int deviceAddress)
        {
            return FstPacket.Build(masterAddress, deviceAddress, CmdLinkCheck);
        }

        public static byte[] BuildStatusRequest(int masterAddress, int deviceAddress)
        {
            return FstPacket.Build(masterAddress, deviceAddress, CmdStatus);
        }

        public static byte[] BuildResetRequest(int masterAddress, int deviceAddress, int channelNo)
        {
            return FstPacket.Build(masterAddress, deviceAddress, CmdReset, (byte)Math.Max(0, Math.Min(8, channelNo)));
        }

        public static byte[] BuildRelayRequest(int masterAddress, int relayAddress, byte command, int relayNo)
        {
            return FstPacket.Build(masterAddress, relayAddress, command, (byte)Math.Max(1, Math.Min(10, relayNo)));
        }

        public static byte[] BuildRelayStateRequest(int masterAddress, int relayAddress, ushort relayMask)
        {
            return FstPacket.Build(masterAddress, relayAddress, CmdRelayState, (byte)(relayMask & 0xFF), (byte)((relayMask >> 8) & 0x03));
        }
    }
}
