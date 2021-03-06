USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenUsuario_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenUsuario_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenUsuario_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 11/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : AlmacenUsuario_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[AlmacenUsuario_ObtenerPorPagina]
@AlmacenUsuarioID int,
@AlmacenID INT,
@OrganizacionID INT,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY AlmacenUsuarioID ASC) AS [RowNum],
		AlmacenUsuarioID,
		AlmacenID,
		UsuarioID,
		Activo
	INTO #AlmacenUsuario
	FROM AlmacenUsuario 
	WHERE @AlmacenID in (AlmacenID, 0)
	AND Activo = @Activo
	SELECT
		au.AlmacenUsuarioID,
		a.AlmacenID,
		a.Descripcion AS Almacen,
		a.CodigoAlmacen,
		o.OrganizacionID,
		o.Descripcion AS Organizacion,
		us.UsuarioID,
		us.Nombre AS Usuario,
		au.Activo
	FROM #AlmacenUsuario au
	INNER JOIN Almacen a on au.AlmacenID = a.AlmacenID	
	INNER JOIN Organizacion o on a.OrganizacionID = o.OrganizacionID
	INNER JOIN Usuario us on au.UsuarioID = us.UsuarioID
	WHERE RowNum BETWEEN @Inicio AND @Limite
	and @OrganizacionID in (a.OrganizacionID, 0)
	SELECT
	COUNT(AlmacenUsuarioID) AS [TotalReg]
	FROM #AlmacenUsuario
	DROP TABLE #AlmacenUsuario
	SET NOCOUNT OFF;
END

GO
