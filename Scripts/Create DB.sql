Use master;
GO

CREATE DATABASE InventarioDB;
GO
USE InventarioDB;
GO

CREATE TABLE Repuestos (

    ID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Marca NVARCHAR(50) NOT NULL,
    Precio DECIMAL(18, 2) NOT NULL,
    Stock int NOT NULL

);