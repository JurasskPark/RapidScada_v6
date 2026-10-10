# PlgMimSVGAnimationJP — Channel catalog and integration

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/channel-catalog.md)

The designer's channel table searches by number, name, code and tag, with object/device filters. Filtering precedes pagination. Inactive channels are visible but cannot be selected. Manual numbers are checked when catalog data is available.

## Hosts and permissions

| Host | Endpoint / provider |
| --- | --- |
| Webstation | `GET /api/plugins/svg-animation-jp/channels`; administrator policy and object View/Control rights |
| WebAdmin | `GET /api/admin/mimic-editor/channels`; current project workspace |
| Custom editor host | `channelCatalogProvider.query(request, abortSignal)` |

Webstation uses its `ConfigDatabase`. Input selection accepts channel types `1–4` with View rights; command selection accepts types `2, 4, 5` with Control rights. These are catalog permissions; they do not grant runtime control rights.

| Parameter | Meaning / limit |
| --- | --- |
| `term` | Search string; at most `200` characters |
| `direction` | `data` or `command` |
| `objNum`, `deviceNum` | Optional filters |
| `page` | Default `1`; clamped to `1–1000000` |
| `pageSize` | Default `50`; clamped to `1–100` |
| `cnlNums` | Comma-separated channel numbers for targeted lookup; at most `2200` characters |

The response includes `items`, `totalItems`, `page`, `pageSize`, `objects` and `devices`. Items describe the channel number, name, code, tag, active flag, type and object/device.

## Provider adapter

Register a custom provider with `SvgAnimationJP.registerChannelCatalog(provider)` or create an HTTP adapter with `SvgAnimationJP.httpCatalog(endpoint)`. Pass the catalog through the host's provider contract; no specific database implementation is required in the browser.

A new query aborts the previous one; a revision check discards stale responses. Closing the dialog cancels pending work. If no provider is available, manual channel assignment remains available.

[Channel roles](channels.md) · [Faceplates](faceplates.md)
