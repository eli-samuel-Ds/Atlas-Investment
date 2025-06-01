USE [InvestmentAtlas];
GO

BEGIN TRY
    BEGIN TRANSACTION;

    EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';

    TRUNCATE TABLE dbo.SimulationMacroIndicators;
    TRUNCATE TABLE dbo.CountryIndicators;
    TRUNCATE TABLE dbo.ReturnRateConfigs;

    DELETE FROM dbo.MacroIndicators;
    DELETE FROM dbo.Countries;

    DBCC CHECKIDENT ('dbo.SimulationMacroIndicators', RESEED, 0);
    DBCC CHECKIDENT ('dbo.CountryIndicators',       RESEED, 0);
    DBCC CHECKIDENT ('dbo.ReturnRateConfigs',       RESEED, 0);
    DBCC CHECKIDENT ('dbo.MacroIndicators',         RESEED, 0);
    DBCC CHECKIDENT ('dbo.Countries',               RESEED, 0);

    EXEC sp_MSForEachTable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL';

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
    RAISERROR('Error al resetear tablas: %s', 16, 1, @ErrMsg);
END CATCH;
GO
