# Public Architecture

## Central application

React consumes an ASP.NET Core API. JWT resolves user, role, and tenant. PostgreSQL stores orders, payments, inventory, and print jobs.

## Resilient printing

1. Creating or updating an order writes a durable `PrintJob`.
2. SignalR notifies the local agent for low latency.
3. Polling discovers jobs missed during disconnects.
4. The authenticated agent claims a job.
5. It formats ESC/POS bytes and sends them over TCP.
6. Success/failure is persisted for retry and audit.

SignalR accelerates delivery; the database guarantees it.

## Inventory and cost

Mass, volume, and count normalize to base units. Custom units are never guessed. Decimal arithmetic is used for quantities and money. Each sold item stores a cost snapshot so future catalog changes do not alter past margin.
