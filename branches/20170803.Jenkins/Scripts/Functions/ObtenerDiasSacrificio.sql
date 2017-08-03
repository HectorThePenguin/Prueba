IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerDiasSacrificio]')
		)
	DROP FUNCTION [dbo].[ObtenerDiasSacrificio]
GO

--======================================================
-- Author     : Jorge Luis Velázquez Araujo
-- Create date: 04/04/2014 12:00:00 a.m.
-- Description: 
-- FnName     : select  dbo.ObtenerDiasSacrificio 4, 1
--======================================================
CREATE FUNCTION dbo.ObtenerDiasSacrificio ( @OrganizacionID INT)
RETURNS INT
AS
BEGIN
	DECLARE @DiasSacrificio INT
	
	declare @DiasPermitidoSacrificio int
	declare @DiasPeriodoEvaluacion int 	
	
	set @DiasPermitidoSacrificio = CAST((select Valor from ParametroOrganizacion po 
	inner join Parametro pa ON po.ParametroID = pa.ParametroID
	where po.OrganizacionID = @OrganizacionID
	AND pa.Clave = 'diasZilmax') AS int)
	
	set @DiasPeriodoEvaluacion = CAST((select Valor from ParametroOrganizacion po 
	inner join Parametro pa ON po.ParametroID = pa.ParametroID
	where po.OrganizacionID = @OrganizacionID
	AND pa.Clave = 'diasMinimos') AS int)
	

	SET @DiasSacrificio = @DiasPermitidoSacrificio + @DiasPeriodoEvaluacion

	RETURN isnull(@DiasSacrificio, 0)
END
GO