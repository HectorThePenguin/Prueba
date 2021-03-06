USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Usuario_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Usuario_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Usuario_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--=============================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 2013/11/06
-- Description: 
-- Usuario_ObtenerPorPagina '', 4, 1, 1, 10 
-- 001 Jorge Luis Velazquez Araujo 07/12/2015 **Se agrega, para que regrese el Nivel de Acceso
--=============================================
CREATE PROCEDURE [dbo].[Usuario_ObtenerPorPagina]	
	@Nombre VARCHAR(50),
	@OrganizacionID INT,
	@Activo BIT,
	@Inicio INT,
	@Limite INT
AS

BEGIN
	SET NOCOUNT ON;

	SELECT
		ROW_NUMBER() OVER (ORDER BY U.Nombre ASC) AS RowNum,
		U.UsuarioID,
		U.Nombre,
		U.OrganizacionID, 
		U.UsuarioActiveDirectory
		,u.Corporativo
		, O.Descripcion AS Organizacion
		, U.Activo
		,u.NivelAcceso
	INTO #Datos
	FROM Usuario U
	INNER JOIN Organizacion O
		ON (U.OrganizacionID = O.OrganizacionID)
	WHERE (Nombre like '%' + @Nombre + '%' OR @Nombre = '')
	  AND @OrganizacionID IN (U.OrganizacionID, 0) 
	  AND U.Activo = @Activo

	SELECT
		UsuarioID,
		Nombre,
		OrganizacionID, 
		UsuarioActiveDirectory,
		Corporativo,
		Organizacion,
		Activo,
		NivelAcceso
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite

	SELECT 
		COUNT(UsuarioID)AS TotalReg
	From #Datos

	DROP TABLE #Datos

END

GO
