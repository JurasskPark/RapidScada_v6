﻿using System.Threading;
using System.Threading.Tasks;

namespace FluentFTP.Servers.Handlers {

	/// <summary>
	/// Server-specific handling for PyFtpdLib FTP servers
	/// </summary>
	internal class PyFtpdLibServer : FtpBaseServer {

		/// <summary>
		/// Return the FtpServer enum value corresponding to your server, or Unknown if its a custom implementation.
		/// </summary>
		public override FtpServer ToEnum() {
			return FtpServer.PyFtpdLib;
		}

		/// <summary>
		/// Return true if your server is detected by the given FTP server welcome message.
		/// </summary>
		public override bool DetectByWelcome(string message) {

			// Detect PyFtpdLib server
			// Welcome message: "220 pyftpdlib 1.5.6 ready"
			if (message.Contains("pyftpdlib ")) {
				return true;
			}

			return false;
		}

	}
}