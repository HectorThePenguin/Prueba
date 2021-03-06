USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ChoferPorProveedor_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ChoferPorProveedor_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[ChoferPorProveedor_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Cesar Fernando Vega Vazquez
-- Create date: 18/10/2013
-- Description:	Obtener listado de chofer.
-- ChoferPorProveedor_ObtenerPorPagina 0, 1, 1, 10 
-- =============================================
CREATE PROCEDURE [dbo].[ChoferPorProveedor_ObtenerPorPagina]    
	@ProveedorID INT,  
	@Activo BIT,  
	@Inicio INT,   
	@Limite INT  
AS  
BEGIN  
	SET NOCOUNT ON;  
	SELECT   
		ROW_NUMBER() OVER ( ORDER BY c.Nombre + ' '+ c.ApellidoPaterno + ' '+ c.ApellidoMaterno ASC) AS RowNum,
		c.ChoferID,
		c.Nombre,
		c.ApellidoPaterno,
		c.ApellidoMaterno,
		c.Activo,
		pc.ProveedorID
	INTO 
		#Chofer  
	FROM 
		Chofer c
		INNER JOIN ProveedorChofer pc on c.ChoferID = pc.ChoferID
	WHERE 
		(@ProveedorID = 0 OR pc.ProveedorID = @ProveedorID)  
		AND pc.Activo = @Activo  
	SELECT   
		ChoferId,   
		Nombre,    
		ApellidoPaterno,  
		ApellidoMaterno,  
		Activo,
		ProveedorID
	FROM 
		#Chofer   
	WHERE 
		RowNum BETWEEN @Inicio AND @Limite  
	Select   
		COUNT(ChoferId)AS TotalReg   
	From 
		#Chofer  
	DROP TABLE 
		#Chofer   
	SET NOCOUNT OFF;      
END  

GO
