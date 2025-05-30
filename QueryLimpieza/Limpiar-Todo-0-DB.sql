USE [InvestmentAtlas];
GO

BEGIN TRY
    BEGIN TRANSACTION;

    -- 1) Desactiva TODAS las FK en la base de datos
    EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';

    -- 2) Trunca solo las tablas que no son “padres” de nadie
    TRUNCATE TABLE dbo.SimulationMacroIndicators;
    TRUNCATE TABLE dbo.CountryIndicators;
    TRUNCATE TABLE dbo.ReturnRateConfigs;

    -- 3) Borra las tablas padre con DELETE (ahora ya no tienen hijos)
    DELETE FROM dbo.MacroIndicators;
    DELETE FROM dbo.Countries;

    -- 4) Reseedea todos los identity
    DBCC CHECKIDENT ('dbo.SimulationMacroIndicators', RESEED, 0);
    DBCC CHECKIDENT ('dbo.CountryIndicators',       RESEED, 0);
    DBCC CHECKIDENT ('dbo.ReturnRateConfigs',       RESEED, 0);
    DBCC CHECKIDENT ('dbo.MacroIndicators',         RESEED, 0);
    DBCC CHECKIDENT ('dbo.Countries',               RESEED, 0);

    -- 5) Reactiva TODAS las FK y comprueba de nuevo la integridad
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
