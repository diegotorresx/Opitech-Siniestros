```markdown
# ADR-001: Clean Architecture + límites DDD

## Contexto
Se requiere una implementación mantenible con separación clara de responsabilidades, flujo correcto de dependencias y un dominio con reglas explícitas.

## Decisión
Se implementa Clean Architecture con 4 proyectos:
- Domain: modelo del dominio, invariantes y Value Objects (sin dependencias).
- Application: CQRS (Commands/Queries + Handlers), contratos e implementación de validación.
- Infrastructure: persistencia SQL Server con EF Core y repositorios.
- Api: endpoints, DI, swagger y manejo global de errores.

El agregado principal es `Accident`. Las reglas del negocio se expresan en el dominio (por ejemplo: víctimas no negativas y al menos un vehículo involucrado).

## Consecuencias
- Modelo de dominio centralizado y consistente.
- Casos de uso testeables y desacoplados del transporte (HTTP) y la persistencia.
- Bajo acoplamiento entre capas, facilitando cambios y mantenimiento.

## Alternativas consideradas
1. Controladores con EF directo (monolito):
   - Menor complejidad inicial, pero mezcla responsabilidades y reduce mantenibilidad.
2. Arquitectura por features sin límites de capas:
   - Funciona, pero dificulta validar dependencias limpias y reglas de dominio consistentes.