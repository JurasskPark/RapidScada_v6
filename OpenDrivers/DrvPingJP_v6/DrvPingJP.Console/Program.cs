using Scada.Comm.Drivers.DrvPingJP;
using Scada.Comm.Drivers.DrvPingJP.Logic;
using System;
using static Scada.Comm.Drivers.DrvPingJP.NetworkInformation;
using Csl = System.Console;

namespace DrPingJP.Console
{
    internal class Program
    {

        private static string configFileName;                 // the configuration file name
        private static DrvPingJPConfig config;                // the device configuration  
        private static bool writeLog;                         // write log
        private static int pingMode;                          // type ping
        private static List<Tag> deviceTags;                  // tags
        private static NetworkInformation networkInformation; // network (ping)

        static void Main(string[] args)
        {
            Csl.WriteLine("Run");
            string configFileName = @$"C:\SCADA_6\ProjectSamples\IMPORT_DATA\Instances\Default\ScadaComm\Config\DrvPingJP_002.xml";
            
            networkInformation = new NetworkInformation();
            networkInformation.OnDebug = new NetworkInformation.DebugData(DebugerLog);
            networkInformation.OnDebugTag = new NetworkInformation.DebugTag(DebugerTag);
            networkInformation.OnDebugTags = new NetworkInformation.DebugTags(DebugerTags);

            // load configuration
            config = new DrvPingJPConfig();
            if (config.Load(configFileName, out string errMsg))
            {
                writeLog = config.Log;
                pingMode = config.Mode;
                deviceTags = config.DeviceTags;
            }
            else
            {
                DebugerLog(errMsg);
            }

            int tryNum = 0;

            while (Request())
            {
                tryNum++;
            }

            Csl.ReadKey();
        }

        static bool Request()
        {
            try
            {
                #region Ping
                if (pingMode == 0)
                {
                    #region Synchronous
                    try
                    {
                        networkInformation.RunPingSynchronous(deviceTags);
                        return true;
                    }
                    catch { }
                    #endregion Synchronous
                }
                else if (pingMode == 1)
                {
                    #region Asynchronous
                    try
                    {
                        networkInformation.RunPingAsynchronous(deviceTags);
                        return true;
                    }
                    catch { }
                    #endregion Asynchronous
                }
                #endregion Ping 
                return true;
            }
            catch (Exception ex)
            {
                DebugerLog(string.Format("Error executing: {0}", ex.Message));
                return false;
            }
        }

        #region Debug Log
        /// <summary>
        /// Getting logs
        /// </summary>
        public static void DebugerLog(string text)
        {
            if (text == string.Empty)
            {
                return;
            }

            if (writeLog)
            {
                Csl.WriteLine(text);
            }
        }
        #endregion Debug Log

        #region Debug Tag
        public static void DebugerTag(Tag tag)
        {
            if (tag == null)
            {
                return;
            }

            int indexTag = deviceTags.IndexOf(tag);

            if (tag.TagEnabled == true) // enabled
            {
                if (tag.TagCode != string.Empty)
                {
                    //SetTagData(tag.TagCode, tag.TagVal, tag.TagStat);
                }
                else
                {
                    //SetTagData(indexTag, tag.TagVal, tag.TagStat);
                }
            }
        }
        #endregion Debug Tag

        #region Debug Tags
        public static void DebugerTags(List<Tag> tags)
        {
            if (tags == null || tags.Count == 0)
            {
                return;
            }

            for (int index = 0; index < tags.Count; index++)
            {
                Tag tmpTag = tags[index];
                int indexTag = deviceTags.IndexOf(tags[index]);
                //DebugerLog("index = " + indexTag.ToString());

                if (tmpTag == null || tmpTag.TagEnabled == false)
                {
                    continue;
                }

                if (tmpTag.TagEnabled == true) // enabled
                {
                    if (tmpTag.TagCode != string.Empty)
                    {
                        //SetTagData(tmpTag.TagCode, tmpTag.TagVal, tmpTag.TagStat);
                    }
                    else
                    {
                        //SetTagData(indexTag, tmpTag.TagVal, tmpTag.TagStat);
                    }
                }
            }
        }
        #endregion Debug Tags
    }
}