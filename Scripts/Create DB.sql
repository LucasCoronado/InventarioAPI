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
    Activo BIT NOT NULL DEFAULT 1;

);

INSERT INTO Repuestos (Nombre, Marca, Precio, Stock) VALUES
('Filtro de aire', 'Bosch', 25.99, 100),
('Bujía', 'NGK', 9.99, 200),
('Aceite de motor', 'Castrol', 29.99, 150),
('Pastillas de freno', 'Brembo', 49.99, 80),
('Amortiguadores', 'Monroe', 89.99, 50);
