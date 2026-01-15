# Siniestros API (.NET 8) – Prueba Técnica

API REST para registrar siniestros viales y consultarlos por departamento, rango de fechas o combinando ambos filtros, con paginación.  
La solución está desarrollada en .NET 8 aplicando Clean Architecture, Domain-Driven Design (DDD) con invariantes de dominio, CQRS con MediatR, Repository pattern, validación con FluentValidation y manejo de errores estandarizado con ProblemDetails.

---

## 1. Alcance funcional

### Escritura
- Registro de siniestros viales con la siguiente información:
  - Identificador único (GUID)
  - Fecha y hora del evento (`DateTimeOffset`)
  - Departamento
  - Ciudad
  - Tipo de siniestro (enum)
  - Vehículos involucrados (lista)
  - Número de víctimas
  - Descripción opcional

### Lectura
- Consulta de siniestros por:
  - Departamento
  - Rango de fechas (`from` / `to`)
  - Combinación de ambos filtros
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
- SQL Server (LocalDB, Express, Developer o Standard)
- (Opcional) SQL Server Management Studio (SSMS)

---

## 4. Base de datos (SQL Server)

En la carpeta `/sql` se incluyen los siguientes scripts:

- `01_create_database.sql`
- `02_seed_data.sql`

### Ejecución
1. Abrir SSMS y ejecutar en orden:
   - `01_create_database.sql`
   - `02_seed_data.sql`

---

## 5. Configuración

Editar `Siniestros.Api/appsettings.json` y configurar la cadena de conexión.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=SiniestrosDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

## 6. Pasos para ejecutar la solución

Desde la raíz del repositorio, ejecutar los siguientes comandos:

```bash
dotnet restore
dotnet build
dotnet run --project Siniestros.Api
```

La aplicación iniciará y quedará disponible en:

http://localhost:5000
https://localhost:5001

Swagger:
- http://localhost:5000/swagger
- https://localhost:5001/swagger

---

## 7. Pruebas unitarias

La solución incluye pruebas unitarias para el dominio, validaciones y casos de uso.

Para ejecutarlas:

```bash
dotnet test
```

## Autor

Diego Alexander Torres Forero
