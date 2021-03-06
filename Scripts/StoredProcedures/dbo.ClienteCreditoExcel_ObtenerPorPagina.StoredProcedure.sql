USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ClienteCreditoExcel_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ClienteCreditoExcel_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[ClienteCreditoExcel_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---====================================================      
--Author  :  Ruben Guzman Meza      
--Create date : 2015/25/05      
--Description:      
--ClienteCreditoExcel_ObtenerPorPagina '', 1, 15     
---====================================================      
CREATE  PROCEDURE [dbo].[ClienteCreditoExcel_ObtenerPorPagina]      
(      
@Nombre NVARCHAR(250),           
@Inicio INT,           
@Limite INT       
)      
AS      
BEGIN       
	SET NOCOUNT ON;
    
	SELECT        
		ROW_NUMBER() OVER ( ORDER BY C.Nombre ASC) AS RowNum, C.CreditoID, C.Nombre, C.Saldo      
	INTO #CreditoSOFOM      
	FROM  CreditoSOFOM AS C (NOLOCK)      
	WHERE (C.Nombre LIKE '%' + @Nombre + '%' OR @Nombre = '') AND Saldo > 0
	
	SELECT      
		c.CreditoID, Nombre = c.Nombre + '    [Saldo: $' + CAST(Saldo AS VARCHAR(20)) + ']'       
	FROM #CreditoSOFOM c      
	WHERE RowNum BETWEEN @Inicio AND @Limite
	       
	SELECT       
	COUNT(CreditoID) AS TotalReg      
	FROM #CreditoSOFOM
	      
	DROP TABLE #CreditoSOFOM      
	
	SET NOCOUNT OFF;      
END
GO