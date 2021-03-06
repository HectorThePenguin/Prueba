USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CancelacionOrdenSacrificioSIAP]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CancelacionOrdenSacrificioSIAP]
GO
/****** Object:  StoredProcedure [dbo].[CancelacionOrdenSacrificioSIAP]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
CancelacionOrdenSacrificioSIAP '
<OrdenSacrificio><DetalleOrden>
	<Corral>L11</Corral>
	<Lote>12779</Lote>
	<LoteID>51393</LoteID>
	<Fecha>2016-11-15</Fecha>
	<CabezasSacrificio>1</CabezasSacrificio>
	<FolioOrdenSacrificio>8841</FolioOrdenSacrificio>
	<OrdenSacrificio>852</OrdenSacrificio>
	</DetalleOrden>
</OrdenSacrificio>',
852, 5
*/
CREATE PROCEDURE [dbo].[CancelacionOrdenSacrificioSIAP]    
(          
@XmlDetalleOrden XML,  
@OrdenSacrificioID INT,  
@UsuarioID INT
)    
AS    
BEGIN    
SET DATEFORMAT YMD  

DECLARE @Organizacion INT
	--BEGIN TRY    
		--BEGIN TRAN    
		SELECT     
			Corral = T.Item.value('./Corral[1]','VARCHAR(3)')    
			,Lote = T.Item.value('./Lote[1]', 'VARCHAR(10)')    
			,LoteID = T.Item.value('./LoteID[1]', 'INT')    
			,Fecha = T.Item.value('./Fecha[1]', 'VARCHAR(10)')    
			,CabezasSacrificio = T.Item.value('./CabezasSacrificio[1]', 'INT')
			,EsIntensivo = 0
		INTO #DetalleOrden
		FROM @XmlDetalleOrden.nodes('OrdenSacrificio/DetalleOrden') AS T(Item)

		SELECT @Organizacion = OrganizacionID FROM OrdenSacrificio WHERE OrdenSacrificioId = @OrdenSacrificioID

		UPDATE DET 
		SET DET.EsIntensivo = 1
		FROM #DetalleOrden DET INNER JOIN LOTE L ON DET.Lote = L.Lote and DET.LoteID = L.LoteID WHERE L.TipoCorralID = 4
		
		-- INACTIVAR DETALLE DE LA ORDEN DE SACRIFICIO  
		UPDATE OSD  
		SET OSD.Activo = 0, OSD.FechaModificacion = GETDATE(), OSD.UsuarioModificacion = @UsuarioID  
		FROM OrdenSacrificioDetalle OSD (NOLOCK)  
		INNER JOIN #DetalleOrden DO  
		ON DO.LoteID = OSD.LoteID AND OSD.OrdenSacrificioID = @OrdenSacrificioID
  
		-- ACTIVAR LOTE  
		UPDATE L  
		SET L.Activo = 1, L.FechaModificacion = GETDATE(), L.UsuarioModificacionID = @UsuarioID
		FROM Lote L (NOLOCK)   
		INNER JOIN #DetalleOrden DO  
		ON DO.LoteID = L.LoteID

		-- ACTIVAR ANIMALES  
		SELECT  
			A.AnimalID  
		INTO #AnimalesOrdenSacrificio  
		FROM OrdenSacrificio OS (NOLOCK)   
		INNER JOIN OrdenSacrificioDetalle OSD (NOLOCK)  
			ON OS.OrdenSacrificioID = OSD.OrdenSacrificioID  
		INNER JOIN AnimalMovimiento AM (NOLOCK)  
			ON AM.LoteId = OSD.LoteID AND AM.OrganizacionId = OS.OrganizacionID AND AM.Activo = 1 AND AM.TipoMovimientoID NOT IN(8,11,16)   
		INNER JOIN Animal A (NOLOCK)  
			ON A.AnimalID = AM.AnimalID AND A.Activo = 0  
		WHERE OS.OrdenSacrificioID = @OrdenSacrificioID

		UPDATE A  
		SET A.Activo = 1, A.FechaModificacion = GETDATE(), A.UsuarioModificacionID = @UsuarioID
		FROM Animal A (NOLOCK)  
		INNER JOIN #AnimalesOrdenSacrificio AOS  
		ON AOS.AnimalID = A.AnimalID
		
		SELECT L.LoteID, L.Lote, OSD.EsIntensivo, Cabezas = COUNT(AM.AnimalID)   
		 INTO #Salida    
		 FROM #DetalleOrden OSD (NOLOCK)    
		 INNER JOIN  Lote L (NOLOCK)    
		 ON L.LoteId = OSD.LoteId AND L.OrganizacionId = @Organizacion
		 INNER JOIN AnimalMovimiento AM (NOLOCK)     
		 ON AM.OrganizacionID = @Organizacion AND AM.CorralID = L.CorralID AND AM.LoteID = L.LoteID AND TipoMovimientoID NOT IN(8,11,16) AND AM.Activo = 1  
		 INNER JOIN Animal A (NOLOCK)    
		 ON A.AnimalID = AM.AnimalID    
		 GROUP BY L.LoteID, L.Lote, OSD.EsIntensivo -- , OSD.CabezasSacrificar  
		 
		UPDATE L  
		SET L.Cabezas = SL.Cabezas, L.FechaModificacion = GETDATE(), L.UsuarioModificacionID = @UsuarioID
		FROM Lote L (NOLOCK)   
		INNER JOIN #Salida SL  
		ON SL.LoteID = L.LoteID
		WHERE SL.EsIntensivo != 1

		-- INACTIVAR ORDEN DE SACRIFICIO SI NO HAY DETALLES ACTIVOS  
		IF NOT EXISTS(SELECT TOP 1 OrdenSacrificioID FROM OrdenSacrificioDetalle (NOLOCK) WHERE Activo = 1 AND OrdenSacrificioID = @OrdenSacrificioID)  
		BEGIN  
			UPDATE OrdenSacrificio SET Activo = 0, FechaModificacion = GETDATE(), UsuarioModificacionID = @UsuarioID WHERE OrdenSacrificioID = @OrdenSacrificioID  
		END
  
		DROP TABLE #DetalleOrden    
		DROP TABLE #AnimalesOrdenSacrificio    
		--COMMIT TRAN    
		SELECT 1 As Resultado, '' As Error  
	--END TRY    
	--BEGIN CATCH    
	--	ROLLBACK TRAN    
	--	SELECT 0 As Resultado, ERROR_MESSAGE() As Error    
	--END CATCH
END