IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerUltimoCorralAnimal]')
		)
	DROP FUNCTION [dbo].ObtenerUltimoCorralAnimal
GO

CREATE FUNCTION dbo.ObtenerUltimoCorralAnimal (
	@OrganizacionID INT
	,@AnimalID INT
	)
RETURNS CHAR(10)
AS
BEGIN
	DECLARE @UltimoCorral CHAR

	SET @UltimoCorral = (
			SELECT TOP 1 co.Codigo
			FROM AnimalMovimiento am
			INNER JOIN Corral co ON am.CorralID = co.CorralID
			INNER JOIN TipoCorral tc ON co.TipoCorralID = tc.TipoCorralID
			WHERE am.OrganizacionID = @OrganizacionID
				AND am.AnimalID = @AnimalID
				AND tc.GrupoCorralID = 2 --Corrales de Tipo Produccion
			ORDER BY am.FechaMovimiento DESC
			)

	RETURN isnull(@UltimoCorral, '')
END
