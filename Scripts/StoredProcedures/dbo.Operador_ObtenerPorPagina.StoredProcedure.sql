USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Operador_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Operador_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Operador_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Ra�l Antonio Esquer Verduzco
-- Create date: 2013/11/13
-- Description: Obtiene una lista de operadores paginada
-- Operador_ObtenerPorPagina '',0,0, 1, 1,15
--=============================================
CREATE PROCEDURE [dbo].[Operador_ObtenerPorPagina]
@Nombre VARCHAR(50),
@RolID INT, 
@OrganizacionID INT,
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Nombre ASC) AS RowNum,
		OperadorID,
		Nombre,
		ApellidoPaterno,
		ApellidoMaterno,
		CodigoSAP,
		RolID,
		UsuarioID,
		Activo,
		OrganizacionID
	INTO #Datos
	FROM Operador
	WHERE (Nombre + ' ' + ApellidoPaterno + ' ' + ApellidoMaterno like '%' + @Nombre + '%' OR @Nombre = '')
	AND @RolID IN (RolID, 0)
	AND @OrganizacionID IN (OrganizacionID, 0)
	AND Activo = @Activo
	SELECT
		o.OperadorID,
		o.Nombre,
		o.ApellidoPaterno,
		o.ApellidoMaterno,
		o.CodigoSAP,
		o.RolID,
		o.UsuarioID,
		u.Nombre as [Usuario],
		r.Descripcion as [Rol],
		o.Activo,
		org.OrganizacionID,
		org.Descripcion
	FROM #Datos o
	Inner join Rol r on r.RolID = o.RolID
	INNER JOIN Organizacion org
		ON (o.OrganizacionID = org.OrganizacionID)
	LEFT JOIN Usuario u on u.UsuarioID = o.UsuarioID
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT 
		COUNT(OperadorID)AS TotalReg
	From #Datos
	DROP TABLE #Datos
END

GO
