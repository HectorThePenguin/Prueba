USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_AuxiliarInventario]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_AuxiliarInventario]
GO
/****** Object:  StoredProcedure [dbo].[Corral_AuxiliarInventario]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 21/02/2014
-- Description: Obtiene los datos para el reporte dia a dia
-- SpName     : Corral_AuxiliarInventario '131', 5
--======================================================
CREATE PROCEDURE [dbo].[Corral_AuxiliarInventario]
@Corral CHAR(10)
, @OrganizacionID INT
AS
BEGIN

	SET NOCOUNT ON

		CREATE TABLE #tCorralAuxiliar
		(
			CorralID			INT
			, OrganizacionID	INT
			, Codigo			VARCHAR(20)
			, TipoCorralID		INT
			, Capacidad			INT
			, MetrosLargo		INT
			, MetrosAncho		BIGINT
			, Seccion			INT
			, Orden				INT
			, Activo			BIT
			, LoteID			INT
			, Lote				VARCHAR(20)
			, LoteActivo		BIT
			, Cabezas			INT
			, Organizacion		VARCHAR(100)
			, TipoGanadoID		INT
			, TipoGanado		VARCHAR(100)
			, ClasificacionID	INT
			, Clasificacion		VARCHAR(50)
			, FechaInicio		SMALLDATETIME
			, TipoCorral		VARCHAR(50)
			, GrupoCorralID		INT
		)

		INSERT INTO #tCorralAuxiliar
		SELECT C.CorralID
			,  C.OrganizacionID
			,  C.Codigo
			,  C.TipoCorralID
			,  C.Capacidad
			,  C.MetrosLargo
			,  C.MetrosAncho
			,  C.Seccion
			,  C.Orden
			,  C.Activo
			,  L.LoteID
			,  L.Lote
			,  L.Activo AS LoteActivo
			,  L.Cabezas
			,  O.Descripcion AS Organizacion
			,  0
			,  ''
			,  0
			,  ''
			,  NULL
			,  TC.Descripcion AS TipoCorral
			,  GC.GrupoCorralID
		FROM Corral C
		INNER JOIN Lote L
			ON (C.CorralID = L.CorralID)
		INNER JOIN Organizacion O
			ON (C.OrganizacionID = O.OrganizacionID)
		INNER JOIN TipoCorral TC
			ON (C.TipoCorralID = TC.TipoCorralID)
		INNER JOIN GrupoCorral GC
			ON (TC.GrupoCorralID = GC.GrupoCorralID)
		WHERE LTRIM(RTRIM(C.Codigo)) = LTRIM(RTRIM(@Corral))
			AND C.OrganizacionID = @OrganizacionID
		
		DECLARE @TipoMovimientoCorte INT
		SET @TipoMovimientoCorte = 5

		DECLARE @Peso NUMERIC(18,2)

		SELECT @Peso = SUM(AM.Peso)
		FROM AnimalMovimiento AM
		INNER JOIN #tCorralAuxiliar CA
			ON (AM.LoteID = CA.LoteID
				AND AM.CorralID = CA.CorralID)
		WHERE AM.TipoMovimientoID = @TipoMovimientoCorte

		DECLARE @Cabezas INT
		SET @Cabezas = (SELECT SUM(Cabezas) FROM #tCorralAuxiliar)
		if @Cabezas = 0
		begin 
			set @Cabezas = 1
		end
		SET @Peso = @Peso / @Cabezas

		DECLARE @Sexo CHAR(1)

		SELECT TOP 1 @Sexo = TG.Sexo
		FROM #tCorralAuxiliar CA
		INNER JOIN AnimalMovimiento AM
			ON (CA.LoteID = AM.LoteID
				AND CA.CorralID = AM.CorralID)
		INNER JOIN Animal A
			ON (AM.AnimalID = A.AnimalID)
		INNER JOIN TipoGanado TG
			ON (A.TipoGanadoID = TG.TipoGanadoID)

		DECLARE @TipoGanadoID INT, @TipoGanado VARCHAR(100)

		SELECT @TipoGanadoID = TipoGanadoID
			,  @TipoGanado = Descripcion
		FROM TipoGanado
		WHERE @Peso BETWEEN PesoMinimo AND PesoMaximo
			AND Sexo = @Sexo

		CREATE TABLE #tClasificacionCorral
		(  ClasificacionID INT,
		   Clasificacion varchar(50),
		   Animales INT
		)

		INSERT #tClasificacionCorral 
		SELECT TOP 1 Clasificacion.ClasificacionGanadoID, Clasificacion.Descripcion, MAX(Clasificacion.Animales)
		FROM 
		(
			SELECT COUNT(AM.AnimalID) AS Animales
				,  A.ClasificacionGanadoID
				,  CG.Descripcion
			FROM #tCorralAuxiliar CA
			INNER JOIN AnimalMovimiento AM
				ON (CA.LoteID = AM.LoteID
					AND CA.CorralID = AM.CorralID)
			INNER JOIN Animal A
				ON (AM.AnimalID = A.AnimalID)
			INNER JOIN ClasificacionGanado CG
				ON (A.ClasificacionGanadoID = CG.ClasificacionGanadoID)
			GROUP BY A.ClasificacionGanadoID
				,    CG.Descripcion
		) Clasificacion 
		GROUP BY Clasificacion.ClasificacionGanadoID
			,	 Clasificacion.Descripcion ORDER BY MAX(Clasificacion.Animales) DESC

		DECLARE @FechaInicio SMALLDATETIME
		DECLARE @GrupoCorralID INT
		
		SELECT @GrupoCorralID = GrupoCorralID
		FROM #tCorralAuxiliar		

		IF (@GrupoCorralID = 1) --RECEPCION
		BEGIN
			SELECT TOP 1 @FechaInicio = FechaEntrada
			FROM EntradaGanado EG
			INNER JOIN #tCorralAuxiliar CA
				ON (EG.CorralID = CA.CorralID
					AND EG.LoteID = CA.LoteID)
		END

		IF (@GrupoCorralID = 2 OR @GrupoCorralID = 3) --PRODUCCION || ENFERMERIA
		BEGIN
			SELECT TOP 1 @FechaInicio = L.FechaInicio
			FROM Lote L
			INNER JOIN #tCorralAuxiliar CA
				ON (L.CorralID = CA.CorralID
					AND L.LoteID = CA.LoteID)
		END

		UPDATE #tCorralAuxiliar
		SET TipoGanadoID = ISNULL(@TipoGanadoID, 0)
			, TipoGanado = ISNULL(@TipoGanado, '')
			, ClasificacionID = ISNULL((SELECT ClasificacionId from #tClasificacionCorral), 0)
			, Clasificacion = ISNULL((SELECT Clasificacion from #tClasificacionCorral), '')
			, FechaInicio = @FechaInicio

		SELECT CorralID			
			, OrganizacionID	
			, Codigo			
			, TipoCorralID		
			, Capacidad			
			, MetrosLargo		
			, MetrosAncho		
			, Seccion			
			, Orden				
			, Activo			
			, LoteID			
			, Lote				
			, LoteActivo		
			, Cabezas			
			, Organizacion		
			, TipoGanadoID		
			, TipoGanado		
			, ClasificacionID
			, Clasificacion
			, FechaInicio
			, TipoCorral
		FROM #tCorralAuxiliar
		where LoteActivo = 1

	SET NOCOUNT OFF

	DROP TABLE #tCorralAuxiliar
	DROP TABLE #tClasificacionCorral

END

GO
