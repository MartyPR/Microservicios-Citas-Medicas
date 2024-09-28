CREATE DATABASE RecetasDb;
GO

-- Usar la base de datos recién creada
USE RecetasDb;
GO

CREATE TABLE Recetas (
    Id INT PRIMARY KEY IDENTITY,       
    CitaId INT NOT NULL,               
    Paciente NVARCHAR(100) NOT NULL,  
	Medico NVARCHAR(100) NOT NULL, 
    Medicamentos NVARCHAR(255) NOT NULL, 
    Estado NVARCHAR(50) NOT NULL,      
    CONSTRAINT FK_Cita FOREIGN KEY (CitaId) REFERENCES CitasDb(Id) 
);