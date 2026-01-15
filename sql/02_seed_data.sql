-- =============================================
-- 02_seed_data.sql
-- Inserts sample data for SiniestrosDb (SQL Server)
-- =============================================

USE SiniestrosDb;
GO

DELETE FROM dbo.AccidentVehicles;
DELETE FROM dbo.Accidents;
GO

DECLARE @A1 UNIQUEIDENTIFIER = NEWID();
DECLARE @A2 UNIQUEIDENTIFIER = NEWID();
DECLARE @A3 UNIQUEIDENTIFIER = NEWID();
DECLARE @A4 UNIQUEIDENTIFIER = NEWID();
DECLARE @A5 UNIQUEIDENTIFIER = NEWID();

INSERT INTO dbo.Accidents (Id, OccurredAt, Department, City, Type, VictimsCount, Description)
VALUES
(@A1, '2026-01-10T08:30:00-05:00', N'Córdoba',  N'Planeta Rica', 1, 1, N'Colisión leve entre vehículo y moto.'),
(@A2, '2026-01-11T18:20:00-05:00', N'Antioquia', N'Medellín',     3, 2, N'Atropello en cruce peatonal.'),
(@A3, '2026-01-12T14:05:00-05:00', N'Córdoba',  N'Montería',     2, 0, NULL),
(@A4, '2026-01-13T21:10:00-05:00', N'Bolívar',  N'Cartagena',    1, 3, N'Colisión múltiple, atención de emergencias.'),
(@A5, '2026-01-14T09:40:00-05:00', N'Córdoba',  N'Planeta Rica', 4, 1, N'Incidente con ciclista, lesiones menores.');

INSERT INTO dbo.AccidentVehicles (AccidentId, Type, Plate)
VALUES
(@A1, N'Car',        N'ABC123'),
(@A1, N'Motorcycle', NULL),

(@A2, N'Car',        N'XYZ987'),

(@A3, N'Truck',      N'TRU555'),

(@A4, N'Bus',        N'BUS777'),
(@A4, N'Car',        N'CAR222'),
(@A4, N'Motorcycle', NULL),

(@A5, N'Bicycle',    NULL),
(@A5, N'Car',        N'AAA111');
GO