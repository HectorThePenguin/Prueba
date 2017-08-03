IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerDiasTransicion]')
		)
	DROP FUNCTION [dbo].[ObtenerDiasTransicion]
GO

--======================================================
-- Author     : Jorge Luis Velázquez Araujo
-- Create date: 10/04/2014
-- Description: 
-- FnName     : SELECT dbo.ObtenerDiasTransicion(4, 1)
--======================================================
CREATE FUNCTION dbo.ObtenerDiasTransicion (
	@OrganizacionID INT
	,@LoteID INT
	)
RETURNS INT
AS
BEGIN
	DECLARE @DiasTransicion INT
	DECLARE @FormulaServida INT
	DECLARE @Fecha SMALLDATETIME

	SELECT TOP 1 @FormulaServida = FormulaIDServida
			,  @Fecha = Fecha
		FROM RepartoDetalle RD
		INNER JOIN Reparto R
			ON (RD.RepartoID = R.RepartoID
				AND R.LoteID = @LoteID
				AND R.OrganizacionID = @OrganizacionID)
		ORDER BY R.RepartoID DESC

		SELECT TOP 1 @DiasTransicion = DATEDIFF(dd, Fecha, @Fecha)
		FROM RepartoDetalle RD
		INNER JOIN Reparto R
			ON (RD.RepartoID = R.RepartoID
				AND R.LoteID = @LoteID
				AND R.OrganizacionID = @OrganizacionID)
		WHERE 
			RD.FormulaIDServida = (SELECT Top 1 rd1.FormulaIDServida FROM RepartoDetalle rd1 where rd1.RepartoID = R.RepartoID AND rd1.TipoServicioID <> RD.TipoServicioID)
		ORDER BY R.RepartoID DESC
		

	RETURN ISNULL(@DiasTransicion, 0)
END
