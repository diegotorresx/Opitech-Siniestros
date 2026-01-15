-- =============================================
-- 01_create_database.sql
-- Creates database, tables and indexes for SiniestrosDb (SQL Server)
-- =============================================

IF DB_ID('SiniestrosDb') IS NULL
BEGIN
    CREATE DATABASE SiniestrosDb;
END
GO

USE SiniestrosDb;
GO

-- Main table: Accidents
IF OBJECT_ID('dbo.Accidents', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Accidents
    (
        Id UNIQUEIDENTIFIER NOT NULL,
        OccurredAt DATETIMEOFFSET(7) NOT NULL,
        Department NVARCHAR(100) NOT NULL,
        City NVARCHAR(100) NOT NULL,
        Type INT NOT NULL,
        VictimsCount INT NOT NULL,
        Description NVARCHAR(500) NULL,
        CONSTRAINT PK_Accidents PRIMARY KEY (Id)
    );

    CREATE INDEX IX_Accidents_Department ON dbo.Accidents(Department);
    CREATE INDEX IX_Accidents_OccurredAt ON dbo.Accidents(OccurredAt);
END
GO

-- Owned collection table: AccidentVehicles
IF OBJECT_ID('dbo.AccidentVehicles', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.AccidentVehicles
    (
        Id INT IDENTITY(1,1) NOT NULL,
        AccidentId UNIQUEIDENTIFIER NOT NULL,
        Type NVARCHAR(50) NOT NULL,
        Plate NVARCHAR(20) NULL,
        CONSTRAINT PK_AccidentVehicles PRIMARY KEY (Id),
        CONSTRAINT FK_AccidentVehicles_Accidents FOREIGN KEY (AccidentId)
            REFERENCES dbo.Accidents(Id) ON DELETE CASCADE
    );

    CREATE INDEX IX_AccidentVehicles_AccidentId ON dbo.AccidentVehicles(AccidentId);
END
GO