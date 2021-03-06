USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CacRecepcionProducto_Insertar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CacRecepcionProducto_Insertar]
GO
/****** Object:  StoredProcedure [dbo].[CacRecepcionProducto_Insertar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================    
-- Author  : Sergio Gamez  
-- Create date : 08/08/2015  
-- Description : Insertar confirmación de productos solicitados del almacén SAP  
-- SpName  : CacRecepcionProducto_Insertar '4900063815','',1,1  
-- Notas  : TipoArete: Sukarne/RFID = 1, Nacional/Siniga = 2  
--======================================================   
CREATE PROCEDURE [dbo].[CacRecepcionProducto_Insertar]    
	@NumeroDocumento VARCHAR(10),    
	@Aretes Xml,      
	@OrganizacionId INT,      
	@UsuarioId INT    
AS        
BEGIN        
SET NOCOUNT ON  

	SELECT        
		NumeroArete = t.item.value('./NumeroArete[1]', 'BIGINT'),    
		TipoArete = t.item.value('./TipoArete[1]', 'INT')        
	INTO #Aretes      
	FROM @Aretes.nodes('ROOT/DATOS') AS T(item)  

	/* INSERTAMOS EL CABECERO DE LA RECEPCIÓN DE PRODUCTOS */  
	INSERT INTO [Sukarne].[dbo].[CacRecepcionProductoEnc]    
	(    
		OrganizacionId,    
		TransferenciaId,    
		OrganizacionOrigenId,    
		TipoDestinoId,    
		OrganizacionDestinoId,    
		EstatusTransferencia,    
		FechaTransferencia,    
		FechaCreacion,    
		UsuarioCreacionID,    
		FechaModificacion,    
		UsuarioModificacionID,    
		Comentarios,    
		PolizaID    
	)  

	SELECT     
		DISTINCT AD.OrganizacionID,     
		SUBSTRING(ITS.NumeroDocumento,3,8),    
		AO.OrganizacionID,     
		1 AS TipoDestinoID,     
		AD.OrganizacionID,     
		0 AS EstatusTransferencia,     
		ITS.FechaMovimiento,     
		GETDATE(),     
		@UsuarioId,     
		GETDATE(),     
		@UsuarioId,     
		'',     
		NULL    
	FROM SIAP.dbo.InterfaceTraspasoSAP ITS    
	INNER JOIN SIAP.dbo.Almacen AD         
		ON AD.AlmacenID = ITS.DestinatarioMercancia    
	INNER JOIN SIAP.dbo.Almacen AO         
		ON AO.CodigoAlmacen = ITS.Centro    
	WHERE ITS.NumeroDocumento = @NumeroDocumento    

	/*¨INSERTAMOS EL DETALLE DE LA RECEPCIÓN DE PRODUCTOS */  
	INSERT INTO [Sukarne].[dbo].[CacRecepcionProductoDet](    
		OrganizacionId,    
		TransferenciaId,    
		OrganizacionOrigenId,    
		DetalleId,    
		ProductoId,    
		AreteId,    
		Cantidad,    
		FechaCreacion,    
		UsuarioCreacionID,    
		FechaModificacion,    
		UsuarioModificacionID,    
		Importe    
	)    

	SELECT     
		AD.OrganizacionID,     
		SUBSTRING(ITS.NumeroDocumento,3,8),    
		AO.OrganizacionID,     
		ROW_NUMBER() OVER (ORDER BY NumeroDocumento) AS DetalleID,     
		P.ProductoID,     
		NULL,     
		ITS.Cantidad,     
		GETDATE(),     
		@UsuarioId,    
		GETDATE(),     
		@UsuarioId,     
		ITS.PrecioUnitario * ITS.Cantidad    
	FROM SIAP.dbo.InterfaceTraspasoSAP ITS    
	INNER JOIN SIAP.dbo.Almacen AD    
		ON AD.AlmacenID = ITS.DestinatarioMercancia    
	INNER JOIN SIAP.dbo.Almacen AO        
		ON AO.CodigoAlmacen = ITS.Centro    
	INNER JOIN SIAP.dbo.Producto P    
		ON P.MaterialSAP = ITS.Material    
	WHERE ITS.NumeroDocumento = @NumeroDocumento  

	/* INSERTAMOS LOS ARETES SUKARNE*/  
	-- Obtenemos los aretes sukarne que no existen   
	SELECT DISTINCT A.NumeroArete     
	INTO #AretesSukarne    
	FROM #Aretes A     
	LEFT OUTER JOIN [Sukarne].[dbo].[CatAreteSukarne] CAS (NOLOCK)  
		ON CAS.NumeroAreteSukarne = A.NumeroArete AND CAS.OrganizacionId = @OrganizacionId  
	WHERE A.TipoArete = 1 AND CAS.NumeroAreteSukarne IS NULL  

	-- Marcamos como replicados todos los aretes ya ingresados en procesos anteriores.  
	UPDATE [Sukarne].[dbo].CatAreteSukarne SET Replicado = 1 WHERE OrganizacionID = @OrganizacionId      

	-- Por motivos de recolección de aretes reactivamos los aretes sukarne que estaba inactivos  
	UPDATE CAS   
	SET Activo = 1, FechaModificacion = GETDATE()  
	FROM [Sukarne].[dbo].[CatAreteSukarne] CAS  
	INNER JOIN #Aretes A  
		ON A.NumeroArete = CAS.NumeroAreteSukarne AND CAS.OrganizacionId = @OrganizacionId AND A.TipoArete = 1  

	-- Insertamos los aretes sukarne que no existen  
	INSERT INTO [Sukarne].[dbo].[CatAreteSukarne](NumeroAreteSukarne,OrganizacionId,Activo,FechaCreacion,UsuarioCreacionID,Replicado,FechaModificacion,UsuarioModificacionID)      
	SELECT NumeroArete, @OrganizacionId, 1, GETDATE(), @UsuarioId, 0, GETDATE(), @UsuarioId 
	FROM #AretesSukarne      

	/* INSERTAMOS LOS ARETES SINIGA*/  
	-- Obtenemos los aretes siniga que no existen   
	SELECT DISTINCT A.NumeroArete     
	INTO #AretesNacional    
	FROM #Aretes A     
	LEFT OUTER JOIN [Sukarne].[dbo].[CatAreteNacional] CAS (NOLOCK)  
		ON CAS.NumeroAreteNacional = A.NumeroArete AND CAS.OrganizacionId = @OrganizacionId  
	WHERE A.TipoArete = 2 AND CAS.NumeroAreteNacional IS NULL  

	-- Por motivos de recolección de aretes reactivamos los aretes siniga que estaba inactivos    
	UPDATE CAS   
	SET Activo = 1, FechaModificacion = GETDATE()  
	FROM [Sukarne].[dbo].[CatAreteNacional] CAS  
	INNER JOIN #Aretes A  
		ON A.NumeroArete = CAS.NumeroAreteNacional AND CAS.OrganizacionId = @OrganizacionId AND A.TipoArete = 2  

	-- Insertamos los aretes siniga que no existen  
	INSERT INTO [Sukarne].[dbo].[CatAreteNacional](NumeroAreteNacional,OrganizacionId,Activo,FechaCreacion,UsuarioCreacionID,FechaModificacion,UsuarioModificacionID)      
	SELECT NumeroArete, @OrganizacionId, 1, GETDATE(), @UsuarioId, GETDATE(), @UsuarioId 
	FROM #AretesNacional      

	/* INACTIVAMOS EL FOLIO */  
	--UPDATE SIAP.dbo.InterfaceTraspasoSAP SET AlmacenMovimientoID = 0 WHERE NumeroDocumento = @NumeroDocumento    

	SELECT 1 AS Resultado    

SET NOCOUNT OFF          
END