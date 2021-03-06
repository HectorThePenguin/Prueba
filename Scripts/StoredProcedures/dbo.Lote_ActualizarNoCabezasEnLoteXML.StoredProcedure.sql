USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizarNoCabezasEnLoteXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ActualizarNoCabezasEnLoteXML]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizarNoCabezasEnLoteXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor: Gilberto Carranza
-- Fecha: 2015-05-19
-- Descripción:	Actualizar el numero de cabezas en lote
-- =============================================
CREATE PROCEDURE [dbo].[Lote_ActualizarNoCabezasEnLoteXML] @LotesDestinoXML XML
	,@LotesOrigenXML XML
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #tLotesDestino (
		LoteID INT
		,Cabezas INT
		,CabezasInicio INT
		,UsuarioModificacionID INT
		)

	CREATE TABLE #tLotesOrigen (
		LoteID INT
		,Cabezas INT
		,CabezasInicio INT
		,UsuarioModificacionID INT
		)


	INSERT INTO #tLotesDestino
	SELECT t.item.value('./LoteID[1]', 'INT') AS LoteID
		,  t.item.value('./Cabezas[1]', 'INT') AS Cabezas
		,  t.item.value('./CabezasInicio[1]', 'INT') AS CabezasInicio
		,  t.item.value('./UsuarioModificacionID[1]', 'INT') AS UsuarioModificacionID
	FROM @LotesDestinoXML.nodes('ROOT/LotesDestino') AS T (item)

	INSERT INTO #tLotesOrigen
	SELECT t.item.value('./LoteID[1]', 'INT') AS LoteID
		,  t.item.value('./Cabezas[1]', 'INT') AS Cabezas
		,  t.item.value('./CabezasInicio[1]', 'INT') AS CabezasInicio
		,  t.item.value('./UsuarioModificacionID[1]', 'INT') AS UsuarioModificacionID
	FROM @LotesOrigenXML.nodes('ROOT/LotesOrigen') AS T (item)

	DECLARE @Fecha DATETIME
	SET @Fecha = GETDATE()

	UPDATE L
	SET L.Cabezas = A.Cabezas
		,L.UsuarioModificacionID = A.UsuarioModificacionID
		,L.FechaModificacion = @Fecha
	FROM Lote L	
	INNER JOIN 
	(
		SELECT tLD.LoteID
			,  tLD.UsuarioModificacionID
			,  COUNT(AM.AnimalID)	AS Cabezas
		FROM #tLotesDestino tLD 
		INNER JOIN AnimalMovimiento AM 
			ON (tLD.LoteID = AM.LoteID
				AND AM.Activo = 1
				AND TipoMovimientoID NOT IN (8, 11, 16))
		GROUP BY tLD.LoteID
			  ,  tLD.UsuarioModificacionID
	) A ON (L.LoteID = A.LoteID)
	WHERE L.TipoCorralID <> 1

	UPDATE L
	SET L.CabezasInicio = L.Cabezas
	FROM Lote L
	INNER JOIN #tLotesDestino tLD ON (L.LoteID = tLD.LoteID)
	INNER JOIN AnimalMovimiento AM ON (
			tLD.LoteID = AM.LoteID
			AND AM.Activo = 1
			)
	WHERE L.TipoCorralID <> 1
		AND L.Cabezas > L.CabezasInicio

	UPDATE L
	SET L.Cabezas = A.Cabezas
		,L.UsuarioModificacionID = A.UsuarioModificacionID
		,L.FechaModificacion = @Fecha
		,L.FechaSalida = @Fecha
	FROM Lote L
	INNER JOIN 
	(
		SELECT tLD.LoteID
			,  tLD.UsuarioModificacionID
			,  COUNT(AM.AnimalID)	AS Cabezas
		FROM #tLotesOrigen tLD 
		INNER JOIN AnimalMovimiento AM 
			ON (tLD.LoteID = AM.LoteID
				AND AM.Activo = 1
				AND TipoMovimientoID NOT IN (8, 11, 16))
		GROUP BY tLD.LoteID
			  ,  tLD.UsuarioModificacionID
	) A ON (L.LoteID = A.LoteID)
	WHERE L.TipoCorralID <> 1

	UPDATE EG
	SET Manejado = 1
		,UsuarioModificacionID = tLO.UsuarioModificacionID
		,FechaModificacion = @Fecha
	FROM EntradaGanado EG
	INNER JOIN #tLotesOrigen tLO ON (EG.LoteID = tLO.LoteID)
	INNER JOIN Lote L
		ON (tLO.LoteID = L.LoteID
			AND L.Cabezas = 0)

	/* Si se sacan todas las cabezas del lote se inactiva el lote */
	UPDATE L
	SET Activo = 0
		,UsuarioModificacionID = tLO.UsuarioModificacionID
		,FechaModificacion = @Fecha
	FROM Lote L
	INNER JOIN #tLotesOrigen tLO ON (L.LoteID = tLO.LoteID)
	WHERE L.Cabezas = 0

	SET NOCOUNT OFF

	DROP TABLE #tLotesDestino

	DROP TABLE #tLotesOrigen
END

GO
