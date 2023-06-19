-- 1
DECLARE @hash VARBINARY(8000);
SELECT @hash = HashBytes('SHA2_512', BulkColumn)
FROM OPENROWSET(BULK N'/home/PROJECT_DB_2.dll', SINGLE_BLOB) AS dll

EXEC sp_add_trusted_assembly @hash = @hash, @description = N'Opis';

-- 2
CREATE ASSEMBLY TrendOcenAssembly
FROM '/home/PROJECT_DB_2.dll'
WITH PERMISSION_SET = SAFE;

CREATE AGGREGATE dbo.TrendOcen(@ocena DECIMAL(9, 4), @data DATETIME)
RETURNS DECIMAL(9, 4)
EXTERNAL NAME TrendOcenAssembly.[TrendOcen];

SELECT dbo.TrendOcen(o.Ocena, o.Data)
FROM Oceny o
WHERE o.KursId = 9;