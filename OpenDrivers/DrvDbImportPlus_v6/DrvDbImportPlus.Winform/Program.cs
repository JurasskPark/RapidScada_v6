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
            // see https://aka.ms/applicationprojecturation.
            ApplicationConfiguration.Initialize();
       
            Scada.Comm.Drivers.DrvDbImportPlus.View.Forms.FrmProject form = new Scada.Comm.Drivers.DrvDbImportPlus.View.Forms.FrmProject();
            Application.Run(form);
        }

     
    }
}