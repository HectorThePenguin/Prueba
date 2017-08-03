IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerDiasZilmax]')
		)
	DROP FUNCTION [dbo].[ObtenerDiasZilmax]
GO

-- =============================================  
-- Autor:  Jorge Luis Velazquez Araujo
-- Create date: 2014/04/04  
-- Description: Funcion para consultar los Dias de Zilmax 
-- ObtenerDiasZilmax 4,1,4
-- =============================================  
CREATE FUNCTION dbo.ObtenerDiasZilmax (
	@OrganizacionID INT
	,@LoteID INT
	,@TipoFormulaFinalizacion INT
	)
RETURNS INT
AS
BEGIN

DECLARE @DiasZilmax INT

	SET @DiasZilmax = (
			SELECT COUNT(r.RepartoID) Total
			FROM Reparto r
			WHERE r.OrganizacionID = @OrganizacionID
				AND r.LoteID = @LoteID
				AND r.Activo = 1
				AND (
					SELECT COUNT(rd.RepartoDetalleID)
					FROM RepartoDetalle rd
					INNER JOIN Formula f ON rd.FormulaIDServida = F.FormulaID
					WHERE r.RepartoID = rd.RepartoID						
						AND rd.Activo = 1
						AND F.TipoFormulaID = @TipoFormulaFinalizacion
					) = 2				
			)

	RETURN isnull(@DiasZilmax, 0)


END