USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteAuxiliarInventario_ObtenerReporte2]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteAuxiliarInventario_ObtenerReporte2]
GO
/****** Object:  StoredProcedure [dbo].[ReporteAuxiliarInventario_ObtenerReporte2]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================        
-- Author     : Gilberto Carranza        
-- Create date: 21/02/2014        
-- Description: Obtiene los datos para el reporte dia a dia        
-- SpName     : ReporteAuxiliarInventario_ObtenerReporte 40673        
--======================================================        
CREATE PROCEDURE [dbo].[ReporteAuxiliarInventario_ObtenerReporte2]        
@LoteID INT            
AS            
BEGIN          
 SET NOCOUNT ON            

  DECLARE @GrupoCorralID  INT 

	DECLARE @ANIMALES TABLE (
			AnimalID INT, 
			FolioEntrada INT,
			Arete VARCHAR(15)
	)	

	DECLARE @ANIMALESMOVIMIENTOSFINAL TABLE (
			AnimalID INT,
			AnimalMovimientoID BIGINT,
			OrganizacionID INT,
			CorralID INT,
			LoteID INT,
			FechaMovimiento DATETIME,
			TipoMovimientoID INT,
			FolioEntrada BIGINT,
			LoteIDOrigen INT,
			AnimalMovimientoIDAnterior BIGINT,
			EsEntrada BIT
	)

 DECLARE @ENTRADAS TABLE(    
  OrganizacionID INT,        
  FolioOrigen	INT,       
  CorralID INT,
  CodigoCorral VARCHAR(10),
  LoteID INT,
  FechaEntrada SMALLDATETIME,
	FolioEntrada INT,
  OrganizacionOrigenID INT,
  TipoOrganizacionID INT,
  CabezasRecibidas  INT
 )      
      
 DECLARE @INTERFACESALIDAANIMAL TABLE(
	Id INT IDENTITY(1,1),
	SalidaID INT,
	Arete  VARCHAR(50),
	CorralID INT
 )           
   
 
    
 SELECT @GrupoCorralID = TC.GrupoCorralID            
 FROM Lote LO
 INNER JOIN TipoCorral TC ON TC.TipoCorralID = Lo.TipoCorralID            
 WHERE LO.LoteID = @LoteID

 IF(@GrupoCorralID != 1) /* Si no es de Recepcion */            
  BEGIN
		--Obtienes todos los animales que haya tenido el corral
		INSERT INTO @Animales 
		SELECT DISTINCT A.AnimalID,
										A.FolioEntrada,
										A.Arete
		FROM AnimalMovimiento AM 
		INNER JOIN Animal A ON (A.AnimalID = AM.AnimalID)
		WHERE LoteID = @LoteID		
		ORDER BY A.AnimalID

		--Obtienes todos los animales que haya tenido el corral en el historico
		INSERT INTO @Animales 
		SELECT DISTINCT A.AnimalID,
										A.FolioEntrada,
										A.Arete
		FROM AnimalMovimientoHistorico AM 
		INNER JOIN AnimalHistorico A ON (A.AnimalID = AM.AnimalID)
		WHERE LoteID = @LoteID			
		ORDER BY A.AnimalID

	
			--Obtienes los movimientos del animal
			INSERT INTO @ANIMALESMOVIMIENTOSFINAL
			SELECT
					AM.AnimalID,            
					AM.AnimalMovimientoID,
					AM.OrganizacionID,            
					AM.CorralID,            
					AM.LoteID,            
					AM.FechaMovimiento,            
					AM.TipoMovimientoID,            
					A.FolioEntrada,    
					AM.LoteIDOrigen,     
					AnimalMovimientoIDAnterior = ISNULL(AM.AnimalMovimientoIDAnterior,0),
					1 AS EsEntrada
			FROM AnimalMovimiento AM
			INNER JOIN @ANIMALES A ON (AM.AnimalID = A.AnimalID)
			where AM.LoteID = @LoteID
			AND (Am.LoteIDOrigen <> @LoteID or am.LoteIDOrigen is null)

			--Obtienes los movimientos del animal
			INSERT INTO @ANIMALESMOVIMIENTOSFINAL
			SELECT
					AM.AnimalID,            
					AM.AnimalMovimientoID,
					AM.OrganizacionID,            
					AM.CorralID,            
					AM.LoteID,            
					AM.FechaMovimiento,            
					AM.TipoMovimientoID,            
					A.FolioEntrada,    
					AM.LoteIDOrigen,     
					AnimalMovimientoIDAnterior = ISNULL(AM.AnimalMovimientoIDAnterior,0),
					0 As EsEntrada
			FROM AnimalMovimiento AM
			INNER JOIN @ANIMALES A ON (AM.AnimalID = A.AnimalID)
			where AM.LoteIDOrigen = @LoteID
			AND am.LoteID <> @LoteID

			INSERT INTO @ANIMALESMOVIMIENTOSFINAL
			SELECT
					AM.AnimalID,            
					AM.AnimalMovimientoID,
					AM.OrganizacionID,            
					AM.CorralID,            
					AM.LoteID,            
					AM.FechaMovimiento,            
					AM.TipoMovimientoID,            
					A.FolioEntrada,    
					AM.LoteIDOrigen,     
					AnimalMovimientoIDAnterior = ISNULL(AM.AnimalMovimientoIDAnterior,0),
					1 As EsEntrada
			FROM AnimalMovimientoHistorico AM
			INNER JOIN @ANIMALES A ON (AM.AnimalID = A.AnimalID)
			where am.LoteID = @LoteID
			AND (am.LoteIDOrigen <> @LoteID or am.LoteIDOrigen is null)

			INSERT INTO @ANIMALESMOVIMIENTOSFINAL
			SELECT
					AM.AnimalID,            
					AM.AnimalMovimientoID,
					AM.OrganizacionID,            
					AM.CorralID,            
					AM.LoteID,            
					AM.FechaMovimiento,            
					AM.TipoMovimientoID,            
					A.FolioEntrada,    
					AM.LoteIDOrigen,     
					AnimalMovimientoIDAnterior = ISNULL(AM.AnimalMovimientoIDAnterior,0),
					0 As EsEntrada
			FROM AnimalMovimientoHistorico AM
			INNER JOIN @ANIMALES A ON (AM.AnimalID = A.AnimalID)
			where am.LoteIDOrigen = @LoteID
			and am.LoteID <> @LoteID	
	
       
   INSERT INTO @ENTRADAS            
   SELECT 
   EG.OrganizacionID
    ,   EG.FolioOrigen            
    ,  EG.CorralID            
    ,  co.Codigo            
    ,  EG.LoteID            
    ,  EG.FechaEntrada            
    ,  EG.FolioEntrada            
    ,  EG.OrganizacionOrigenID            
    ,  TOO.TipoOrganizacionID            
    ,  EG.CabezasRecibidas            
   FROM EntradaGanado EG            
   INNER JOIN Lote L ON (EG.CorralID = L.CorralID AND EG.LoteID = L.LoteID)            
   INNER JOIN Corral co ON L.CorralID = co.CorralID            
   INNER JOIN Organizacion O ON (EG.OrganizacionOrigenID = O.OrganizacionID)            
   INNER JOIN TipoOrganizacion TOO ON (O.TipoOrganizacionID = TOO.TipoOrganizacionID)            
   WHERE EG.FolioEntrada IN (SELECT FolioEntrada FROM @ANIMALES)            
  END            
 ELSE            
  BEGIN
		INSERT INTO @ENTRADAS
		SELECT 
		EG.OrganizacionID,
			EG.FolioOrigen,       
		  C.CorralID,
		  C.Codigo,
		  L.LoteID,
		  EG.FechaEntrada,
			EG.FolioEntrada,
		  EG.OrganizacionOrigenID,
		  TOO.TipoOrganizacionID,
		  EG.CabezasRecibidas
		FROM EntradaGanado EG
		INNER JOIN Corral C ON (EG.CorralID = C.CorralID)
		INNER JOIN Lote L ON (EG.CorralID = L.CorralID AND EG.LoteID = L.LoteID)            
				INNER JOIN Corral co ON L.CorralID = co.CorralID            
				INNER JOIN Organizacion O ON (EG.OrganizacionOrigenID = O.OrganizacionID)            
				INNER JOIN TipoOrganizacion TOO ON (O.TipoOrganizacionID = TOO.TipoOrganizacionID) 
		WHERE 
					 EG.LoteID = @LoteID					
					

		INSERT INTO @ANIMALES
		SELECT 
			A.AnimalID,
			A.FolioEntrada,
			A.Arete
		FROM Animal A WHERE FolioEntrada IN (SELECT FolioEntrada FROM @ENTRADAS e where a.OrganizacionIDEntrada = e.OrganizacionID)
		

		INSERT INTO @ANIMALES
		SELECT 
			A.AnimalID,
			A.FolioEntrada,
			A.Arete
		FROM AnimalHistorico A WHERE FolioEntrada IN (SELECT FolioEntrada FROM @ENTRADAS e where a.OrganizacionIDEntrada = e.OrganizacionID)


		--Obtienes los movimientos del animal
			INSERT INTO @ANIMALESMOVIMIENTOSFINAL
			SELECT
					AM.AnimalID,            
					AM.AnimalMovimientoID,
					AM.OrganizacionID,            
					AM.CorralID,            
					AM.LoteID,            
					AM.FechaMovimiento,            
					AM.TipoMovimientoID,            
					A.FolioEntrada,    
					AM.LoteIDOrigen,     
					AnimalMovimientoIDAnterior = ISNULL(AM.AnimalMovimientoIDAnterior,0),
					0 AS EsEntrada
			FROM AnimalMovimiento AM
			INNER JOIN @ANIMALES A ON (AM.AnimalID = A.AnimalID)
			and am.AnimalMovimientoID = (select min(AnimalMovimientoID) from AnimalMovimiento am1 where a.AnimalID = am1.AnimalID)
		

			INSERT INTO @ANIMALESMOVIMIENTOSFINAL
			SELECT
					AM.AnimalID,            
					AM.AnimalMovimientoID,
					AM.OrganizacionID,            
					AM.CorralID,            
					AM.LoteID,            
					AM.FechaMovimiento,            
					AM.TipoMovimientoID,            
					A.FolioEntrada,    
					AM.LoteIDOrigen,     
					AnimalMovimientoIDAnterior = ISNULL(AM.AnimalMovimientoIDAnterior,0),
					0 As EsEntrada
			FROM AnimalMovimientoHistorico AM
			INNER JOIN @ANIMALES A ON (AM.AnimalID = A.AnimalID)	
			and am.AnimalMovimientoID = (select min(AnimalMovimientoID) from AnimalMovimientoHistorico am1 where a.AnimalID = am1.AnimalID)	

			--INSERT INTO @INTERFACESALIDAANIMAL
			--select 
			--isa.SalidaID
			--,isa.Arete
			--,e.CorralID
			--from InterfaceSalidaAnimal isa
			--inner join @ENTRADAS e on isa.SalidaID = e.FolioOrigen and isa.OrganizacionID = e.OrganizacionOrigenID

  END
    
 
          
 SELECT tM.AnimalID                
  ,  tM.AnimalMovimientoID             
  ,  tM.OrganizacionID              
  ,  tM.CorralID                   
  ,  tM.LoteID                
  ,  tM.FechaMovimiento              
  ,  tM.TipoMovimientoID            
  ,  FolioEntrada = ISNULL(tM.FolioEntrada,0)            
  ,  TOO.TipoOrganizacionID   
  ,  co.Codigo AS CorralOrigen
 FROM @ANIMALESMOVIMIENTOSFINAL tM            
 INNER JOIN Organizacion O            
  ON (tM.OrganizacionID = O.OrganizacionID)            
 INNER JOIN TipoOrganizacion TOO            
  ON (O.TipoOrganizacionID = TOO.TipoOrganizacionID)    
