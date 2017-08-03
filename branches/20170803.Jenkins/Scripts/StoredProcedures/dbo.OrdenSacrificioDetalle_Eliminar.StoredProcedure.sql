IF EXISTS(SELECT * FROM sys.objects WHERE [object_id] = Object_id(N'[dbo].[OrdenSacrificioDetalle_Eliminar]'))
	DROP PROCEDURE [dbo].[OrdenSacrificioDetalle_Eliminar]
GO
CREATE PROCEDURE [dbo].[OrdenSacrificioDetalle_Eliminar]  
(  
 @OrganizacionId INT,            
 @OrdenSacrificioId INT,  
 @Xml XML,  
 @AplicaMarel INT,  
 @UsuarioId INT
)            
AS            
BEGIN  
SET NOCOUNT ON  
	BEGIN TRY  
		BEGIN TRAN  
		SELECT    
			LoteID = T.item.value('./LoteID[1]', 'INT')  
		INTO #Lotes   
		FROM  @Xml.nodes('ROOT/Lotes') AS T(item)  

		IF @AplicaMarel =  1  
		BEGIN  
			SELECT   
				A.AnimalID  
			INTO #Animales1  
			FROM OrdenSacrificio OS (NOLOCK)   
			INNER JOIN OrdenSacrificioDetalle OSD (NOLOCK)  
				ON OS.OrdenSacrificioID = OSD.OrdenSacrificioID  
			INNER JOIN AnimalMovimiento AM (NOLOCK)  
				ON AM.LoteId = OSD.LoteID AND AM.OrganizacionId = OS.OrganizacionID AND AM.Activo = 1 AND AM.TipoMovimientoID NOT IN(8,11,16)  
			INNER JOIN Animal A (NOLOCK)  
				ON A.AnimalID = AM.AnimalID AND A.Activo = 0   
			WHERE OS.OrdenSacrificioID = @OrdenSacrificioID AND OS.OrganizacionID = @OrganizacionID

			INSERT INTO BitacoraOrdenSacrificio(OrdenSacrificioID,FechaOrden,OrganizacionID,CorralID,LoteID,AnimalID,Arete,AnimalMovimientoID,TipoMovimientoID,FechaRegistro,UsuarioRegistro, AplicaRollBack, EsRollBack)  
			SELECT   
				OS.OrdenSacrificioID, OS.FechaOrden, OS.OrganizacionID, AM.CorralID, AM.LoteId, A.AnimalID, A.Arete, AM.AnimalMovimientoID, AM.TipoMovimientoID, GETDATE(), @UsuarioId, 0, 1
			FROM OrdenSacrificio OS (NOLOCK)   
			INNER JOIN OrdenSacrificioDetalle OSD (NOLOCK)  
				ON OS.OrdenSacrificioID = OSD.OrdenSacrificioID  
			INNER JOIN AnimalMovimiento AM (NOLOCK)  
				ON AM.LoteId = OSD.LoteID AND AM.OrganizacionId = OS.OrganizacionID AND AM.Activo = 1 AND AM.TipoMovimientoID NOT IN(8,11,16)  
			INNER JOIN Animal A (NOLOCK)  
				ON A.AnimalID = AM.AnimalID AND A.Activo = 0   
			WHERE OS.OrdenSacrificioID = @OrdenSacrificioID AND OS.OrganizacionID = @OrganizacionID

  
			UPDATE A  
			SET Activo = 1, FechaModificacion = GETDATE(), UsuarioModificacionID = @UsuarioId  
			FROM Animal A (NOLOCK)  
			INNER JOIN #Animales1 AOS  
				ON AOS.AnimalID = A.AnimalID  
		END

		DELETE FROM OrdenSacrificioDetalle WHERE OrdenSacrificioId = @OrdenSacrificioId AND LoteId IN(SELECT LoteID FROM #Lotes )  

		IF NOT EXISTS(SELECT 1 FROM OrdenSacrificioDetalle WHERE OrdenSacrificioId = @OrdenSacrificioId)  
		BEGIN  
			DELETE FROM OrdenSacrificio WHERE OrdenSacrificioId = @OrdenSacrificioId AND OrganizacionID = @OrganizacionId  
		END

		UPDATE Lote  
		SET Activo = 1, FechaModificacion = GETDATE(), UsuarioModificacionID = @UsuarioId 
		FROM Lote L  
		INNER JOIN #Lotes TMP 
			ON TMP.LoteID=L.LoteID
  
		COMMIT TRAN  
		SELECT 1 As Resultado  
	END TRY  
	BEGIN CATCH  
		ROLLBACK TRAN  
	END CATCH  
SET NOCOUNT OFF  
END