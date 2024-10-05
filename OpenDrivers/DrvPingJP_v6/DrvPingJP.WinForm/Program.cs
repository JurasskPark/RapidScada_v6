using Scada.Comm.Drivers.DrvPingJP.View;

namespace DrvPingJP.WinForm
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            string fileName = @$"DrvPingJP.xml";
            Scada.Comm.Drivers.DrvPingJP.View.Forms.FrmConfig form = new Scada.Comm.Drivers.DrvPingJP.View.Forms.FrmConfig(fileName);
            Application.Run(form);
        }
    }
}