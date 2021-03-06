USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Operadores_ObtenerPorIDRol]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Operadores_ObtenerPorIDRol]
GO
/****** Object:  StoredProcedure [dbo].[Operadores_ObtenerPorIDRol]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Leonel ayala flores
-- Origen:    APInterfaces
-- Create date: 20/11/2013
-- Description:  Consulta de Operadores.
-- Empresa: Apinterfaces
-- EXEC Operadores_ObtenerPorIDRol 4,10
-- =============================================
CREATE PROCEDURE [dbo].[Operadores_ObtenerPorIDRol]
@OrganizacionID INT,
@IdRol INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		O.OperadorID,	
		O.nombre,
		O.ApellidoPaterno,
		O.ApellidoMaterno,
		O.RolID,
		O.UsuarioID,
		O.OrganizacionID,
		O.Activo
		, R.Descripcion AS Rol
		, Org.Descripcion AS Organizacion
		, U.Nombre
	FROM Operador AS O
	INNER JOIN Rol AS R ON O.RolID = R.RolID
	INNER JOIN Organizacion Org
		ON (O.OrganizacionID = Org.OrganizacionID)
	LEFT OUTER JOIN Usuario U
		ON (O.UsuarioID = U.UsuarioID)
	WHERE R.RolID = @IdRol
		AND O.OrganizacionID = @OrganizacionID
	ORDER BY O.nombre ASC
	SET NOCOUNT OFF;
END

GO
