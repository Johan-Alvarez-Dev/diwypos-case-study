# Arquitectura pública

## Aplicación central

React consume una API ASP.NET Core. JWT aporta identidad, rol y tenant; cada operación de negocio aplica ese contexto. PostgreSQL mantiene pedidos, pagos, inventario y trabajos de impresión.

## Impresión resiliente

1. Crear o modificar una orden genera un `PrintJob` persistido.
2. SignalR notifica con baja latencia.
3. El agente autentica y reclama trabajo pendiente.
4. Formatea ESC/POS y envía por TCP.
5. Confirma éxito o registra fallo/reintento.

SignalR acelera; la tabla es la garantía. El agente funciona como servicio en Linux/Windows y no requiere el SDK en producción.

## Inventario

Cantidades se normalizan a `g`, `ml` o `unidad`. Las unidades personalizadas no se convierten sin equivalencia. El costo unitario base y el snapshot por pedido evitan reescribir utilidad histórica.
