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
        public DriverClient()
        {
            this.project = new Project();

            this.networkInformation = new NetworkInformation();
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DriverClient(Project project)
        {
            this.project = project;

            this.networkInformation = new NetworkInformation(this.project.Mode, this.project.DeviceTags);
        }

        #region Log
        /// <summary>
        /// Getting logs
        /// </summary>
        public static DebugData OnDebug;
        public delegate void DebugData(string msg);

        /// <summary>
        /// Passes log messages to subscribers.
        /// </summary>
        internal void DebugerLog(string text)
        {
            if (OnDebug == null)
            {
                return;
            }

            OnDebug(text);
        }
        #endregion Log

        #region DebugerTag
        /// <summary>
        /// Getting tag.
        /// </summary>
        public static DebugTag OnDebugTag;
        public delegate void DebugTag(DriverTag tags);

        /// <summary>
        /// Passes a tag to subscribers.
        /// </summary>
        internal void DebugerTag(DriverTag tag)
        {
            if (OnDebugTag == null)
            {
                return;
            }

            OnDebugTag(tag);
        }
        #endregion DebugerTag

        #region DebugerTags
        
        /// <summary>
        /// Getting tags.
        /// </summary>
        public static DebugTags OnDebugTags;
        public delegate void DebugTags(List<DriverTag> tags);

        /// <summary>
        /// Passes tags to subscribers.
        /// </summary>
        internal void DebugerTags(List<DriverTag> tags)
        {
            if (OnDebugTags == null)
            {
                return;
            }

            OnDebugTags(tags);
        }

        #endregion DebugerTags

        #region Dispose
        private IntPtr _bufferPtr;                      // buffer
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

            // free any unmanaged objects here.
            Marshal.FreeHGlobal(_bufferPtr);
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
