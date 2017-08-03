IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerDiasFormula]')
		)
	DROP FUNCTION [dbo].[ObtenerDiasFormula]
GO

--======================================================
-- Author     : Gilberto Carranza
-- Create date: 27/03/2014
-- Description: 
-- FnName     : SELECT dbo.ObtenerDiasFormula(4, 1)
--======================================================
CREATE FUNCTION dbo.ObtenerDiasFormula (
	@OrganizacionID INT
	,@LoteID INT
	)
RETURNS INT
AS
BEGIN
	DECLARE @DiasFormula INT
	DECLARE @FormulaServida INT
	DECLARE @Fecha SMALLDATETIME

	SELECT TOP 1 @FormulaServida = FormulaIDServida
		,@Fecha = Fecha
	FROM RepartoDetalle RD
	INNER JOIN Reparto R ON (
			RD.RepartoID = R.RepartoID
			AND R.LoteID = @LoteID
			AND R.OrganizacionID = @OrganizacionID
			)
	ORDER BY R.RepartoID DESC

	SELECT TOP 1 @DiasFormula = DATEDIFF(dd, Fecha, @Fecha)
	FROM RepartoDetalle RD
	INNER JOIN Reparto R ON (
			RD.RepartoID = R.RepartoID
			AND R.LoteID = @LoteID
			AND R.OrganizacionID = @OrganizacionID
			)
	WHERE FormulaIDServida <> @FormulaServida
		AND Fecha < @Fecha
	ORDER BY R.RepartoID DESC

	IF @DiasFormula IS NULL
	BEGIN
		SELECT TOP 1 @DiasFormula = DATEDIFF(dd, Fecha, @Fecha)
		FROM RepartoDetalle RD
		INNER JOIN Reparto R ON (
				RD.RepartoID = R.RepartoID
				AND R.LoteID = @LoteID
				AND R.OrganizacionID = @OrganizacionID
				)
		WHERE Fecha < @Fecha
			AND RD.RepartoID = (
				SELECT TOP 1 re1.RepartoID
				FROM Reparto re1
				WHERE re1.OrganizacionID = @OrganizacionID
					AND re1.LoteID = @LoteID
				ORDER BY re1.RepartoID
				)
		ORDER BY R.RepartoID DESC
	END

	RETURN ISNULL(@DiasFormula, 0)
END
