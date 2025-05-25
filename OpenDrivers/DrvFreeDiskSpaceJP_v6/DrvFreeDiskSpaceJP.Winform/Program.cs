using Scada.Comm.Drivers.DrvFreeDiskSpaceJP;
using System.Collections;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace DrvFreeDiskSpaceJP.Winform
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
       
            string fileName = Scada.Comm.Drivers.DrvFreeDiskSpaceJP.DriverUtils.GetFileName();
            string lanaugeDir = AppDomain.CurrentDomain.BaseDirectory;

            bool isRussian = true;
            if (args != null && args.Length > 0)
            {
                string culture = args[0];
                if (culture == "ru")
                {
                    isRussian = true;
                }
            }
            Scada.Comm.Drivers.DrvFreeDiskSpaceJP.View.Forms.FrmConfig form = new Scada.Comm.Drivers.DrvFreeDiskSpaceJP.View.Forms.FrmConfig();
            Application.Run(form);
        }

        

    }
}