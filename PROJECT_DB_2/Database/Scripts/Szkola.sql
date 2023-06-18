CREATE DATABASE Szkola;
GO

USE Szkola;
GO

CREATE TABLE Studenci (
    StudentId INT IDENTITY(1,1) PRIMARY KEY,
    Imie VARCHAR(50),
    Nazwisko VARCHAR(50),
    DataUrodzenia DATE,
    Klasa VARCHAR(50)
);
GO

CREATE TABLE Nauczyciele (
    NauczycielId INT IDENTITY(1,1) PRIMARY KEY,
    Imie VARCHAR(50),
    Nazwisko VARCHAR(50),
    Przedmiot VARCHAR(50)
);
GO

CREATE TABLE Kursy (
    KursId INT IDENTITY(1,1) PRIMARY KEY,
    NazwaKursu VARCHAR(50),
    NauczycielId INT FOREIGN KEY REFERENCES Nauczyciele(NauczycielId)
);
GO

CREATE TABLE Oceny (
    OcenaId INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT FOREIGN KEY REFERENCES Studenci(StudentId),
    KursId INT FOREIGN KEY REFERENCES Kursy(KursId),
    Ocena DECIMAL(3,2),
    Data DATE
);
GO
