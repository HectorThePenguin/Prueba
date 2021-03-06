USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Cliente_ObtenerClientesPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Cliente_ObtenerClientesPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Cliente_ObtenerClientesPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ramses Santos
-- Create date: 04-03-2014
-- Description:	Obtiene un listado de Clientes 
-- exec Cliente_ObtenerClientesPorPagina 'Ram', 4, 6
-- =============================================
CREATE PROCEDURE [dbo].[Cliente_ObtenerClientesPorPagina]
	@Descripcion VARCHAR(150),
	@Inicio INT, 
	@Limite INT 
AS
BEGIN	
	SET NOCOUNT ON
	SELECT ROW_NUMBER() OVER ( ORDER BY ClienteID ASC) AS RowNum, ClienteID, CodigoSAP, Descripcion, Activo 
	INTO #ClienteTemp 
	FROM  Cliente WHERE Descripcion LIKE '%' + @Descripcion + '%'
	SELECT RowNum, ClienteID, CodigoSAP, Descripcion, Activo FROM #ClienteTemp WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT 
		COUNT(DISTINCT ClienteID)AS TotalReg 
	FROM #ClienteTemp
	DROP TABLE #ClienteTemp	
END

GO
