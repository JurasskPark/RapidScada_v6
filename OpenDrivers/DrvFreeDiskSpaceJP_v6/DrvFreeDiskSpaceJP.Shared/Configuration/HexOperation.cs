using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP
{
    public class HEX_OPERATION
    {
        public static byte[] BYTEARRAY_COMBINE(byte[] bytesData1, byte[] bytesData2)
        {
            try
            {
                byte[] newArray = (byte[])null;

                if (bytesData1 == null && bytesData2.Length > 0)
                {
                    return bytesData2;
                }
                else if (bytesData2 == null && bytesData1.Length > 0)
                {
                    return bytesData1;
                }
                else if (bytesData1 == null && bytesData2 == null)
                {
                    return null;
                }

                newArray = new byte[bytesData1.Length + bytesData2.Length];
                Array.Copy(bytesData1, 0, newArray, 0, bytesData1.Length);
                Array.Copy(bytesData2, 0, newArray, bytesData1.Length, bytesData2.Length);
                return newArray;
            }
            catch
            {
                return (byte[])null;
            }
        }

        public static byte[] BYTEARRAY_SEARCH(byte[] bytes_Data, int address_array, int number_of_array_cells)
        {       
            try
            {
                byte[] newArray = new byte[number_of_array_cells];
                Array.Copy(bytes_Data, address_array, newArray, 0, number_of_array_cells);
                return newArray;
            }
            catch
            {
                return (byte[])null;
            }
        }
    }
}
