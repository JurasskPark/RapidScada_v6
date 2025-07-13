using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Scada.Comm.Drivers.DrvPingJP
{
    internal class DriverClient
    {
        private readonly Project project;                                   // configuration
        private readonly NetworkInformation networkInformation;             // network (ping)

        public DriverClient()
        {
            this.project = new Project();

            this.networkInformation = new NetworkInformation();
        }

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
        // transfer to the form and to the file in the Log folder
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
        /// Getting tag
        /// <para>Получение тего<para>
        /// </summary>
        public static DebugTag OnDebugTag;
        public delegate void DebugTag(DriverTag tags);
        // transfer to the form and to the file in the Log folder
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
        /// Getting tags
        /// <para>Получение тегов<para>
        /// </summary>
        public static DebugTags OnDebugTags;
        public delegate void DebugTags(List<DriverTag> tags);
        // transfer to the form and to the file in the Log folder
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
        public int BUFFER_SIZE = 1024 * 1024 * 50;      // 50 MB
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
                #region Synchronous
                try
                {
                    networkInformation.PingSynchronous();
                }
                catch { }
                #endregion Synchronous
            }
            else if (project.Mode == 1)
            {
                #region Asynchronous
                try
                {
                   networkInformation.PingAsynchronous();
                }
                catch { }
                #endregion Asynchronous
            }
        }

        #endregion Process

    }
}
