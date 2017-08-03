IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerPesoProyectado]')
		)
	DROP FUNCTION [dbo].[ObtenerPesoProyectado]
GO

CREATE FUNCTION dbo.ObtenerPesoProyectado (
	@OrganizacionId INT
	,@LoteID INT
	,@Cabezas INT
	)
RETURNS INT
AS
BEGIN
	DECLARE @TablaPesoProyectado AS TABLE (
		LoteID INT
		,Peso INT
		,FechaInicio SMALLDATETIME
		,Cabezas INT
		)

	INSERT INTO @TablaPesoProyectado
	SELECT am.LoteID
		,SUM(am.Peso) AS Peso
		,NULL
		,Count(am.Peso) AS Cabezas
	FROM AnimalMovimiento am
	INNER JOIN Lote lo ON am.LoteID = lo.LoteID
	WHERE lo.OrganizacionID = @OrganizacionId
		AND lo.LoteID = @LoteID
		AND am.Activo = 1
	GROUP BY am.LoteID

	UPDATE pp
	SET pp.FechaInicio = lo.FechaInicio
	FROM @TablaPesoProyectado pp
	INNER JOIN Lote lo ON pp.LoteID = lo.LoteID

	DECLARE @ValidacionCabezas INT

	SET @ValidacionCabezas = (
			SELECT pp.Cabezas
			FROM @TablaPesoProyectado pp
			)

	IF (@ValidacionCabezas <> @Cabezas)
	BEGIN
		RETURN 0 --Indica que el número de Cabezas del Lote y la tabla Animal Movimiento no es el mismo
	END

	DECLARE @DiasEngorda INT

	SET @DiasEngorda = (
			SELECT DATEDIFF(DAY, pp.FechaInicio, getdate())
			FROM @TablaPesoProyectado pp
			)

	DECLARE @GananciaDiaria DECIMAL(10, 2)

	SET @GananciaDiaria = (
			SELECT lp.GananciaDiaria
			FROM LoteProyeccion lp
			WHERE lp.LoteID = @LoteID
			)

	DECLARE @GananciaCorral DECIMAL(10, 2)

	SET @GananciaCorral = @DiasEngorda * @GananciaDiaria

	DECLARE @PesoTotal INT

	SET @PesoTotal = (
			SELECT pp.Peso
			FROM @TablaPesoProyectado pp
			)

	DECLARE @PesoProyectado INT

	SET @PesoProyectado = (@PesoTotal / @Cabezas) + @GananciaCorral

	RETURN isnull(@PesoProyectado, 0)
END
