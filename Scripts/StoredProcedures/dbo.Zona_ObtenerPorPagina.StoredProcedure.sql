USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Zona_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Zona_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Zona_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  Audomar Piña Aguilar
-- Create date: 08-04-2015  
-- Description: Otiene un listado de Zonas paginado  
-- Zona_ObtenerPorPagina  '','',0, 1, 1, 15   
-- =============================================  
CREATE PROCEDURE [dbo].[Zona_ObtenerPorPagina]  
@Descripcion NVARCHAR(100),   
@PaisID INT,  
@Activo BIT,  
@Inicio INT,   
@Limite INT  
AS  
BEGIN  
	SET NOCOUNT ON;  
	SELECT ROW_NUMBER() OVER ( ORDER BY b.Descripcion ASC) AS RowNum,  
		   b.ZonaID,  
		   b.Descripcion,  
		   b.PaisID,
		   p.Descripcion AS Pais,
		   b.Activo    
      INTO #Zona
	  FROM Zona b  
	 INNER JOIN Pais p 
	    ON b.PaisID = p.PaisID
	 WHERE (b.descripcion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')  
       AND @PaisID IN (b.PaisID, 0)  
       AND b.Activo = @Activo  
       

	SELECT b.ZonaID,   
		   b.Descripcion,  
		   b.PaisID,
		   b.Pais,
		   b.Activo  
      FROM #Zona b   
	 WHERE RowNum BETWEEN @Inicio AND @Limite  

	SELECT COUNT(ZonaID)AS TotalReg   
	  FROM #Zona   
	
	SET NOCOUNT OFF;  
END  
GO
