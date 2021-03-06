USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Operador_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Operador_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Operador_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Ra�l Antonio Esquer Verduzco
-- Create date: 2013/11/13
-- Description: Obtiene un operador por ID 
-- Operador_ObtenerPorID 2
--=============================================
CREATE PROCEDURE [dbo].[Operador_ObtenerPorID]
@OperadorID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		o.OperadorID,
		o.Nombre,
		o.ApellidoPaterno,
		o.ApellidoMaterno,
		o.CodigoSAP,
		o.RolID,
		o.UsuarioID,
		u.Nombre as [Usuario],
		o.OrganizacionID,
		o.Activo
		, Org.Descripcion AS Organizacion
		, R.Descripcion AS Rol
	FROM Operador o
	INNER JOIN Organizacion Org
		ON (O.OrganizacionID = Org.OrganizacionID)
	INNER JOIN Rol R
		ON (o.RolID = r.RolID)
	LEFT JOIN Usuario u on u.UsuarioID = o.UsuarioID
	WHERE OperadorID = @OperadorID
	SET NOCOUNT OFF;
END

GO
