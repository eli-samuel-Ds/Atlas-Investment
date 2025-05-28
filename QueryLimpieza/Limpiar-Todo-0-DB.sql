USE [InvestmentAtlas];
GO

BEGIN TRANSACTION;

DELETE FROM dbo.Countries;
GO

DBCC CHECKIDENT ('dbo.Countries', RESEED, 0);
GO

COMMIT TRANSACTION;

USE [InvestmentAtlas];
GO

BEGIN TRANSACTION;

DELETE FROM dbo.MacroIndicators;
GO

DBCC CHECKIDENT ('dbo.MacroIndicators', RESEED, 0);
GO

COMMIT TRANSACTION;
GO

USE [InvestmentAtlas];
GO

BEGIN TRY
    BEGIN TRANSACTION;
    TRUNCATE TABLE [dbo].[CountryIndicators];

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    RAISERROR ('Ha ocurrido un error al resetear CountryIndicators: %s', 16, 1, @ErrorMessage);
END CATCH;
GO
