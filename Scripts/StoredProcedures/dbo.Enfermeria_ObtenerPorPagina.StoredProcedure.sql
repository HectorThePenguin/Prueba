USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Enfermeria_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--==========================================================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 29/04/2014 12:00:00 a.m.
-- Description: Obtiene todos las enfermerias por Organizacion
-- SpName     : EXEC Enfermeria_ObtenerPorPagina 0,0,null,1, 1,15
--==========================================================================
CREATE PROCEDURE [dbo].[Enfermeria_ObtenerPorPagina] @EnfermeriaID INT
	,@OrganizacionID INT
	,@Descripcion VARCHAR(50)
	,@Activo INT
	,@Inicio INT
	,@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	IF (@Descripcion IS NULL)
	BEGIN
		SET @Descripcion = ''
	END
	SET @Descripcion = '%' + rtrim(@Descripcion) + '%'
	DECLARE @Datos AS TABLE (
		EnfermeriaID INT
		,OrganizacionID INT
		,Descripcion VARCHAR(50)
		,Activo BIT
		,Indice INT
		)
	INSERT INTO @Datos
	SELECT EnfermeriaID
		,OrganizacionID
		,Descripcion
		,Activo
		,ROW_NUMBER() OVER (
			ORDER BY Descripcion
			) AS [Indice]
	FROM Enfermeria
	WHERE @OrganizacionID IN (
			OrganizacionID
			,0
			)
		AND Descripcion LIKE @Descripcion
		AND Activo = @Activo
	SELECT e.EnfermeriaID
		,e.Descripcion
		,e.OrganizacionID
		,o.Descripcion AS [Organizacion]
		,e.Activo
		,Indice
	FROM @Datos e
	INNER JOIN Organizacion o ON o.OrganizacionID = e.OrganizacionID
	WHERE Indice BETWEEN @Inicio
			AND @Limite
	SELECT COUNT('') AS [TotalReg]
	FROM @Datos
	SET NOCOUNT OFF;
END

GO
