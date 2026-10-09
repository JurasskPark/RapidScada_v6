# DrvFtpJP — Scenario Actions

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/scenario-actions.md)

| Action | English behavior |
| --- | --- |
| `None` | Does nothing. |
| `LocalCreateDirectory` | Creates a local directory from `LocalPath`. |
| `RemoteCreateDirectory` | Creates a remote FTP directory from `RemotePath`. |
| `LocalRename` | Renames a local file or directory from `LocalPath` to `RemotePath`. |
| `RemoteRename` | Renames a remote FTP object from `LocalPath` to `RemotePath`. |
| `LocalDeleteFile` | Deletes a local file if it exists. |
| `LocalDeleteDirectory` | Deletes a local directory recursively if it exists. |
| `RemoteDeleteFile` | Deletes a remote FTP file. |
| `RemoteDeleteDirectory` | Deletes a remote FTP directory. |
| `LocalUploadFile` | Uploads one local file to a remote directory. |
| `LocalUploadDirectory` | Uploads a local directory to a remote directory and creates a remote child directory with the local directory name. |
| `RemoteDownloadFile` | Downloads one remote file to a local directory. |
| `RemoteDownloadDirectory` | Downloads a remote directory to a local directory and creates a local child directory with the remote directory name. |
