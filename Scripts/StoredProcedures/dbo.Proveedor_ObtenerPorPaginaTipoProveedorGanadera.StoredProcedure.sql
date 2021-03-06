USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorPaginaTipoProveedorGanadera]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_ObtenerPorPaginaTipoProveedorGanadera]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorPaginaTipoProveedorGanadera]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================        
-- Author:  Ruben guzman meza      
-- Create date: 2015-05-27        
-- Description: Otiene un listado de Proveedores paginado  de  tipo proveedor ganadera       
-- Proveedor_ObtenerPorPaginaTipoProveedorGanadera 0,'','',1,1,30,19
-- =============================================        
CREATE PROCEDURE [dbo].[Proveedor_ObtenerPorPaginaTipoProveedorGanadera]        
	@ProveedorID INT,        
	@CodigoSAP VARCHAR(10),        
	@Descripcion NVARCHAR(50),         
	@Activo BIT,        
	@Inicio INT,         
	@Limite INT,        
	@OrganizacionId INT
AS        
BEGIN        
	SET NOCOUNT ON;
	SELECT         
		ROW_NUMBER() OVER ( ORDER BY p.Descripcion ASC) AS RowNum,        
		p.ProveedorID,        
		p.Descripcion,        
		p.CodigoSAP,        
		p.TipoProveedorID,        
		p.ImporteComision,
		tp.Descripcion as  [TipoProveedor],        
		p.Activo,
		P.OrganizacionId          
	INTO #Proveedor        
	FROM [Sukarne].[dbo].[CatProveedor] p
	INNER JOIN TipoProveedor tp 
		ON tp.TipoProveedorID = p.TipoProveedorID         
	WHERE (p.descripcion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')        
	AND p.Activo = @Activo        
	AND @ProveedorID IN (p.ProveedorID, 0)       
	AND OrganizacionId = @OrganizacionId
	SELECT
		p.ProveedorID,         
		p.Descripcion,        
		p.CodigoSAP,         
		p.TipoProveedorID,        
		p.ImporteComision,        
		p.TipoProveedor,        
		p.Activo,
		P.OrganizacionId        
	FROM #Proveedor p         
	WHERE RowNum BETWEEN @Inicio AND @Limite        
	SELECT COUNT(ProveedorID)AS TotalReg FROM #Proveedor         
	SET NOCOUNT OFF;        
END

GO
