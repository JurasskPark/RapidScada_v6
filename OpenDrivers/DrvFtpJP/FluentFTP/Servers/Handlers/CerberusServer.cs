﻿using System.Threading;
using System.Threading.Tasks;

namespace FluentFTP.Servers.Handlers {

	/// <summary>
	/// Server-specific handling for Cerberus FTP servers
	/// </summary>
	internal class CerberusServer : FtpBaseServer {

		/// <summary>
		/// Return the FtpServer enum value corresponding to your server, or Unknown if its a custom implementation.
		/// </summary>
		public override FtpServer ToEnum() {
			return FtpServer.Cerberus;
		}

		/// <summary>
		/// Return true if your server is detected by the given FTP server welcome message.
		/// </summary>
		public override bool DetectByWelcome(string message) {

			// Detect Cerberus server
			// Welcome message: "220-Cerberus FTP Server Personal Edition"
			if (message.Contains("Cerberus FTP")) {
				return true;
			}

			return false;
		}

	}
}
