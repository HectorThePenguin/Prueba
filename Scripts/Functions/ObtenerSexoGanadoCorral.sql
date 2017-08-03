IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerSexoGanadoCorral]')
		)
	DROP FUNCTION [dbo].[ObtenerSexoGanadoCorral]
GO
--=============================================
-- Author     : José Gilberto Quintero López
-- Create date: 2014/04/10
-- Description: Función para rellenar de ceros a la izquierda
-- select dbo.ObtenerSexoGanadoCorral(1,1)
--=============================================
CREATE FUNCTION dbo.ObtenerSexoGanadoCorral (
	@OrganizacionId INT
	,@LoteID INT
	)
RETURNS CHAR(1)
AS
BEGIN
	DECLARE @SexoGanado CHAR(1)

	SELECT @SexoGanado = tg.Sexo
	FROM (
		SELECT a.LoteID
			,a.TipoGanadoID
			,ROW_NUMBER() OVER (
				PARTITION BY LoteId ORDER BY LoteId
				) AS [Orden]
		FROM (
			SELECT am.LoteId
				,a.TipoGanadoID
			FROM AnimalMovimiento am
			INNER JOIN Animal a ON a.AnimalID = am.AnimalID
			WHERE am.OrganizacionId = @OrganizacionId
			GROUP BY am.LoteId
				,a.TipoGanadoID
			) a
		) a
	INNER JOIN TipoGanado tg ON tg.TipoGanadoID = a.TipoGanadoID
	WHERE Orden = 1

	RETURN isnull(@SexoGanado, '')
END
GO
