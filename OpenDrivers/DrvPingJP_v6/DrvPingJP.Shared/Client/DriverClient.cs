using System.Runtime.InteropServices;

namespace Scada.Comm.Drivers.DrvPingJP
{
    /// <summary>
    /// Implements the driver client.
    /// <para>Реализует клиентский драйвер.</para>
    /// </summary>
    internal class DriverClient
    {
        private readonly Project project;                                   // configuration
        private readonly NetworkInformation networkInformation;             // network (ping)

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DriverClient(DriverTagReturn.DebugData onTagsReceived = null)
        {
            this.project = new Project();

            this.networkInformation = new NetworkInformation(onTagsReceived);
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DriverClient(Project project, DriverTagReturn.DebugData onTagsReceived = null)
        {
            this.project = project;

            this.networkInformation = new NetworkInformation(this.project.Mode, this.project.DeviceTags, onTagsReceived);
        }

        #region Dispose
        private bool _disposed = false;                 // disposed

        /// <summary>
        /// Dispose client
        /// </summary>
        ~DriverClient()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose client
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // free any other managed objects here.
            }

            _disposed = true;
        }

        /// <summary>
        /// Dispose client
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Dispose

        #region Start
        /// <summary>
        /// Start
        /// </summary>
        public void Start()
        {
            networkInformation.PingStart();
        }
        #endregion Start

        #region Stop
        /// <summary>
        /// Stop
        /// </summary>
        public void Stop()
        {
            networkInformation.PingStop();
        }

        #endregion Stop

        #region Process
        /// <summary>
        /// Ping
        /// </summary>
        public void Ping()
        {
            if (project.Mode == 0)
            {
                networkInformation.PingSynchronous();
            }
            else if (project.Mode == 1)
            {
                networkInformation.PingAsynchronous().GetAwaiter().GetResult();
            }
        }

        #endregion Process

    }
}
