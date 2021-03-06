USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Operador_ObtenerPorUsuarioID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Operador_ObtenerPorUsuarioID]
GO
/****** Object:  StoredProcedure [dbo].[Operador_ObtenerPorUsuarioID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================s
-- Author     : Ramses Abdiel Santos Beltran
-- Create date: 2014/02/13
-- Description: Obtiene el operador en base al usuarioID
-- Operador_ObtenerPorUsuarioID 2629, 1
--=============================================
CREATE PROCEDURE [dbo].[Operador_ObtenerPorUsuarioID]
	@UsuarioID INT
   ,@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT op.OperadorID, op.OrganizacionID, op.Nombre, op.ApellidoPaterno, op.ApellidoMaterno, op.CodigoSAP, op.RolID, op.Activo , rl.Activo EstatusRol, us.Activo EstatusUsuario
	FROM Operador op
	INNER JOIN usuario us ON us.UsuarioID = op.UsuarioId
	INNER JOIN Rol rl on op.RolID = rl.RolID
	WHERE op.UsuarioID = @UsuarioID AND us.OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END