USE [InvestmentAtlas];
GO

BEGIN TRANSACTION;

-- 1) Borrar los datos (puede omitirse si la tabla ya está vacía)
DELETE FROM dbo.Countries;
GO

-- 2) Forzar el reseed de identidad a 0,
--    de modo que el siguiente INSERT genere Id = 1
DBCC CHECKIDENT ('dbo.Countries', RESEED, 0);
GO

COMMIT TRANSACTION;

USE [InvestmentAtlas];
GO

BEGIN TRANSACTION;

-- 1) Borrar los datos de MacroIndicators
DELETE FROM dbo.MacroIndicators;
GO

-- 2) Forzar el reseed de identidad a 0,
--    de modo que el siguiente INSERT genere Id = 1
DBCC CHECKIDENT ('dbo.MacroIndicators', RESEED, 0);
GO

COMMIT TRANSACTION;
GO
