IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerRangoObjetivoCalidad]')
		)
	DROP FUNCTION [dbo].[ObtenerRangoObjetivoCalidad]
GO

CREATE FUNCTION ObtenerRangoObjetivoCalidad (@IndicadorObjetivoID INT)
RETURNS VARCHAR(20)

BEGIN
	DECLARE @rangoObjetivo VARCHAR(400)

	SELECT @rangoObjetivo = CASE 
			WHEN IOB.TipoObjetivoCalidadID = 3
				THEN (CAST(IOB.ObjetivoMinimo AS VARCHAR(10)) + ' - ' + CAST(IOB.ObjetivoMaximo AS VARCHAR(10)))
			WHEN IOB.TipoObjetivoCalidadID = 1
				THEN CAST(IOB.ObjetivoMinimo AS VARCHAR(10))
			WHEN IOB.TipoObjetivoCalidadID = 2
				THEN CAST(IOB.ObjetivoMaximo AS VARCHAR(10))
			ELSE CAST(IOB.ObjetivoMinimo AS VARCHAR(10))
			END
	FROM IndicadorObjetivo IOB
	WHERE IOB.IndicadorObjetivoID = @IndicadorObjetivoID

	RETURN @rangoObjetivo;
END
