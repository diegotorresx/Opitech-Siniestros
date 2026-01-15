# ADR-003: SQL Server + EF Core y scripts SQL

## Contexto
La solución debe operar con SQL Server y el entregable debe ser sencillo de ejecutar por el evaluador.

## Decisión
- Persistencia con EF Core sobre SQL Server.
- Se entregan scripts SQL para:
  - Creación de base de datos, tablas e índices
  - Carga de datos de prueba

## Consecuencias
- Provisioning rápido para el evaluador sin depender de migrations.
- Esquema alineado con el modelo: `Accidents` y `AccidentVehicles`.
- Posibilidad de migrar a migrations sin cambios funcionales.

## Alternativas consideradas
1. Solo EF Migrations:
   - Adecuado, pero puede crear fricción en entornos donde el evaluador prefiera scripts.
2. Solo scripts SQL sin EF:
   - Menos flexible y reduce mantenibilidad de la capa de persistencia.
