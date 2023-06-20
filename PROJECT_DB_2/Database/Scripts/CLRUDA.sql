-- 
USE Szkola;
GO

DECLARE @hash VARBINARY(8000);
SELECT @hash = HashBytes('SHA2_512', BulkColumn)
FROM OPENROWSET(BULK N'/home/PROJECT_DB_2.dll', SINGLE_BLOB) AS dll

EXEC sp_add_trusted_assembly @hash = @hash, @description = N'Opis';

-- 
CREATE ASSEMBLY ClrUdaAssembly
FROM '/home/PROJECT_DB_2.dll'
WITH PERMISSION_SET = SAFE;

--

CREATE AGGREGATE CzestoscPiatki(@ocena DECIMAL)
RETURNS NVARCHAR(MAX)
EXTERNAL NAME ClrUdaAssembly.[CzestoscPiatki];
GO

SELECT 
    Kursy.NazwaKursu, 
    dbo.CzestoscPiatki(Oceny.Ocena) AS CzestoscPiatki
FROM 
    Kursy
JOIN 
    Oceny ON Kursy.KursId = Oceny.KursId
WHERE 
	Kursy.KursId = 1
GROUP BY 
    Kursy.NazwaKursu;
GO

--
CREATE AGGREGATE dbo.SredniaOcenNauczyciela(@ocena decimal(5, 2))
RETURNS decimal(5, 2)
EXTERNAL NAME ClrUdaAssembly.[SredniaOcenNauczyciela];

SELECT 
    K.NauczycielId,
    dbo.SredniaOcenNauczyciela(O.Ocena) AS SredniaOcena
FROM 
    Oceny O
JOIN 
    Kursy K ON O.KursId = K.KursId
WHERE
    K.NauczycielId = 1
GROUP BY 
    K.NauczycielID;
--

CREATE AGGREGATE dbo.RozkladOcen (@ocena decimal(3,1))
RETURNS nvarchar(max)
EXTERNAL NAME [ClrUdaAssembly].[RozkladOcen];

SELECT 
    K.KursID,
    dbo.RozkladOcen(O.Ocena) AS RozkladOcen
FROM 
    Oceny O
JOIN 
    Kursy K ON O.KursID = K.KursID
WHERE
    K.KursID = 1 -- podmień @TwojKursID na ID kursu, dla którego chcesz zobaczyć rozkład ocen
GROUP BY 
    K.KursID;

--
CREATE AGGREGATE dbo.LiczbaOcen(@ocena decimal(5, 2), @data datetime, @StudentId int) 
RETURNS int 
EXTERNAL NAME ClrUdaAssembly.[LiczbaOcen];

SELECT 
    dbo.LiczbaOcen(O.Ocena, O.Data, StudentId) AS LiczbaOcen
FROM 
    Oceny O
WHERE
    O.Data BETWEEN '2023-01-01' AND '2023-12-12' 
    AND O.StudentId = 2;
--

CREATE AGGREGATE dbo.LiczbaOcenPozNeg(@ocena decimal(5, 2)) 
RETURNS nvarchar(4000) 
EXTERNAL NAME ClrUdaAssembly.[LiczbaOcenPozNeg];

SELECT 
    dbo.LiczbaOcenPozNeg(O.Ocena) AS LiczbaOcenPozNeg
FROM 
    Oceny O
WHERE
    O.KursId = 2;

--