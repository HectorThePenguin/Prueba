USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ExportSalidaSacrificio_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ExportSalidaSacrificio_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[ExportSalidaSacrificio_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================    
-- Author     : Lic. Sergio Alberto Gamez Gomez   
-- Create date: 03/09/2015
-- Description: Obtener los animales que se han ordenado sacrificar para enviarlos a Marel    
-- SpName     : ExportSalidaSacrificio_ObtenerPorID 602, 2, 1, 1    
--====================================================== 
CREATE PROCEDURE [dbo].[ExportSalidaSacrificio_ObtenerPorID](          
@OrdenSacrificioID INT,  
@OrganizacionID INT,  
@AplicaRollBack BIT,
@UsuarioID INT  
)          
AS          
BEGIN          
SET NOCOUNT ON  
	IF @AplicaRollBack = 1  
	BEGIN
		-- CANCELACION DE ORDEN DE SACRIFICIO  
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
			OS.OrdenSacrificioID, OS.FechaOrden, OS.OrganizacionID, AM.CorralID, AM.LoteId, A.AnimalID, A.Arete, AM.AnimalMovimientoID, AM.TipoMovimientoID, GETDATE(), @UsuarioID, 1, 0
		FROM OrdenSacrificio OS (NOLOCK)   
		INNER JOIN OrdenSacrificioDetalle OSD (NOLOCK)  
			ON OS.OrdenSacrificioID = OSD.OrdenSacrificioID  
		INNER JOIN AnimalMovimiento AM (NOLOCK)  
			ON AM.LoteId = OSD.LoteID AND AM.OrganizacionId = OS.OrganizacionID AND AM.Activo = 1 AND AM.TipoMovimientoID NOT IN(8,11,16)    
		INNER JOIN Animal A (NOLOCK)  
			ON A.AnimalID = AM.AnimalID AND A.Activo = 0   
		WHERE OS.OrdenSacrificioID = @OrdenSacrificioID AND OS.OrganizacionID = @OrganizacionID

		UPDATE A  
		SET Activo = 1, FechaModificacion = GETDATE(), UsuarioModificacionID = @UsuarioID
		FROM Animal A (NOLOCK)  
		INNER JOIN #Animales1 AOS  
			ON AOS.AnimalID = A.AnimalID  
	END  
	ELSE  
	BEGIN
		-- ORDEN DE SACRIFICIO    
		SELECT   
			A.AnimalID  
		INTO #Animales2  
		FROM OrdenSacrificio OS (NOLOCK)   
		INNER JOIN OrdenSacrificioDetalle OSD (NOLOCK)  
			ON OS.OrdenSacrificioID = OSD.OrdenSacrificioID  
		INNER JOIN AnimalMovimiento AM (NOLOCK)  
			ON AM.LoteId = OSD.LoteID AND AM.OrganizacionId = OS.OrganizacionID AND AM.Activo = 1 AND AM.TipoMovimientoID NOT IN(8,11,16)     
		INNER JOIN Animal A (NOLOCK)  
			ON A.AnimalID = AM.AnimalID AND A.Activo = 1  
		WHERE OS.OrdenSacrificioID = @OrdenSacrificioID AND OS.OrganizacionID = @OrganizacionID

		INSERT INTO BitacoraOrdenSacrificio(OrdenSacrificioID,FechaOrden,OrganizacionID,CorralID,LoteID,AnimalID,Arete,AnimalMovimientoID,TipoMovimientoID,FechaRegistro,UsuarioRegistro, AplicaRollBack, EsRollBack)  
		SELECT   
			OS.OrdenSacrificioID, OS.FechaOrden, OS.OrganizacionID, AM.CorralID, AM.LoteId, A.AnimalID, A.Arete, AM.AnimalMovimientoID, AM.TipoMovimientoID, GETDATE(), @UsuarioID, 0, 0
		FROM OrdenSacrificio OS (NOLOCK)   
		INNER JOIN OrdenSacrificioDetalle OSD (NOLOCK)  
			ON OS.OrdenSacrificioID = OSD.OrdenSacrificioID  
		INNER JOIN AnimalMovimiento AM (NOLOCK)  
			ON AM.LoteId = OSD.LoteID AND AM.OrganizacionId = OS.OrganizacionID AND AM.Activo = 1 AND AM.TipoMovimientoID NOT IN(8,11,16)       
		INNER JOIN Animal A (NOLOCK)  
			ON A.AnimalID = AM.AnimalID AND A.Activo = 1   
		WHERE OS.OrdenSacrificioID = @OrdenSacrificioID AND OS.OrganizacionID = @OrganizacionID

		UPDATE A  
		SET Activo = 0, FechaModificacion = GETDATE(), UsuarioModificacionID = @UsuarioID  
		FROM Animal A (NOLOCK)  
		INNER JOIN #Animales2 AOS  
			ON AOS.AnimalID = A.AnimalID
  
		SELECT   
			OSD.LoteID,   
			OSD.CabezasLote,   
			OSD.FolioSalida,  
			A.AnimalID,   
			A.Arete   
		FROM OrdenSacrificio OS (NOLOCK)   
		INNER JOIN OrdenSacrificioDetalle OSD (NOLOCK)  
			ON OS.OrdenSacrificioID = OSD.OrdenSacrificioID  
		INNER JOIN AnimalMovimiento AM (NOLOCK)  
			ON AM.LoteId = OSD.LoteID AND AM.OrganizacionId = OS.OrganizacionID AND AM.Activo = 1 AND AM.TipoMovimientoID NOT IN(8,11,16)       
		INNER JOIN Animal A (NOLOCK)  
			ON A.AnimalID = AM.AnimalID
		INNER JOIN #Animales2 AOS  
			ON AOS.AnimalID = A.AnimalID
		WHERE OS.OrdenSacrificioID = @OrdenSacrificioID AND OS.OrganizacionID = @OrganizacionID  
	END  
SET NOCOUNT OFF    
END