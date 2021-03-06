USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Pais_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Pais_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Pais_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  Audomar Piña Aguilar
-- Create date: 08-04-2015  
-- Description: Otiene un listado de pais paginado  
-- Pais_ObtenerPorPagina  '',0, 1, 1, 15   
-- =============================================  
CREATE PROCEDURE [dbo].[Pais_ObtenerPorPagina]  
@Descripcion NVARCHAR(100),   
@Activo BIT,  
@Inicio INT,   
@Limite INT  
AS  
BEGIN  
	SET NOCOUNT ON;  
	SELECT ROW_NUMBER() OVER ( ORDER BY b.Descripcion ASC) AS RowNum,  
		   b.PaisID,  
		   b.Descripcion, 
		   b.DescripcionCorta,
		   b.Activo    
      INTO #Pais
	  FROM Pais b  
	 WHERE (b.Descripcion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')  
       AND b.Activo = @Activo  
       

	SELECT b.PaisID,   
		   b.Descripcion,  
		   b.DescripcionCorta,
		   b.Activo  
      FROM #Pais b   
	 WHERE RowNum BETWEEN @Inicio AND @Limite  

	SELECT COUNT(PaisID)AS TotalReg   
	  FROM #Pais   
	
	SET NOCOUNT OFF;  
END  
GO
