# Technical Decisions

## Tenant context on every operation

The API derives tenant identity from the authenticated principal. Client-side filtering is not authorization.

## Persistence before real-time delivery

A durable queue guarantees printing; SignalR only reduces latency.

## Local hardware agent

A cloud API cannot directly reach restaurant LAN hardware. An authenticated local service owns printer transport.

## Base measurement units

Normalized quantities make stock and recipes comparable while preserving display preferences.

## Cost snapshots

Historical reports use cost captured at sale time rather than the latest recipe price.
