using Scada.Comm.Drivers.DrvTelnetJP.View;

namespace DrvTelnetJP.WinForm
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
            string fileName = @$"C:\SCADA_6\ProjectSamples\IMPORT_DATA\Instances\Default\ScadaComm\Config\DrvTelnetJP_003.xml";
            Scada.Comm.Drivers.DrvTelnetJP.View.Forms.FrmConfig form = new Scada.Comm.Drivers.DrvTelnetJP.View.Forms.FrmConfig(fileName);
            Application.Run(form);
        }
    }
}