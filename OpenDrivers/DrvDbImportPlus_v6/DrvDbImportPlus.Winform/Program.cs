using FastColoredTextBoxNS;
using System.Data.SqlTypes;
using System.Text;
using System.Collections;
using System.Linq;
using Scada.Comm.Drivers.DrvDbImportPlus;

namespace DrvDbImportPlus.Winform
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            double c = (double)5 / 2;
            c = Math.Ceiling(c);

            string val = "Оченьдлиннаяфамилиянасорокзнаковаж А.А.";

            IEnumerable<string> enumVal = Split(val.ToString(), 4);
            List<string> lstVal = enumVal.ToList();

            double[] arrWords = new double[lstVal.Count];

            for (int i = 0; i < lstVal.Count; i++)
            {
                string s = lstVal[i].ToString();
                byte[] buf = new byte[8];
                int len = Math.Min(4, s.Length);
                Encoding.Unicode.GetBytes(s, 0, len, buf, 0);
                double word = BitConverter.ToDouble(buf, 0);
                arrWords[i] = word;
            }

            //IEnumerable<string> t = Split(str, 4);
            //List<string> asList = t.ToList();
            //List<string> strs = new List<string>();
            //for (int i = 2; i < str.Length; i += 4)
            //{
            //    string g = str.Substring()
            //    strs.Add(str.Substring(i - 2, 4));
            //}



            //object val = null;
            //val = "Оченьдлиннаяфамилиянасорокзнаковаж А.А.";
            //int numChar = val.ToString().Length;
            //byte[] vals = Encoding.Unicode.GetBytes(val.ToString());

            //for (int index = 0; index < vals.Length / 8; ++index)
            //{
            //    //int RegAddr = 0 + index;
            //    byte[] arrData = BYTEARRAY_SEARCH(vals, index * 8, 8);

            //    int num = arrData.Length;
            //    double[] array = new double[(num % 8 == 0) ? (num / 8) : (num / 8 + 1)];
            //    Buffer.BlockCopy(arrData, 0, array, 0, num);

            //}






            //string str_ascii = string.Empty;

            //byte[] tmp_numArray = Encoding.ASCII.GetBytes("abcde");
            //ASCIIEncoding ascii = new ASCIIEncoding();
            //string win1251 = Encoding.GetEncoding(1251).GetString(Enumerable.Range(0, 256).Select(n => (byte)n).ToArray());

            //foreach (byte value in tmp_numArray)
            //{
            //    try
            //    {
            //        int chislo = Convert.ToInt32(value);
            //        str_ascii += win1251[chislo].ToString();
            //    }
            //    catch { }
            //}

            //string tf = str_ascii;

            

            string fileName = Scada.Comm.Drivers.DrvDbImportPlus.DriverUtils.GetFileName();
            string lanaugeDir = AppDomain.CurrentDomain.BaseDirectory;

            //DrvDbImportPlusConfig config = new DrvDbImportPlusConfig();
            //config.Load(fileName, out string errMsg);


            bool isRussian = true;
            if (args != null && args.Length > 0)
            {
                string culture = args[0];
                if (culture == "ru")
                {
                    isRussian = true;
                }
            }
            Scada.Comm.Drivers.DrvDbImportPlus.View.Forms.FrmConfig form = new Scada.Comm.Drivers.DrvDbImportPlus.View.Forms.FrmConfig(fileName);
            form.LoadLanguage(lanaugeDir, isRussian);
            Application.Run(form);
        }

        public static byte[]? BYTEARRAY_SEARCH(byte[] bytes_Data, int address_array, int number_of_array_cells)
        {
            try
            {
                byte[] newArray = new byte[number_of_array_cells];
                Array.Copy(bytes_Data, address_array, newArray, 0, number_of_array_cells);
                return newArray;
            }
            catch
            {
                return null;
            }
        }

        static IEnumerable<string> Split(string s, int length)
        {
            int i;
            for (i = 0; i + length < s.Length; i += length)
            {
                yield return s.Substring(i, length);
            }            
            if (i != s.Length)
            {
                yield return s.Substring(i, s.Length - i);
            }             
        }
    }
}