# PlgMimTankJP — Industrial SVG Vessels

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/industrial-svg-vessels.md)

Every vessel always displays its body and nameplate. The level indicator, alarm badges and instruments are disabled by default and appear only when enabled in properties. Liquid is shown on a wide external level scale and in separate value cards; it is not painted transparently inside the vessel body.

## Vessel Capability Matrix

| Vessel | Layers | Temperature | Pressure | Additional |
| --- | ---: | --- | --- | --- |
| `RvsVessel` — vertical storage tank | 3 | Single, multipoint | Yes | Alarms |
| `VerticalProcessVessel` — vertical process vessel | 3 | Single, multipoint | Yes | Alarms |
| `HorizontalProcessVessel` — horizontal process vessel | 3 | Single | Yes | Alarms |
| `SiloHopper` — silo or hopper | 1 product | Single, multipoint | No | Direct mass |
| `RectangularClosedTank` — closed rectangular tank | 3 | Single | Yes | Alarms |
| `OpenBath` — open bath | 1 | Single | No | Alarms |
| `SphericalTank` — spherical tank | 3 | Single | Yes | Alarms |
| `ReactorMixer` — reactor/mixer | 1 | Single, multipoint | Yes | Drive state |

All vessel types also support `LL/L/H/HH` level alarms and an independent general alarm channel.

## Common Vessel Properties

| Property | Default | Purpose |
| --- | ---: | --- |
| `vesselName` | Type-specific | Text on the nameplate. |
| `bodyStyle` | `Tinted` | Selects a tintable body or fixed steel gradients. |
| `bodyColor` | `#748491` | Body tint used by `Tinted`. |
| `levelEnabled` | `false` | Shows the external level scale and value cards. |
| `tankHeightMeters` | `8` | Physical vessel height. |
| `deadZoneMeters` | `0` | Bottom sediment compensation. |
| `decimalPlaces` | `3` | Level value precision from `0` to `6`. |
| `emptyColor` | `#e2e8f0` | Empty part of the external scale. |
| `alarmsEnabled` | `false` | Enables `LL/L/H/HH` badges and bindings. |
| `generalAlarmEnabled` | `false` | Enables the independent general alarm. |
| `generalAlarmInCnlNum` | `0` | Nonzero good value activates the alarm outline. |
| `clickAction` | Empty | Standard Mimic action. |

The layer source, color, dead-zone, overflow, quality and alarm rules are the same as for the full level indicators. One-layer vessels expose only the first layer.

## Body Styles

| Style | Behavior |
| --- | --- |
| `Tinted` | Uses `bodyColor` while preserving industrial light and shadow. |
| `Steel` | Uses the fixed metallic gradients of the original SVG. `bodyColor` does not recolor the steel coating. |

## Pressure and Single Temperature

Pressure properties are available only for vessel types marked in the capability matrix:

| Property group | Main settings | Editor preview |
| --- | --- | ---: |
| Pressure | `pressureEnabled`, `pressureInCnlNum`, `pressureUnit`, `pressureDecimalPlaces` | `0.62 MPa / МПа` |
| Single temperature | `temperatureMode = Single`, `temperatureInCnlNum`, `temperatureUnit`, `temperatureDecimalPlaces` | `54.8 °C` |

At runtime, the configured channel value replaces the preview. Missing or bad-quality values are displayed as `#.#` together with the configured unit.

## Multipoint Temperature

Multipoint mode is available for `RvsVessel`, `VerticalProcessVessel`, `SiloHopper` and `ReactorMixer`. The editable list contains from 1 to 24 `TemperaturePoint` entries.

Each point stores:

- `name` — point name;
- `heightMeters` — physical installation height from the bottom;
- `inCnlNum` — input channel.

The temperature card selects the point nearest to the current physical liquid level before dead-zone subtraction. If two points are equally distant, the lower point is selected. An unknown or partial physical level, or bad quality of the selected point, produces `#.#` without substituting another point.

At runtime, click the `T` card to open a read-only table of all points sorted by height. The table shows name, height, value and quality. This click does not execute the vessel's general `clickAction`.

## Silo Mass

`SiloHopper` can display mass using `massEnabled`, `massInCnlNum`, `massUnit` and `massDecimalPlaces`. The editor preview is `42.5 t / т`.

Mass is read directly from the channel. The plugin does not calculate volume, density, temperature correction or derived mass.

## Reactor Drive

Enable `driveEnabled` and set `driveInCnlNum` to display the reactor drive state.

| Channel data | State and display |
| --- | --- |
| Good quality, value `0` | `STOPPED`, steady red motor |
| Good quality, value `1` | `RUNNING`, green motor and rotating mixer |
| Good quality, value `2` or `-1` | `ALARM`, blinking red motor and card |
| Missing, bad quality or another value | `UNKNOWN`, amber motor |

Mixer rotation and alarm blinking are disabled in edit mode and when reduced motion is requested by the operating system.
