USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Usuario_ObtenerNivelAlertaPorUsuarioID]    Script Date: 15/03/2016 3:22:00 p.m. ******/
DROP PROCEDURE [dbo].[Usuario_ObtenerNivelAlertaPorUsuarioID]
GO
/****** Object:  StoredProcedure [dbo].[Usuario_ObtenerNivelAlertaPorUsuarioID]    Script Date: 15/03/2016 3:22:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Eric García
-- Create date: 15/03/2016 3:22:00 p.m.
-- Description: Obtiene todas las incidencias activas por la organización del usuario
-- SpName     : EXEC Usuario_ObtenerNivelAlertaPorUsuarioID 5
--======================================================
CREATE PROCEDURE [dbo].[Usuario_ObtenerNivelAlertaPorUsuarioID]
@UsuarioID INT
AS
BEGIN
	SET NOCOUNT ON;
		SELECT U.UsuarioID, U.UsuarioActiveDirectory, U.Corporativo, U.OrganizacionID, Ro.RolID, Ro.Descripcion, Ro.NivelAlertaID
		FROM Usuario U
		INNER JOIN Operador AS Op ON Op.UsuarioID = U.UsuarioID
		INNER JOIN Rol AS Ro ON Op.RolID = Ro.RolID
		WHERE U.Activo = 1
		AND U.UsuarioID = @UsuarioID;
	SET NOCOUNT OFF;
END

