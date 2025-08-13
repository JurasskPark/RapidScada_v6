namespace DrvFtpJP.Winform
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
       
            string fileName = Scada.Comm.Drivers.DrvFtpJP.DriverUtils.GetFileName();
            string lanaugeDir = AppDomain.CurrentDomain.BaseDirectory;

            bool isRussian = false;
            if (args != null && args.Length > 0)
            {
                string culture = args[0];
                if (culture == "ru")
                {
                    isRussian = true;
                }
            }
            Scada.Comm.Drivers.DrvFtpJP.View.Forms.FrmConfig form = new Scada.Comm.Drivers.DrvFtpJP.View.Forms.FrmConfig();
            Application.Run(form);
        }

     
    }
}