# Domain Model

El dominio se centra en el agregado `Accident` y una colección de vehículos involucrados (`VehicleInvolved`).

```mermaid
classDiagram
class Accident {
  AccidentId Id
  DateTimeOffset OccurredAt
  string Department
  string City
  AccidentType Type
  int VictimsCount
  string? Description
  List~VehicleInvolved~ Vehicles
}
class VehicleInvolved {
  string Type
  string? Plate
}
Accident "1" --> "*" VehicleInvolved
