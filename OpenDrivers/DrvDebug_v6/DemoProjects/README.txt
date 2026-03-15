DrvDebug demo configurations

Files in this folder are example driver configuration files for Rapid SCADA.
They demonstrate:
- sequential command sending
- byte array decoding by index and length
- numeric formats
- ASCII and Unicode string decoding
- simulation modes
- Decode+Simulate fallback mode

Suggested usage:
1. Open the configuration form of the DrvDebug device.
2. Load or copy one of the XML files from this folder into the active device config file.
3. Adjust channel numbers and payloads if needed for your test stand.

Files:
- demo_01_uint16.xml
- demo_02_float.xml
- demo_03_ascii.xml
- demo_04_unicode.xml
- demo_05_decode_simulate.xml
- demo_06_string_sequence.xml
- demo_master_command_decode.xml
- demo_slave_decode_and_simulate.xml
