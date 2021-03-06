USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Vigilancia_ChoferObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Vigilancia_ChoferObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Vigilancia_ChoferObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Eduardo Cota
-- Create date: 21/Mayo/2014
-- Description:	Obtener listado de chofer.
-- EXEC Vigilancia_ChoferObtenerPorPagina '', 0, 1, 1, 10, 375
-- =============================================
CREATE PROCEDURE [dbo].[Vigilancia_ChoferObtenerPorPagina]		
	@Nombre NVARCHAR(50),
	@ChoferID INT,
	@Activo BIT,
	@Inicio INT, 
	@Limite INT,
	@ProveedorID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
	    ROW_NUMBER() OVER ( ORDER BY c.Nombre + ' '+ c.ApellidoPaterno + ' '+ c.ApellidoMaterno ASC) AS RowNum,
		P.ProveedorChoferID,
		C.ChoferID,
		C.Nombre, 
		C.ApellidoPaterno, 
		C.ApellidoMaterno,
		C.Activo		
		INTO #Chofer
		FROM ProveedorChofer P	
		INNER JOIN Chofer C ON P.ChoferID = C.ChoferID
		WHERE (c.Nombre + ' '+ c.ApellidoPaterno + ' '+ c.ApellidoMaterno LIKE '%'+@Nombre+'%' OR @Nombre = '')
		  AND (@ChoferID = 0 OR C.ChoferID = @ChoferID)
		  AND c.Activo = @Activo
		  AND P.ProveedorID = @ProveedorID
		  AND P.Activo = 1
	SELECT 
		ChoferId, 
		Nombre,		
		ApellidoPaterno,
		ApellidoMaterno,
		Activo
	FROM #Chofer	
	WHERE RowNum BETWEEN @Inicio AND @Limite
	Select 
		COUNT(ChoferId)AS TotalReg 
	From #Chofer
	DROP TABLE #Chofer	
	SET NOCOUNT ON;	   
END

GO
