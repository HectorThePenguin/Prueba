USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorGanadera_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProveedorGanadera_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorGanadera_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:    Ruben Guzman meza  
-- Create date: 25/05/2015    
-- Description:  Obtener listado de Proveedores de la BD Sukarne
-- ProveedorGanadera_ObtenerPorID 1,1    
-- =============================================    
CREATE PROCEDURE [dbo].[ProveedorGanadera_ObtenerPorID]   
	@ProveedorID INT,
    @OrganizacionID INT
AS    
BEGIN    
	SET NOCOUNT ON;
	SELECT   
		P.ProveedorID,    
		P.CodigoSAP,    
		P.Descripcion,    
		P.TipoProveedorID,    
		p.ImporteComision,    
		P.Activo,   
		TP.Descripcion AS TipoProveedor,
		P.OrganizacionId    
	FROM [Sukarne].[dbo].[CatProveedor] P    
	INNER JOIN TipoProveedor TP    
		ON (P.TipoProveedorID = TP.TipoProveedorID)    
	WHERE P.ProveedorID = @ProveedorID  
	AND OrganizacionId = @OrganizacionID
	AND P.Activo = 1
	SET NOCOUNT OFF;    
END
GO