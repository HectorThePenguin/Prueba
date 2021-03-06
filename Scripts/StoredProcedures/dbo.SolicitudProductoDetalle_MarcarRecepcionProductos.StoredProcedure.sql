USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProductoDetalle_MarcarRecepcionProductos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SolicitudProductoDetalle_MarcarRecepcionProductos]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProductoDetalle_MarcarRecepcionProductos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:  Audomar Piña Aguilar    
-- Create date: 28/04/2015    
-- Description: Actualizacion a productos como recibidos    
-- =============================================    
CREATE PROCEDURE [dbo].[SolicitudProductoDetalle_MarcarRecepcionProductos]    
@SolicitudProductoDetalleXML XML      
AS    
BEGIN    
	SET NOCOUNT ON;    
	CREATE TABLE #SolicitudProductoDetalle (      
		InterfaceTraspasoSAPID INT    
		,FolioSolicitud BIGINT    
		,AlmacenMovimientoID BIGINT      
		,ProductoID INT      
		,Cantidad DECIMAL(18,2)      
		,Activo BIT      
		,UsuarioCreacionID INT      
	)      
	INSERT #SolicitudProductoDetalle    
	(    
		InterfaceTraspasoSAPID     
		,FolioSolicitud     
		,AlmacenMovimientoID    
		,ProductoID      
		,Cantidad     
		,Activo     
		,UsuarioCreacionID     
	)    
	SELECT InterfaceTraspasoSAPID = t.item.value('./InterfaceTraspasoSAPID[1]', 'INT')      
		   ,FolioSolicitud = t.item.value('./FolioSolicitud[1]', 'BIGINT')      
		   ,AlmacenMovimientoID = t.item.value('./AlmacenMovimientoID[1]', 'INT')      
		   ,ProductoID = t.item.value('./ProductoID[1]', 'INT')      
		   ,Cantidad = t.item.value('./Cantidad[1]', 'decimal(18,2)')      
		   ,Activo = t.item.value('./Activo[1]', 'BIT')      
		   ,UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'INT')    
	  FROM @SolicitudProductoDetalleXML.nodes('ROOT/SolicitudProductoDetalle') AS T(item)      

	UPDATE A    
	   SET Activo = B.Activo,    
		   AlmacenMovimientoID = B.AlmacenMovimientoID,    
		   UsuarioModificacionID = B.UsuarioCreacionID,    
		   FechaModificacion = GETDATE()    
	  FROM InterfaceTraspasoSAP A    
	 INNER JOIN #SolicitudProductoDetalle B     
	    ON A.InterfaceTraspasoSAPID = B.InterfaceTraspasoSAPID    

	UPDATE A  
	   SET Activo = 0    
	  FROM InterfaceTraspasoSAP A    
	 INNER JOIN #SolicitudProductoDetalle B     
	    ON CONVERT(BIGINT,A.NumeroDocumento) = B.FolioSolicitud  
	 WHERE A.AlmacenMovimientoID IS NULL  

	SET NOCOUNT OFF;     
END 

GO
