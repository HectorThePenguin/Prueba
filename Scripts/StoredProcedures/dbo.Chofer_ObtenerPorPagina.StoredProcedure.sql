USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Chofer_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Chofer_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Chofer_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jos� Gilberto Quintero L�pez
-- Create date: 18/10/2013
-- Description:	Obtener listado de chofer.
-- Chofer_ObtenerPorPagina '', 1, 1, 10 
-- =============================================
CREATE PROCEDURE [dbo].[Chofer_ObtenerPorPagina]		
	@Nombre NVARCHAR(50),
	@ChoferID INT,
	@Activo BIT,
	@Inicio INT, 
	@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
	    ROW_NUMBER() OVER ( ORDER BY c.Nombre + ' '+ c.ApellidoPaterno + ' '+ c.ApellidoMaterno ASC) AS RowNum,
		ChoferID,
		c.Nombre,
		c.ApellidoPaterno,
		c.ApellidoMaterno,
		c.Activo,
		c.Boletinado,
		c.Observaciones
		INTO #Chofer
		FROM Chofer c			
		WHERE (c.Nombre + ' '+ c.ApellidoPaterno + ' '+ c.ApellidoMaterno LIKE '%'+@Nombre+'%' OR @Nombre = '')
		  AND (@ChoferID = 0 OR ChoferID = @ChoferID)
		  AND c.Activo = @Activo
	SELECT 
		ChoferId, 
		Nombre,		
		ApellidoPaterno,
		ApellidoMaterno,
		Activo,
		Boletinado,
		Observaciones
	FROM #Chofer	
	WHERE RowNum BETWEEN @Inicio AND @Limite
	Select 
		COUNT(ChoferId)AS TotalReg 
	From #Chofer
	DROP TABLE #Chofer	
	SET NOCOUNT ON;	   
END

GO
