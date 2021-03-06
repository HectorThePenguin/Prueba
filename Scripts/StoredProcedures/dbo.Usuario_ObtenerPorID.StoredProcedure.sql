USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Usuario_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Usuario_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Usuario_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 2013/11/06
-- Description: Usuario_ObtenerPorID 3
--=============================================
CREATE PROCEDURE [dbo].[Usuario_ObtenerPorID]
	@UsuarioID INT
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT 
		U.UsuarioID,
		U.Nombre,
		U.OrganizacionID,
		U.UsuarioActiveDirectory	
		,u.Corporativo
		, O.Descripcion AS Organizacion
		, R.RolID
		, R.Descripcion AS Rol
	FROM Usuario U
	INNER JOIN Organizacion O
		ON (U.OrganizacionID = O.OrganizacionID)
	LEFT OUTER JOIN Operador Op
		ON (U.UsuarioID = OP.UsuarioID
			AND O.OrganizacionID = Op.OrganizacionID)
	LEFT JOIN Rol R
		ON (Op.RolID = R.RolID)
	WHERE U.UsuarioID = @UsuarioID
	
	SET NOCOUNT OFF;
END

GO
