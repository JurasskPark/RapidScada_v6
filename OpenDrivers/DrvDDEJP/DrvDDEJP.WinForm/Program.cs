namespace DrvDDEJP.WinForm
{
    /// <summary>
    /// Provides the application entry point.
    /// <para>Предоставляет точку входа приложения.</para>
    /// </summary>
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
            Scada.Comm.Drivers.DrvDDEJP.View.Forms.FrmProject form = new Scada.Comm.Drivers.DrvDDEJP.View.Forms.FrmProject();
            Application.Run(form);
        }
    }
}
