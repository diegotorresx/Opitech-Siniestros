# ADR-002: CQRS con MediatR + validación en pipeline

## Contexto
El enunciado solicita CQRS y se valora el patrón Mediator. Además, se requiere validación y manejo de errores consistente.

## Decisión
- Commands y Queries se implementan con MediatR (`IRequest<T>` + handlers).
- Validación con FluentValidation.
- Se incorpora un `ValidationBehavior<TRequest, TResponse>` para ejecutar validaciones antes del handler.

## Consecuencias
- Controllers delgados y casos de uso con responsabilidad única.
- Validaciones centralizadas y consistentes (no duplicadas).
- Estructura clara para extensión (nuevos commands/queries sin afectar el resto).

## Alternativas consideradas
1. Validación en controllers:
   - Rápido, pero se duplica y dificulta el mantenimiento.
2. Servicios sin CQRS:
   - Menos archivos, pero pierde claridad entre lectura y escritura y complica pruebas.
