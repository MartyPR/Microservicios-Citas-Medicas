CREATE DATABASE PersonasDb;
GO

-- Usar la base de datos recién creada
USE PersonasDb;
GO

CREATE TABLE Personas (
    Id INT PRIMARY KEY IDENTITY,       
    Nombre NVARCHAR(100) NOT NULL,    
    Apellido NVARCHAR(100) NOT NULL,  
    Email NVARCHAR(100) NOT NULL,      
    Telefono NVARCHAR(50) NOT NULL,    
    TipoDePersona NVARCHAR(50) NOT NULL 
);
