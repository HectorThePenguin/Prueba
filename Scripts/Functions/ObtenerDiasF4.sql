IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerDiasF4]')
		)
	DROP FUNCTION [dbo].[ObtenerDiasF4]
GO

--======================================================
-- Author     : Jorge Luis Velázquez Araujo
-- Create date: 27/02/2014 12:00:00 a.m.
-- Description: 
-- FnName     : select  dbo.ObtenerDiasF4 4, 1
--======================================================
CREATE FUNCTION dbo.ObtenerDiasF4 ( @OrganizacionID INT, @LoteID INT)
RETURNS INT
AS
BEGIN
	DECLARE @DiasF4 INT

	SET @DiasF4 = (
			SELECT COUNT(r.RepartoID) Total
			FROM Reparto r
			WHERE r.OrganizacionID = @OrganizacionID
				AND r.LoteID = @LoteID
				AND r.Activo = 1
				AND (
					SELECT COUNT(rd.RepartoDetalleID)
					FROM RepartoDetalle rd
					INNER JOIN Formula fo ON rd.FormulaIDServida = fo.FormulaID					
					WHERE r.RepartoID = rd.RepartoID
						AND fo.TipoFormulaID = 3 --Tipo Formula Produccion
						AND rd.Activo = 1
					) = 2				
			)

	RETURN isnull(@DiasF4, 0)
END
GO