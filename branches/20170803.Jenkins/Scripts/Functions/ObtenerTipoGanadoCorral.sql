IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[ObtenerTipoGanadoCorral]'))
		DROP FUNCTION [dbo].[ObtenerTipoGanadoCorral]
GO
CREATE FUNCTION dbo.ObtenerTipoGanadoCorral(@OrganizacionId INT, @CorralID INT)
Returns VARCHAR(50)
AS
BEGIN
	DECLARE @TipoGanado VARCHAR(50)
	
	SET @TipoGanado = 
		(SELECT tg.Descripcion 
		FROM TipoGanado tg
		INNER JOIN TipoGanadoCorrales tgc ON tg.TipoGanadoID = tgc.TipoGanadoID
		INNER JOIN CorralRango cg ON (tgc.RangoInicial = cg.RangoInicial AND tgc.RangoFinal = cg.RangoFinal)
		WHERE OrganizacionId = @OrganizacionId 	
		AND cg.CorralID = @CorralID
		AND cg.Sexo = tg.Sexo)

	Return isnull(@TipoGanado, '')
END

GO