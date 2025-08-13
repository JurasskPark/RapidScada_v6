﻿using System.Threading;
using System.Threading.Tasks;

namespace FluentFTP.Servers.Handlers {

	/// <summary>
	/// Server-specific handling for Titan FTP servers
	/// </summary>
	internal class TitanFtpServer : FtpBaseServer {

		/// <summary>
		/// Return the FtpServer enum value corresponding to your server, or Unknown if its a custom implementation.
		/// </summary>
		public override FtpServer ToEnum() {
			return FtpServer.TitanFTP;
		}

		/// <summary>
		/// Return true if your server is detected by the given FTP server welcome message.
		/// </summary>
		public override bool DetectByWelcome(string message) {

			// Detect Pure-FTPd server
			// Welcome message: "220 Titan FTP Server 10.01.1740 Ready"
			if (message.Contains("Titan FTP")) {
				return true;
			}

			return false;
		}

	}
}
