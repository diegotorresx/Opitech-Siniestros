# Siniestros API (.NET 8) – Prueba Técnica

API REST para registrar siniestros viales y consultarlos por departamento, rango de fechas o combinando ambos filtros, con paginación.  
Implementación en .NET 8 aplicando Clean Architecture, DDD (modelo de dominio con invariantes), CQRS con MediatR, Repository pattern, validación con FluentValidation y manejo de errores con ProblemDetails.

---

## 1. Alcance funcional

### Escritura
- Registrar siniestros con:
  - Identificador único (GUID)
  - Fecha y hora del evento (`DateTimeOffset`)
  - Departamento
  - Ciudad
  - Tipo de siniestro (enum)
  - Vehículos involucrados (lista)
  - Número de víctimas
  - Descripción opcional

### Lectura
- Consultar siniestros por:
  - Departamento
  - Rango de fechas (from/to)
  - Combinación de ambos
- Paginación:
  - `pageNumber` (>= 1)
  - `pageSize` (1..100)

---

## 2. Arquitectura

La solución se organiza en 4 capas siguiendo Clean Architecture:

- **Siniestros.Domain**
  - Entidades, Value Objects e invariantes del dominio.
  - No depende de otras capas.

- **Siniestros.Application**
  - Casos de uso (CQRS: Commands/Queries + Handlers).
  - Contratos como `IAccidentRepository`.
  - Validaciones con FluentValidation + pipeline behavior.

- **Siniestros.Infrastructure**
  - EF Core + SQL Server.
  - Implementaciones de repositorios y DbContext.

- **Siniestros.Api**
  - Endpoints HTTP, DI, Swagger y manejo global de errores (ProblemDetails).

Dependencias:
- Api -> Application + Infrastructure
- Infrastructure -> Application + Domain
- Application -> Domain
- Domain -> (nada)

---

## 3. Requisitos

- .NET SDK 8.x
- SQL Server (LocalDB / Express / Developer / Standard)
- (Opcional) SSMS para ejecutar scripts SQL

---

## 4. Base de datos (SQL Server)

Se incluyen scripts en `/sql`:

- `sql/01_create_database.sql` (crea base + tablas + índices)
- `sql/02_seed_data.sql` (inserta datos de prueba)

### Ejecución
1. Abrir SSMS y ejecutar en orden:
   - `01_create_database.sql`
   - `02_seed_data.sql`

---

## 5. Configuración

En `Siniestros.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=SiniestrosDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
