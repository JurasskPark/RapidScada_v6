# PlgMimControlsJP — Overview

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/overview.md)

![Rapid SCADA](https://img.shields.io/badge/Rapid%20SCADA-6.5-blue.svg)
![.NET](https://img.shields.io/badge/.NET-10.0-purple.svg)
![Version](https://img.shields.io/badge/version-6.5.0.15-green.svg)
![Platform](https://img.shields.io/badge/platform-Windows%20%7C%20Linux-lightgrey.svg)

This guide explains how engineers, operators and administrators use `PlgMimControlsJP` in Rapid SCADA mimic diagrams. It covers the component catalog, channel bindings, command confirmation, data quality, value-entry forms, themes, installation, activation and troubleshooting.

`PlgMimControlsJP` version `6.5.0.15` provides nineteen localized control components and the autonomous `ControlsDemo` component in the **CONTROLS** toolbox. The plugin uses the public standard Mimic contract and works with both the standard Mimic Editor and compatible alternative editors. It does not require `PlgMimicJP` or a separate backend API.

Authoring is free of the component runtime license: engineers can place, configure, copy and save ordinary controls without installing a local ControlsJP license. Executing those controls in Webstation requires a valid server-side `PlgMimControlsJP` license. `ControlsDemo` works without that license using only simulated data.
