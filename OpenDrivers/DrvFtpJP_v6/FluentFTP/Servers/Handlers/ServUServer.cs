﻿using System.Threading;
using System.Threading.Tasks;
using FluentFTP.Client.BaseClient;

namespace FluentFTP.Servers.Handlers {

	/// <summary>
	/// Server-specific handling for ServU FTP servers
	/// </summary>
	internal class ServUServer : FtpBaseServer {

		/// <summary>
		/// Return the FtpServer enum value corresponding to your server, or Unknown if its a custom implementation.
		/// </summary>
		public override FtpServer ToEnum() {
			return FtpServer.ServU;
		}

		/// <summary>
		/// Return true if your server is detected by the given FTP server welcome message.
		/// </summary>
		public override bool DetectByWelcome(string message) {

			// Detect Serv-U server
			// Welcome message: "220 Serv-U FTP Server v5.0 for WinSock ready."
			if (message.Contains("Serv-U FTP")) {
				return true;
			}

			return false;
		}

		/// <summary>
		/// Perform server-specific delete directory commands here.
		/// Return true if you executed a server-specific command.
		/// </summary>
		public override bool DeleteDirectory(FtpClient client, string path, string ftppath, bool deleteContents, FtpListOption options) {

			// Support #88 - Support RMDA command for Serv-U
			if (deleteContents && client.HasFeature(FtpCapability.RMDA)) {
				if ((client.Execute("RMDA " + ftppath)).Success) {
					((IInternalFtpClient)client).LogStatus(FtpTraceLevel.Verbose, "Used the server-specific RMDA command to quickly delete directory: " + ftppath);
					return true;
				}
				else {
					((IInternalFtpClient)client).LogStatus(FtpTraceLevel.Verbose, "Failed to use the server-specific RMDA command to quickly delete directory: " + ftppath);
				}
			}

			return false;
		}

		/// <summary>
		/// Perform async server-specific delete directory commands here.
		/// Return true if you executed a server-specific command.
		/// </summary>
		public override async Task<bool> DeleteDirectoryAsync(AsyncFtpClient client, string path, string ftppath, bool deleteContents, FtpListOption options, CancellationToken token) {

			// Support #88 - Support RMDA command for Serv-U
			if (deleteContents && client.HasFeature(FtpCapability.RMDA)) {
				if ((await client.Execute("RMDA " + ftppath, token)).Success) {
					((IInternalFtpClient)client).LogStatus(FtpTraceLevel.Verbose, "Used the server-specific RMDA command to quickly delete directory: " + ftppath);
					return true;
				}
				else {
					((IInternalFtpClient)client).LogStatus(FtpTraceLevel.Verbose, "Failed to use the server-specific RMDA command to quickly delete directory: " + ftppath);
				}
			}

			return false;
		}

	}
}
