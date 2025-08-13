using System;
using System.Collections.Generic;
using System.Text;

namespace Scada.Comm.Drivers.DrvFtpJP
{
    public enum FtpAction : int
    {
        CreateDirectory = 0,
        DeleteDirectory = 1,
        DeleteFile = 3,
        DownloadDirectory = 3,
        DownloadFile = 4,
        UploadDirectory = 2,
        UploadFile = 8,
        Execute = 5,
        Rename = 6,
    }
}
