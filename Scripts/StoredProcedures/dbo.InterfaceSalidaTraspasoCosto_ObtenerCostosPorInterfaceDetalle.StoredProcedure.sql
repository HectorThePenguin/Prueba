USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspasoCosto_ObtenerCostosPorInterfaceDetalle]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalidaTraspasoCosto_ObtenerCostosPorInterfaceDetalle]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspasoCosto_ObtenerCostosPorInterfaceDetalle]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gilberto Carranza
-- Create date: 05-12-2014
-- Description:	Actualiza las cabezas en lote
-- InterfaceSalidaTraspasoCosto_CrearXML
-- =============================================
CREATE PROCEDURE [dbo].[InterfaceSalidaTraspasoCosto_ObtenerCostosPorInterfaceDetalle]
@InterfaceSalidaTraspasoDetalleXML	XML
AS 
BEGIN	

	SET NOCOUNT ON
	DECLARE @esCancelacion BIT 
	CREATE TABLE #Animales(AnimalID INT, Serie VARCHAR(5), Folio VARCHAR(10), Facturado BIT)


	SELECT T.N.value('./InterfaceSalidaTraspasoDetalleID[1]','INT') AS InterfaceSalidaTraspasoDetalleID
		  ,T.N.value('./Cabezas[1]','INT') AS Cabezas    
		  ,T.N.value('./Serie[1]','VARCHAR(5)') AS Serie  
		  ,T.N.value('./Folio[1]','INT') AS Folio   
	  INTO #TMP
	  FROM @InterfaceSalidaTraspasoDetalleXML.nodes('/ROOT/InterfaceSalidaTraspasoCosto') as T(N)  
  
	
	SELECT @esCancelacion = 1 
	  FROM InterfaceSalidaTraspasoCosto ISTC (NOLOCK)      
	 INNER JOIN #TMP x       
	    ON ISTC.InterfaceSalidaTraspasoDetalleID = x.InterfaceSalidaTraspasoDetalleID
	   AND ISTC.Serie = x.Serie
	   AND ISTC.Folio = x.Folio

	IF ISNULL(@esCancelacion,0) = 0
		BEGIN
			DECLARE @CabezasSacrificadas INT = (SELECT SUM(Cabezas) FROM #TMP)
  
			INSERT INTO #Animales
			SELECT TOP (@CabezasSacrificadas) ISTC.AnimalID, x.Serie, x.Folio, 0 AS Facturado
			  FROM InterfaceSalidaTraspasoCosto ISTC (NOLOCK)
			 INNER JOIN #TMP x 
			    ON (ISTC.InterfaceSalidaTraspasoDetalleID = x.InterfaceSalidaTraspasoDetalleID)
			 WHERE ISTC.Facturado = 0  
			 GROUP BY ISTC.AnimalID, x.Serie, x.Folio         
			 ORDER BY ISTC.AnimalID        

		END
	ELSE		
		BEGIN
			    
			INSERT INTO #Animales
			SELECT ISTC.AnimalID, NULL Serie, NULL Folio, 1 AS Facturado
			  FROM InterfaceSalidaTraspasoCosto ISTC (NOLOCK)      
			 INNER JOIN #TMP x       
			    ON (ISTC.InterfaceSalidaTraspasoDetalleID = x.InterfaceSalidaTraspasoDetalleID)      
			 WHERE ISTC.Facturado = 1        
			   AND ISTC.Serie = x.Serie
			   AND ISTC.Folio = x.Folio
			 GROUP BY ISTC.AnimalID  
			 ORDER BY ISTC.AnimalID  

		END


	UPDATE ISTC  
	   SET Facturado = A.Facturado,
		   Serie = RTRIM(LTRIM(A.Serie)),  
		   Folio = CONVERT(VARCHAR(10),A.Folio)  
	  FROM InterfaceSalidaTraspasoCosto ISTC (NOLOCK)      
	 INNER JOIN #Animales A ON ISTC.AnimalID = A.AnimalID      


		SELECT ISTC.InterfaceSalidaTraspasoCostoID
			,  ISTC.InterfaceSalidaTraspasoDetalleID
			,  ISTC.AnimalID
			,  ISTC.CostoID
			,  ISTC.Importe
			,  ISTC.Facturado
	  FROM InterfaceSalidaTraspasoCosto ISTC (NOLOCK)
	 INNER JOIN #Animales x 
	    ON (ISTC.AnimalID = x.AnimalID)
  
  	DROP TABLE #TMP
	DROP TABLE #Animales

	SET NOCOUNT OFF
END

GO
