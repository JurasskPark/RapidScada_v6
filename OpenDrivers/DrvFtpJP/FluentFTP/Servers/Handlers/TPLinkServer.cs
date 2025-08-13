﻿using System.Threading;
using System.Threading.Tasks;

namespace FluentFTP.Servers.Handlers {

	/// <summary>
	/// Server-specific handling for TP-LINK FTP servers
	/// </summary>
	internal class TPLinkServer : FtpBaseServer {

		/// <summary>
		/// Return the FtpServer enum value corresponding to your server, or Unknown if its a custom implementation.
		/// </summary>
		public override FtpServer ToEnum() {
			return FtpServer.TPLink;
		}

		/// <summary>
		/// Return true if your server is detected by the given FTP server welcome message.
		/// </summary>
		public override bool DetectByWelcome(string message) {

			// Detect TP-LINK server
			// Welcome message: "TP-LINK FTP version 1.0 ready"
			if (message.Contains("TP-LINK FTP")) {
				return true;
			}

			return false;
		}

	}
}