LEFT JOIN Lote l (nolock)  on tM.LoteIDOrigen = l.LoteID
left join AnimalMovimiento am on tm.AnimalMovimientoID = am.AnimalMovimientoID  
left join Lote lo on am.LoteIDOrigen = lo.LoteID AND am.LoteIDOrigen <> @LoteID
left join Corral co on lo.CorralID = co.CorralID         
 ORDER BY tM.FechaMovimiento ASC


 SELECT EG.FolioOrigen            
  ,  EG.CorralID            
  ,  EG.CodigoCorral          
  ,  EG.LoteID            
  ,  EG.FechaEntrada            
  ,  EG.FolioEntrada            
  ,  EG.OrganizacionOrigenID            
  ,  EG.TipoOrganizacionID            
  ,  EG.CabezasRecibidas            
 FROM @ENTRADAS EG 

           
 SELECT DISTINCT C.CorralID            
  ,  C.Codigo            
  ,  C.TipoCorralID            
  ,  L.LoteID            
  ,  L.Lote            
  ,  L.Cabezas            
  ,  L.CabezasInicio            
  ,  L.FechaInicio            
  ,  L.TipoProcesoID            
  ,  GC.GrupoCorralID            
,  GC.Descripcion AS GrupoCorral            
 FROM Corral C            
 INNER JOIN Lote L            
  ON (C.CorralID = L.CorralID)            
 INNER JOIN @ANIMALESMOVIMIENTOSFINAL CA            
  ON (L.CorralID = CA.CorralID            
   AND L.LoteID = CA.LoteID)            
 INNER JOIN TipoCorral TC      ON (C.TipoCorralID = TC.TipoCorralID)            
 INNER JOIN GrupoCorral GC            
  ON (TC.GrupoCorralID = GC.GrupoCorralID)            


 SELECT TM.TipoMovimientoID            
  ,  TM.EsEntrada            
  ,  TM.EsSalida            
  ,  TM.ClaveCodigo            
 FROM TipoMovimiento TM            


 SELECT Id            
  ,  ISA.SalidaID            
  ,  ISA.Arete            
  ,  ISA.CorralID            
 FROM @INTERFACESALIDAANIMAL ISA            


 SELECT A.AnimalID            
  ,  Arete = ISNULL(A.Arete,'')            
  ,  FolioEntrada = ISNULL(A.FolioEntrada,0 )              
 FROM @ANIMALES A      
 
 SET NOCOUNT OFF            
END
GO
