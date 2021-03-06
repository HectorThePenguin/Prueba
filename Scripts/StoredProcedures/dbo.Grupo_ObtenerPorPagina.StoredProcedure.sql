USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Grupo_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Grupo_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Grupo_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Raul Esquer
-- Create date: 26/09/2013
-- Description:	Obtener listado de Grupo.
-- Grupo_ObtenerPorPagina '', 1, 1, 10 
-- =============================================
CREATE PROCEDURE [dbo].[Grupo_ObtenerPorPagina]		
	@Descripcion NVARCHAR(50),
	@Activo BIT,
	@Inicio INT, 
	@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
	    ROW_NUMBER() OVER ( ORDER BY G.descripcion ASC) AS RowNum,
		GrupoID,
		G.Descripcion,
		G.Activo		
		INTO #Grupo
		FROM Grupo G			
		WHERE (G.descripcion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')
		  AND G.Activo = @Activo
	SELECT 
		GrupoId, 
		Descripcion,		
		Activo
	FROM    #Grupo	
	WHERE   RowNum BETWEEN @Inicio AND @Limite
	Select 
		COUNT(grupoId)AS TotalReg 
	From #Grupo
	DROP TABLE #Grupo		   
END

GO
