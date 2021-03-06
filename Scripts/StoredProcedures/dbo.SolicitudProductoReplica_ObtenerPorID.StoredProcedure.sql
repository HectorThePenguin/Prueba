USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProductoReplica_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SolicitudProductoReplica_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProductoReplica_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================          
-- Author     : Audomar Pi�a Aguilar        
-- Create date: 22/04/2015 12:00:00 a.m.          
-- Description:           
-- SpName     : [SolicitudProductoReplica_ObtenerPorID] 1,4900184140,1      
--======================================================          
CREATE PROCEDURE [dbo].[SolicitudProductoReplica_ObtenerPorID]            
@OrganizacionID INT,        
@FolioSolicitud BIGINT,          
@Activo BIT        
AS          
BEGIN          
	SET NOCOUNT ON;          
	SELECT ROW_NUMBER() OVER (ORDER BY s.InterfaceTraspasoSAPID ASC) AS [RowNum]          
		   ,a.OrganizacionID          
		   ,s.InterfaceTraspasoSAPID        
		   ,CONVERT(BIGINT, s.NumeroDocumento) AS FolioSolicitud        
		   ,DestinatarioMercancia AS AlmacenDestinoID    
		   --,'Almacen Norte' AS AlmacenDestinoDescripcion        
		   ,s.CuentaSAP        
		   ,CASE WHEN AlmacenMovimientoID IS NULL THEN 1 ELSE 0 END Activo          
		   ,p.ProductoID        
		   ,s.Cantidad        
		   ,s.PrecioUnitario  
		   ,s.Activo AS ActivoDetalle   
		   ,s.FechaCreacion      
		   ,ISNULL(AlmacenMovimientoID,0) AS AlmacenMovimientoID
		   ,s.Material AS Material        
	INTO #SolicitudProducto          
	FROM InterfaceTraspasoSAP s  
	INNER JOIN Almacen a ON a.CodigoAlmacen = s.Centro         
	INNER JOIN Producto p ON s.Material = p.MaterialSAP        
	WHERE @OrganizacionID in (Convert(INT,a.OrganizacionID), 0)          
	AND @FolioSolicitud in (Convert(BIGINT,s.NumeroDocumento),0)
	AND CAST(SUBSTRING(s.NumeroDocumento,3,8) AS INT) NOT IN (SELECT TransferenciaId FROM [Sukarne].[dbo].[CacRecepcionProductoEnc] WHERE OrganizacionOrigenId = a.OrganizacionID)
	SELECT *            
	FROM #SolicitudProducto s          
	DROP TABLE #SolicitudProducto           
	SET NOCOUNT OFF;          
END

GO
