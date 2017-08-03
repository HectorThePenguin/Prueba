IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerPesoInicio]')
		)
	DROP FUNCTION [dbo].[ObtenerPesoInicio]
GO

CREATE FUNCTION dbo.ObtenerPesoInicio (
	@OrganizacionId INT
	,@LoteID INT
	,@Cabezas INT
	)
RETURNS INT
AS
BEGIN
	DECLARE @PesoInicio INT

	SET @PesoInicio = (
			SELECT CASE 
					WHEN COUNT(a.PesoLlegada) = @Cabezas
						THEN (SUM(a.PesoLlegada) / @Cabezas)
					ELSE 0
					END AS PesoInicio
			FROM Animal a
			INNER JOIN AnimalMovimiento am ON a.AnimalID = am.AnimalID
			WHERE am.OrganizacionID = @OrganizacionId
				AND am.LoteID = @LoteID
				and am.Activo =1
			GROUP BY am.LoteID
			)

	RETURN isnull(@PesoInicio, 0)
END
