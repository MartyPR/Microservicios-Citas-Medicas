

CREATE DATABASE CitasDb;
GO

-- Usar la base de datos recién creada
USE CitasDb;
GO
CREATE TABLE Citas (
    Id INT PRIMARY KEY IDENTITY,       
    Fecha DATETIME NOT NULL,          
    Paciente NVARCHAR(100) NOT NULL,   
    Medico NVARCHAR(100) NOT NULL,     
    Especialidad NVARCHAR(100) NOT NULL, 
    Estado NVARCHAR(50) NOT NULL      
);