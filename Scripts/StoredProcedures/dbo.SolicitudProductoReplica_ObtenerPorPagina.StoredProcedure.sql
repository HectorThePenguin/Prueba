USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProductoReplica_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SolicitudProductoReplica_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProductoReplica_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================    
-- Author     : Audomar Pi�a Aguilar  
-- Create date: 22/04/2015 12:00:00 a.m.    
-- Description:     
-- SpName     : SolicitudProductoReplica_ObtenerPorPagina 1,0,1   
--======================================================    
CREATE PROCEDURE [dbo].[SolicitudProductoReplica_ObtenerPorPagina]      
@OrganizacionID INT,    
@FolioSolicitud BIGINT,      
@Activo BIT    
AS      
BEGIN      
	SET NOCOUNT ON;      
	SELECT 
		CONVERT(BIGINT, s.NumeroDocumento) AS FolioSolicitud,
		a.Descripcion AS AlmacenDestinoDescripcion,
		MAX(AlmacenMovimientoID) AS AlmacenMovimientoID,
		o.OrganizacionID,
		a.AlmacenID AS AlmacenDestinoID
	INTO #SolicitudProducto      
	FROM InterfaceTraspasoSAP s       
	INNER JOIN Almacen o(NOLOCK)   
		ON s.Centro = o.CodigoAlmacen  
	INNER JOIN Almacen a     
		ON CONVERT(INT, s.DestinatarioMercancia) = a.AlmacenID    
	WHERE @OrganizacionID in (Convert(INT,o.OrganizacionID), 0)      
	AND @FolioSolicitud in (Convert(BIGINT,s.NumeroDocumento),0)  
	AND CAST(SUBSTRING(s.NumeroDocumento,3,8) AS INT) NOT IN (SELECT TransferenciaId FROM [Sukarne].[dbo].[CacRecepcionProductoEnc] WHERE OrganizacionOrigenId = o.OrganizacionID)    
	GROUP BY CONVERT(BIGINT, s.NumeroDocumento), a.Descripcion ,o.OrganizacionID, a.AlmacenID   
	SELECT FolioSolicitud, AlmacenDestinoDescripcion, OrganizacionID, AlmacenDestinoID
	FROM #SolicitudProducto s    
	WHERE AlmacenMovimientoID IS NULL    
	SELECT COUNT(FolioSolicitud) AS TotalReg        
	FROM #SolicitudProducto    
	WHERE AlmacenMovimientoID IS NULL    
	DROP TABLE #SolicitudProducto       
	SET NOCOUNT OFF;      
END

GO
