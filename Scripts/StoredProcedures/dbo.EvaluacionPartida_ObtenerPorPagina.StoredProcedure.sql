USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EvaluacionPartida_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EvaluacionPartida_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[EvaluacionPartida_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez
-- Create date: 16/05/2014
-- Description:  Obtener listado de Evaluaciones a imprimir
-- EvaluacionPartida_ObtenerPorPagina 1, '2014-05-14', 1, 15
-- =============================================
CREATE PROCEDURE [dbo].[EvaluacionPartida_ObtenerPorPagina]
  @OrganizacionID INT, 
  @FechaEvaluacion DATE, 
  @Inicio INT, 
  @Limite INT
AS
  BEGIN
    SET NOCOUNT ON;
	-- Declaracion de variables para el cursor
	DECLARE @ID INT, @LoteID INT, @LoteIDAnterior INT;
	CREATE TABLE #TablaTemporal (ID INT);
   SELECT ROW_NUMBER() OVER (ORDER BY EC.FolioEvaluacion ASC) AS RowNum,
		  EC.EvaluacionID, EC.FolioEvaluacion, EC.OrganizacionID, EC.LoteID, L.Lote, C.CorralID, C.Codigo, EG.PesoBruto, EG.PesoTara,
		  EG.FolioOrigen, EG.FechaEntrada, Org.OrganizacionID [OrganizacionOrigenID], Org.Descripcion [DescripcionOrigen], EC.FechaEvaluacion, 
		  EC.Cabezas, EC.EsMetafilaxia, EC.OperadorID, EC.NivelGarrapata, EC.MetafilaxiaAutorizada, EC.Justificacion, EC.Activo,
		  EC.FechaCreacion, EC.UsuarioCreacionID
	 INTO #Datos
	 FROM EvaluacionCorral EC
	INNER JOIN Lote L ON L.LoteID = EC.LoteID
	INNER JOIN Corral C ON C.CorralID = L.CorralID
	INNER JOIN EntradaGanado EG ON ( EG.LoteID  = L.LoteID AND EG.CorralID = C.CorralID)
	INNER JOIN Organizacion Org ON Org.OrganizacionID = EG.OrganizacionOrigenID
	INNER JOIN Operador O ON O.OperadorID = EC.OperadorID
	WHERE EC.OrganizacionID = @OrganizacionID
	  AND CAST(FechaEvaluacion AS DATE) = CAST(@FechaEvaluacion AS DATE)
	  AND EC.Activo = 1
	ORDER BY FolioEvaluacion
	-- Declaraci�n del cursor
	DECLARE curEvaluaciones CURSOR FOR SELECT  Dts.RowNum, Dts.LoteID FROM #Datos Dts;
	SET @LoteIDAnterior = 0;
	-- Apertura del cursor
	OPEN curEvaluaciones
	-- Lectura de la primera fila del cursor
		FETCH curEvaluaciones INTO @ID, @LoteID
			WHILE (@@FETCH_STATUS = 0 )
			BEGIN
			-- Lectura de la siguiente fila del cursor
				IF(@LoteIDAnterior != @LoteID)
					BEGIN
						SET @LoteIDAnterior = @LoteID; 
					END
				ELSE
					BEGIN
						INSERT INTO #TablaTemporal VALUES(@ID);
					END
				FETCH curEvaluaciones INTO @ID, @LoteID
			END
		-- Cierre del cursor
		CLOSE curEvaluaciones
	-- Liberar los recursos
	DEALLOCATE curEvaluaciones
	   SELECT Dts.EvaluacionID,
			  Dts.FolioEvaluacion,
			  Dts.OrganizacionID,
			  Dts.LoteID,
			  Dts.Lote,
			  Dts.CorralID,
			  Dts.Codigo,
			  Dts.PesoBruto,
			  Dts.PesoTara,
			  Dts.FolioOrigen,
			  Dts.FechaEntrada "FechaRecepcion",
			  Dts.OrganizacionOrigenID,
			  Dts.DescripcionOrigen,
			  Dts.FechaEvaluacion "FechaEvaluacion",
			  Dts.Cabezas,
			  Dts.EsMetafilaxia,
			  Dts.OperadorID,
			  Dts.NivelGarrapata,
			  Dts.MetafilaxiaAutorizada,
			  Dts.Justificacion,
			  Dts.Activo,
			  Dts.FechaCreacion "FechaCreacion",
			  Dts.UsuarioCreacionID,
			  dbo.ObtenerOrganizacionOrigenCorralLote(Dts.CorralID,Dts.LoteID) "OrganizacionOrigen",
			  dbo.ObtenerPartidasCorralLote(Dts.CorralID,Dts.LoteID,',') "Partidas"
		 FROM #Datos Dts
		WHERE RowNum BETWEEN @Inicio AND @Limite
		  AND RowNum NOT IN (SELECT ID FROM #TablaTemporal)
		SELECT 
			COUNT(EvaluacionID) AS TotalReg
		FROM #Datos 
	   WHERE RowNum NOT IN (SELECT ID FROM #TablaTemporal)
		DROP TABLE #Datos
		DROP TABLE #TablaTemporal
    SET NOCOUNT OFF;
END

GO
