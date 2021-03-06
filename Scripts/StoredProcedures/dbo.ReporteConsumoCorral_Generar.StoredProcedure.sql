USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteConsumoCorral_Generar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteConsumoCorral_Generar]
GO
/****** Object:  StoredProcedure [dbo].[ReporteConsumoCorral_Generar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================          
-- Author     : Cesar Vega          
-- Create date: 08/05/2014          
-- Description: Obtiene la informacion de consumo de corral por el ID del Lote          
-- SpName     : ReporteConsumoCorral_Generar 29727, '20150610', '20150807'          
--======================================================          
CREATE PROCEDURE [dbo].[ReporteConsumoCorral_Generar]          
 @LoteId int        
 , @FechaInicio DATE         
 , @FechaFin DATE         
AS          
BEGIN          
 --======================================================          
 --  D E C L A R A C I O N   D E   V A R I A B L E S          
 --======================================================          
 DECLARE @TipoProceso_EngordaPropio tinyint;          
 DECLARE @TipoProceso_Maquila tinyint;          
 DECLARE @TipoProceso_EngordaIntensivo tinyint;          
 DECLARE @TipoMovimiento_Corte tinyint;          
 DECLARE @TipoMovimiento_TraspasoGanado tinyint; 
 DECLARE @TipoMovimiento_Sacrificio tinyint;
 DECLARE @Matutino TINYINT, @Vespertino TINYINT          
 DECLARE @TipoAlmacenMateriaPrima TINYINT          
 DECLARE @TipoAlmacenPlantaAlimentos TINYINT          
 SET @TipoProceso_EngordaPropio = 1;          
 SET @TipoProceso_Maquila = 2;          
 SET @TipoProceso_EngordaIntensivo = 3;          
 SET @TipoMovimiento_Corte = 5;          
 SET @TipoMovimiento_TraspasoGanado = 17;  
 SET @TipoMovimiento_Sacrificio = 16;       
 SET @TipoAlmacenMateriaPrima = 6          
 SET @TipoAlmacenPlantaAlimentos = 8          
 SET @FechaInicio = DATEADD(dd, 0, DATEDIFF(dd, 0, @FechaInicio))          
 SET @FechaFin = DATEADD(dd, 0, DATEDIFF(dd, 0, @FechaFin))          
 SET @Matutino = 1          
 SET @Vespertino = 2 
DECLARE @T1 TABLE          
(          
	LoteId int, 
	OrganizacionId int,          
	CorralId int,
	Corral varchar(10),
	TipoProcesoId int,
	TipoProceso varchar(max),
	Encabezado varchar(max),
	Sexo char(1),
	TipoGanadoId tinyint,
	TipoGanado varchar(max)
)          
CREATE TABLE #tReparto          
(          
	RepartoID    BIGINT,
	Fecha     SMALLDATETIME,
	FormulaIDServida  INT,
	Cabezas    INT,
	CantidadServida  NUMERIC(18,2),
	LoteID    INT,
	RepartoDetalleID  BIGINT,    
	CostoPromedio DECIMAL(18,4)                
)         
CREATE TABLE #contenedordatos        
(          
	RepartoID    BIGINT,
	Fecha     SMALLDATETIME,
	FormulaIDServida  INT,
	Cabezas    INT,
	CantidadServida  NUMERIC(18,2),
	SERVIDOS_ACUMULADO NUMERIC(18,2),
	DIAS_ANIMAL INT,
	CONSUMO_DIA NUMERIC(18,2),
	PROMEDIO_ACUMULADO NUMERIC(18,2),
	PrecioPromedio NUMERIC(18,2),
	Importe NUMERIC(18,2)
)
CREATE TABLE #Dias    
(    
	Dias int,    
	FormulaIDServida int    
)
CREATE TABLE #Formulas        
(        
	FormulaIDServida  INT          
)        
 DECLARE @TipoProceso tinyint;          
 --======================================================          
 --  O B T E N E R   E N C A B E Z A D O          
 --======================================================          
	INSERT INTO @T1 (LoteId, OrganizacionId, CorralId, TipoProcesoId, Corral, TipoProceso)           
	SELECT          
		L.LoteID,          
		L.OrganizacionID,          
		L.CorralID,          
		L.TipoProcesoID,          
		C.Codigo,          
		TP.Descripcion          
	FROM           
	Lote L          
	INNER JOIN Corral C (NOLOCK)
		ON L.CorralID = C.CorralID          
	INNER JOIN TipoProceso TP (NOLOCK)
		ON L.TipoProcesoID = TP.TipoProcesoID          
	WHERE LoteID = @LoteId 
	SELECT @TipoProceso = TipoProcesoID FROM @T1          
	IF @TipoProceso = 1          
	BEGIN          
		UPDATE T          
		SET Encabezado = 'SUKARNE AGROINDUSTRIAL - ' + O.Descripcion          
		FROM @T1 T          
		INNER JOIN Organizacion O ON T.OrganizacionId = O.OrganizacionID          
		GOTO PASO2;        
	END
	IF @TipoProceso = 2 or @TipoProceso = 3          
	BEGIN          
		UPDATE T 
		SET Encabezado = P.Descripcion          
		FROM @T1 T          
		INNER JOIN EntradaGanado EG (NOLOCK) 
			ON T.OrganizacionId = EG.OrganizacionID AND T.LoteId = EG.LoteID          
		INNER JOIN EntradaGanadoCosteo EGC (NOLOCK)
			ON EG.EntradaGanadoID = EGC.EntradaGanadoID          
		INNER JOIN EntradaGanadoCosto EC (NOLOCK)
			ON EGC.EntradaGanadoCosteoID = EC.EntradaGanadoCosteoID          
		INNER JOIN Proveedor P (NOLOCK)
			ON EC.ProveedorID = P.ProveedorID          
	END
PASO2:          
	UPDATE T          
	SET Sexo = [dbo].ObtenerSexoGanadoCorral(T.OrganizacionId, T.LoteId)          
	FROM @T1 T           
	UPDATE T          
	SET TipoGanadoID = (select TipoGanadoID from [dbo].ObtenerTipoGanado(T.OrganizacionId, T.LoteID, @TipoMovimiento_Corte, T.Sexo)),
	TipoGanado = (select Descripcion from [dbo].ObtenerTipoGanado(T.OrganizacionId, T.LoteID, @TipoMovimiento_Corte, T.Sexo))          
	FROM @T1 T          
	SELECT Encabezado, Corral, TipoGanado, TipoProceso
	FROM @T1          
 --======================================================          
 --  O B T E N E R   D E T A L L E          
 --======================================================          
	INSERT INTO #tReparto          
	SELECT 
		R.RepartoID,
		R.Fecha,
		RD.FormulaIDServida,
		RD.Cabezas,
		RD.CantidadServida,
		R.LoteID,
		RD.RepartoDetalleID,
		RD.CostoPromedio          
	FROM          
	Reparto R (NOLOCK)         
	INNER JOIN RepartoDetalle RD (NOLOCK)
		ON R.RepartoID = RD.RepartoID AND RD.Servido = 1 AND RD.TipoServicioID IN (@Matutino, @Vespertino)          
	INNER JOIN @T1 t          
		ON (R.OrganizacionID = t.OrganizacionId)          
	WHERE  CAST(R.Fecha AS DATE) BETWEEN @FechaInicio AND @FechaFin AND R.LoteID = @LoteId          
    --ANIMALES MOVIMIENTOS
	 INSERT INTO #Dias    
	 SELECT  
	 COUNT( DISTINCT AC.Fecha),AC.FormulaIDServida         
	 FROM AnimalMovimiento AM  (NOLOCK)        
	 INNER JOIN AnimalConsumo AC (NOLOCK)
		ON AM.AnimalID = AC.AnimalID        
	 INNER JOIN Lote L (NOLOCK)          
		ON (AM.LoteID = L.LoteID)          
	 WHERE --AM.TipoMovimientoID = @TipoMovimiento_TraspasoGanado AND 
	 AM.Activo = 1          
	 AND AM.LoteID = @LoteId    
	 GROUP BY AC.FormulaIDServida     
	 ORDER BY AC.FormulaIDServida    
	--ANIMALES MOVIMIENTOS HISTORICO 
	 INSERT INTO #Dias    
	 SELECT  
	 COUNT( DISTINCT AC.Fecha),AC.FormulaIDServida         
	 FROM AnimalMovimientoHistorico AM  (NOLOCK)        
	 INNER JOIN AnimalConsumoHistorico AC (NOLOCK)
		ON AM.AnimalID = AC.AnimalID        
	 INNER JOIN Lote L (NOLOCK)          
		ON (AM.LoteID = L.LoteID)          
	 WHERE --AM.TipoMovimientoID = @TipoMovimiento_Sacrificio AND 
	 AM.Activo = 1          
	 AND AM.LoteID = @LoteId    
	 GROUP BY AC.FormulaIDServida     
	 ORDER BY AC.FormulaIDServida 
	-- select * from #Dias
	INSERT INTO #contenedordatos --MOVIMIENTOS NORMALES       
	SELECT            
		R.RepartoID,
		R.Fecha,
		R.FormulaIDServida,
		R.Cabezas,
		CantidadServida = ROUND(SUM( CAST( R.CantidadServida AS NUMERIC(18,2) ) ),0 ),
		CAST(0 AS NUMERIC(18,2)) AS [SERVIDOS_ACUMULADO],
		CASE WHEN  DATEDIFF(D, CAST(L.FechaInicio AS date), CAST(R.Fecha AS date)) = 0 
			THEN 1 * R.Cabezas 
			ELSE DATEDIFF(D, CAST(L.FechaInicio AS date), CAST(R.Fecha AS date))  * R.Cabezas 
		END  AS [DIAS_ANIMAL],
		SUM( CASE WHEN  DATEDIFF(D, CAST(L.FechaInicio AS date), CAST(R.Fecha AS date)) = 0 
			THEN  COALESCE( CAST(R.CantidadServida / NULLIF(R.Cabezas, 0) AS NUMERIC(18,2)), 0) /1
			ELSE COALESCE( CAST(R.CantidadServida / 
				NULLIF(R.Cabezas, 0) AS NUMERIC(18,2)), 0)  --/ NULLIF((DATEDIFF(D, CAST(L.FechaInicio AS date), CAST(R.Fecha AS date)) ), 0)
			END 
		) AS [CONSUMO_DIA],
		CAST(0 AS NUMERIC(18,2)) AS [PROMEDIO_ACUMULADO],
		CAST(R.CostoPromedio AS NUMERIC(18,2)) AS PrecioPromedio,
		ROUND(SUM( CAST( R.CantidadServida AS NUMERIC(18,2) ) ),0 ) *  CAST(R.CostoPromedio AS NUMERIC(18,2)) as Importe   --* CAST( R.CantidadServida AS NUMERIC(18,2)) AS NUMERIC(18,2) ) --SUM() 		               
	FROM            
	#tReparto R            
	INNER JOIN Formula F (NOLOCK) 
		ON R.FormulaIDServida = F.FormulaID            
	INNER JOIN LOTE L (NOLOCK) 
		ON R.LoteID = L.LoteID            	
	GROUP BY  R.RepartoID,R.Fecha,R.FormulaIDServida,R.Cabezas,DATEDIFF(D, CAST(L.FechaInicio AS date), CAST(R.Fecha AS date))  * R.Cabezas,L.FechaInicio,R.Fecha,R.CostoPromedio 
	UNION ALL 
	--ANIMALES MOVIMIENTOS
	SELECT         
		RepartoID = 0,
		Fecha = L.FechaInicio,
		AC.FormulaIDServida,
		L.Cabezas,
		CanitdadServida = ROUND(SUM( CAST( AC.Cantidad AS NUMERIC(18,2) ) ),0 ),
		CAST(0 AS NUMERIC(18,2)) AS [SERVIDOS_ACUMULADO],
		0 AS [DIAS_ANIMAL],
		SUM( CASE WHEN D.Dias = 0 
			 THEN 
				COALESCE( CAST( AC.Cantidad AS NUMERIC(18,2)) / NULLIF(L.Cabezas, 0) / 1 , 0)  
			 ELSE 
				COALESCE( CAST( AC.Cantidad AS NUMERIC(18,2)) / NULLIF(L.Cabezas, 0)  , 0)-- / NULLIF(D.Dias, 0)
			 END 
		)  AS [CONSUMO_DIA],
		CAST(0 AS NUMERIC(18,2)) AS [PROMEDIO_ACUMULADO],
		CAST(AVG(rd.CostoPromedio) AS NUMERIC(18,2)) AS PrecioPromedio,
		CAST(AVG(rd.CostoPromedio) AS NUMERIC(18,2)) *   ROUND(SUM( CAST( AC.Cantidad AS NUMERIC(18,2) ) ),0 )  as Importe
	FROM AnimalMovimiento AM (NOLOCK)          
	INNER JOIN AnimalConsumo AC (NOLOCK) 
		ON AM.AnimalID = AC.AnimalID       
		INNER JOIN RepartoDetalle rd (nolock) on ac.RepartoID = rd.RepartoID 
	INNER JOIN Lote L (NOLOCK)           
		ON (AM.LoteID = L.LoteID)          
	INNER JOIN Formula F (NOLOCK)            
		ON (AC.FormulaIDServida = F.FormulaID)        
	INNER JOIN #Dias D 
		ON (F.FormulaID = D.FormulaIDServida)     	
	WHERE --AM.TipoMovimientoID = @TipoMovimiento_TraspasoGanado AND 
	AM.Activo = 1 AND AM.LoteID = @LoteId            
	GROUP BY L.FechaInicio,AC.FormulaIDServida ,L.Cabezas        
 --   UNION ALL 
 --   --ANIMALES MOVIMIENTOS HISTORICO
 --   SELECT 
	--RepartoID = 0,
	--Fecha = L.FechaInicio,
	--AC.FormulaIDServida,
	--L.CabezasInicio,
	--CanitdadServida = ROUND(SUM( CAST( AC.Cantidad AS NUMERIC(18,2) ) ),0 ) ,
	--CAST(0 AS NUMERIC(18,2)) AS [SERVIDOS_ACUMULADO],
	--0 AS [DIAS_ANIMAL],
	--SUM( CASE WHEN D.Dias = 0 
	--	 THEN 
	--		COALESCE( CAST( AC.Cantidad AS NUMERIC(18,2)) / NULLIF(L.CabezasInicio, 0) / 1 , 0)  
	--	 ELSE 
	--		COALESCE( CAST( AC.Cantidad AS NUMERIC(18,2)) / NULLIF(L.CabezasInicio, 0)  , 0) -- / NULLIF(D.Dias, 0)
	--	 END 
	--)  AS [CONSUMO_DIA],
	--CAST(0 AS NUMERIC(18,2)) AS [PROMEDIO_ACUMULADO],
	--CAST(AVG(rd.CostoPromedio) AS NUMERIC(18,2)) AS PrecioPromedio,
	--	CAST(AVG(rd.CostoPromedio) AS NUMERIC(18,2)) *   ROUND(SUM( CAST( AC.Cantidad AS NUMERIC(18,2) ) ),0 )  as Importe
	--FROM AnimalMovimientoHistorico AM (NOLOCK)          
	--INNER JOIN AnimalConsumoHistorico AC (NOLOCK) 
	--	ON AM.AnimalID = AC.AnimalID        
	--	INNER JOIN RepartoDetalle rd (nolock) on ac.RepartoID = rd.RepartoID 
	--INNER JOIN Lote L (NOLOCK)           
	--	ON (AM.LoteID = L.LoteID)          
	--INNER JOIN Formula F (NOLOCK)            
	--	ON (AC.FormulaIDServida = F.FormulaID)        
	--INNER JOIN #Dias D 
	--	ON (F.FormulaID = D.FormulaIDServida)     	
	--WHERE --AM.TipoMovimientoID = @TipoMovimiento_Sacrificio AND 
	--AM.Activo = 1 AND AM.LoteID = @LoteId            
	--GROUP BY L.FechaInicio,AC.FormulaIDServida ,L.CabezasInicio   
		SELECT DISTINCT
		RepartoID,
		Fecha,
		FormulaIDServida,
		Cabezas,
		CantidadServida,
		SERVIDOS_ACUMULADO,
		DIAS_ANIMAL,
		CONSUMO_DIA,
		PROMEDIO_ACUMULADO,
		PrecioPromedio,
		Importe         
	FROM #contenedordatos       
	ORDER BY Fecha,FormulaIDServida       
	--MOVIMIENTOS TRASPASADOS        
	SELECT         
	AC.FormulaIDServida,
	F.Descripcion,
	Peso = sum( CAST( AC.Cantidad AS NUMERIC(18,2) )  ),
	L.Cabezas,
	DATEDIFF(D, L.FechaInicio, AM.FechaMovimiento) AS [DIAS_ANIMAL_TRANSFERIDOS],
	AC.Fecha
	FROM AnimalMovimiento AM (NOLOCK) 
	INNER JOIN AnimalConsumo AC (NOLOCK) 
		ON AM.AnimalID = AC.AnimalID        
	INNER JOIN Lote L (NOLOCK) 
		ON (AM.LoteID = L.LoteID)          
	INNER JOIN Formula F (NOLOCK) 
		ON (AC.FormulaIDServida = F.FormulaID)        
	WHERE AM.TipoMovimientoID = @TipoMovimiento_TraspasoGanado AND AM.Activo = 1 AND AM.LoteID = @LoteId            
	GROUP BY AC.FormulaIDServida ,F.Descripcion,L.Cabezas,DATEDIFF(D, L.FechaInicio, AM.FechaMovimiento) ,AC.Fecha        
	ORDER BY AC.Fecha        
    --FORMULAS DE DIAS NORMALES      
	INSERT INTO #Formulas        
	SELECT DISTINCT R.FormulaIDServida               
	FROM #tReparto R            
	INNER JOIN Formula F (NOLOCK) 
		ON R.FormulaIDServida = F.FormulaID            
	INNER JOIN LOTE L (NOLOCK) 
		ON R.LoteID = L.LoteID            
	INNER JOIN AlmacenInventario AI (NOLOCK) 
		ON F.ProductoID = AI.ProductoID             
	INNER JOIN Almacen A (NOLOCK) 
		ON (AI.AlmacenID = A.AlmacenID AND (A.TipoAlmacenID = @TipoAlmacenMateriaPrima OR A.TipoAlmacenID = @TipoAlmacenPlantaAlimentos))            
	GROUP BY  R.FormulaIDServida 
	--SACAMOS LAS FORMULAS PARA LOS MOVIMIENTOS DE TRASPASOS 
	INSERT INTO #Formulas
	SELECT 
		DISTINCT AC.FormulaIDServida             
	FROM AnimalMovimiento AM (NOLOCK) 
	INNER JOIN AnimalConsumo AC (NOLOCK) 
		ON AM.AnimalID = AC.AnimalID        
	INNER JOIN Lote L  (NOLOCK) 
		ON (AM.LoteID = L.LoteID)          
	INNER JOIN Formula F   (NOLOCK) 
		ON (AC.FormulaIDServida = F.FormulaID)        
	WHERE --AM.TipoMovimientoID = @TipoMovimiento_TraspasoGanado AND 
	AM.Activo = 1 AND AM.LoteID = @LoteId   
	AND AC.FormulaIDServida NOT IN (SELECT FormulaIDServida FROM #Formulas)     
	GROUP BY AC.FormulaIDServida
    --SACAMOS LAS FORMULAS PARA LOS MOVIMIENTOS DE SACRIFICIOS
    INSERT INTO #Formulas
	SELECT 
		DISTINCT AC.FormulaIDServida             
	FROM AnimalMovimientoHistorico AM (NOLOCK) 
	INNER JOIN AnimalConsumoHistorico AC (NOLOCK) 
		ON AM.AnimalID = AC.AnimalID        
	INNER JOIN Lote L  (NOLOCK) 
		ON (AM.LoteID = L.LoteID)          
	INNER JOIN Formula F   (NOLOCK) 
		ON (AC.FormulaIDServida = F.FormulaID)        
	WHERE --AM.TipoMovimientoID = @TipoMovimiento_Sacrificio AND 
	AM.Activo = 1 AND AM.LoteID = @LoteId   
	AND AC.FormulaIDServida NOT IN (SELECT FormulaIDServida FROM #Formulas)     
	GROUP BY AC.FormulaIDServida
  --======================================================          
 --  O B T E N E R   C A T A L O G O S          
 --======================================================          
	SELECT          
		F.FormulaID,
		F.Descripcion,
		F.TipoFormulaID,
		F.ProductoID,
		F.Activo
	FROM          
		#Formulas  tmpf        
	INNER JOIN Formula F (NOLOCK)  
		ON tmpf.FormulaIDServida = F.FormulaID         
 DROP TABLE #tReparto        
 DROP TABLE #Formulas          
 DROP TABLE #contenedordatos     
 DROP TABLE #Dias    
END 

GO
