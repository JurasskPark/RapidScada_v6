﻿using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using FluentFTP;
using FluentFTP.Client.BaseClient;
using FluentFTP.Helpers;

namespace Examples {
	/// <summary>
	/// This is an example a FTP file listing parser. This code in the example
	/// is currently used to parse UNIX style long listings and serves as an
	/// example for adding your own parser.
	/// </summary>
	internal static class CustomParser {
		/// <summary>
		/// Adds a custom file listing parser
		/// </summary>
		public static void AddCustomParser(FtpClient client) {
			client.Config.ListingCustomParser = ParseUnixList;
		}

		/// <summary>
		/// Parses LIST format listings
		/// </summary>
		/// <param name="buf">A line from the listing</param>
		/// <param name="capabilities">Server capabilities</param>
		/// <returns>FtpListItem if the item is able to be parsed</returns>
		private static FtpListItem ParseUnixList(string buf, List<FtpCapability> capabilities, BaseFtpClient client) {
			var item = new FtpListItem();
			Match m = null;
			var regex =
				@"(?<permissions>[\w-]{10})\s+" +
				@"(?<objectcount>[0-9]+)\s+" +
				@"(?<user>[\w0-9]+)\s+" +
				@"(?<group>[\w0-9]+)\s+" +
				@"(?<size>[0-9]+)\s+" +
				@"(?<modify>\w+\s+[0-9]+\s+[0-9]+:[0-9]+|\w+\s+[0-9]+\s+[0-9]+)\s+" +
				@"(?<name>.*)$";

			if (!(m = Regex.Match(buf, regex, RegexOptions.IgnoreCase)).Success) {
				return null;
			}

			if (m.Groups["permissions"].Value.StartsWith("d")) {
				item.Type = FtpObjectType.Directory;
			}
			else if (m.Groups["permissions"].Value.StartsWith("-")) {
				item.Type = FtpObjectType.File;
			}
			else {
				return null;
			}

			// if we can't determine a file name then
			// we are not considering this a successful parsing operation.
			if (m.Groups["name"].Value.Length < 1) {
				return null;
			}

			item.Name = m.Groups["name"].Value;

			if (item.Type == FtpObjectType.Directory && (item.Name == "." || item.Name == "..")) {
				return null;
			}

			////
			// Ignore the Modify times sent in LIST format for files
			// when the server has support for the MDTM command
			// because they will never be as accurate as what can be had
			// by using the MDTM command. MDTM does not work on directories
			// so if a modify time was parsed from the listing we will try
			// to convert it to a DateTime object and use it for directories.
			////
			if ((!capabilities.Contains(FtpCapability.MDTM) || item.Type == FtpObjectType.Directory) && m.Groups["modify"].Value.Length > 0) {
				item.Modified = m.Groups["modify"].Value.ParseFtpDate(client);
			}

			if (m.Groups["size"].Value.Length > 0) {
				long size;

				if (long.TryParse(m.Groups["size"].Value, out size)) {
					item.Size = size;
				}
			}

			if (m.Groups["permissions"].Value.Length > 0) {
				var perms = Regex.Match(m.Groups["permissions"].Value,
					@"[\w-]{1}(?<owner>[\w-]{3})(?<group>[\w-]{3})(?<others>[\w-]{3})",
					RegexOptions.IgnoreCase);

				if (perms.Success) {
					if (perms.Groups["owner"].Value.Length == 3) {
						if (perms.Groups["owner"].Value[0] == 'r') {
							item.OwnerPermissions |= FtpPermission.Read;
						}

						if (perms.Groups["owner"].Value[1] == 'w') {
							item.OwnerPermissions |= FtpPermission.Write;
						}

						if (perms.Groups["owner"].Value[2] == 'x' || perms.Groups["owner"].Value[2] == 's') {
							item.OwnerPermissions |= FtpPermission.Execute;
						}

						if (perms.Groups["owner"].Value[2] == 's' || perms.Groups["owner"].Value[2] == 'S') {
							item.SpecialPermissions |= FtpSpecialPermissions.SetUserID;
						}
					}

					if (perms.Groups["group"].Value.Length == 3) {
						if (perms.Groups["group"].Value[0] == 'r') {
							item.GroupPermissions |= FtpPermission.Read;
						}

						if (perms.Groups["group"].Value[1] == 'w') {
							item.GroupPermissions |= FtpPermission.Write;
						}

						if (perms.Groups["group"].Value[2] == 'x' || perms.Groups["group"].Value[2] == 's') {
							item.GroupPermissions |= FtpPermission.Execute;
						}

						if (perms.Groups["group"].Value[2] == 's' || perms.Groups["group"].Value[2] == 'S') {
							item.SpecialPermissions |= FtpSpecialPermissions.SetGroupID;
						}
					}

					if (perms.Groups["others"].Value.Length == 3) {
						if (perms.Groups["others"].Value[0] == 'r') {
							item.OthersPermissions |= FtpPermission.Read;
						}

						if (perms.Groups["others"].Value[1] == 'w') {
							item.OthersPermissions |= FtpPermission.Write;
						}

						if (perms.Groups["others"].Value[2] == 'x' || perms.Groups["others"].Value[2] == 't') {
							item.OthersPermissions |= FtpPermission.Execute;
						}

						if (perms.Groups["others"].Value[2] == 't' || perms.Groups["others"].Value[2] == 'T') {
							item.SpecialPermissions |= FtpSpecialPermissions.Sticky;
						}
					}
				}
			}

			return item;
		}
	}
}