USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Banco_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Banco_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Banco_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  Audomar Piña Aguilar
-- Create date: 08-04-2015  
-- Description: Otiene un listado de bancos paginado  
-- Banco_ObtenerPorPagina  '','',0, 1, 1, 15   
-- =============================================  
CREATE PROCEDURE [dbo].[Banco_ObtenerPorPagina]  
@Descripcion NVARCHAR(70),   
@Telefono VARCHAR(10),
@PaisID INT,  
@Activo BIT,  
@Inicio INT,   
@Limite INT  
AS  
BEGIN  
	SET NOCOUNT ON;  
	SELECT ROW_NUMBER() OVER ( ORDER BY b.Descripcion ASC) AS RowNum,  
		   b.BancoID,  
		   b.Descripcion,  
		   b.PaisID,
		   p.Descripcion AS Pais,
		   b.Telefono,  
		   b.Activo    
      INTO #Banco
	  FROM Banco b  
	 INNER JOIN Pais p 
	    ON b.PaisID = p.PaisID
	 WHERE (b.descripcion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')  
       AND (b.Telefono LIKE '%'+@Telefono+'%' OR @Telefono = '')  
       AND @PaisID IN (b.PaisID, 0)  
       AND b.Activo = @Activo  
       

	SELECT b.BancoID,   
		   b.Descripcion,  
		   b.Telefono,   
		   b.PaisID,
		   b.Pais,
		   b.Activo  
      FROM #Banco b   
	 WHERE RowNum BETWEEN @Inicio AND @Limite  

	SELECT COUNT(BancoID)AS TotalReg   
	  FROM #Banco   
	
	SET NOCOUNT OFF;  
END  
GO
