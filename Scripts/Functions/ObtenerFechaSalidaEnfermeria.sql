IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerFechaSalidaEnfermeria]')
		)
	DROP FUNCTION [dbo].[ObtenerFechaSalidaEnfermeria]
GO

--======================================================
-- Author     : Jorge Luis Velázquez Araujo
-- Create date: 24/04/2014 12:00:00 a.m.
-- Description: Obtiene la fecha en que el Animal Salio de Enfermería
-- FnName     : select  dbo.ObtenerFechaSalidaEnfermeria 4, 1
--======================================================
CREATE FUNCTION dbo.ObtenerFechaSalidaEnfermeria ( @OrganizacionID INT, @AnimalID INT, @FechaEntradaEnfermeria SMALLDATETIME)
RETURNS SMALLDATETIME
AS
BEGIN
	DECLARE @FechaSalidaEnfermeria SMALLDATETIME
	DECLARE @TipoSalidaEnfermeria INT
	set @TipoSalidaEnfermeria = 10
	
	set @FechaSalidaEnfermeria = (SELECT TOP 1 am.FechaMovimiento from AnimalMovimiento am
									WHERE am.OrganizacionID = @OrganizacionID
									AND am.AnimalID = @AnimalID
									AND am.TipoMovimientoID = @TipoSalidaEnfermeria
									AND am.FechaMovimiento > @FechaEntradaEnfermeria)
	

	RETURN @FechaSalidaEnfermeria
END
GO