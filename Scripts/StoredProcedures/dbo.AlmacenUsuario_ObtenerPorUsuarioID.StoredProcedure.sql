USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenUsuario_ObtenerPorUsuarioID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenUsuario_ObtenerPorUsuarioID]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenUsuario_ObtenerPorUsuarioID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 11/11/2014
-- Description: 
-- SpName     : AlmacenUsuario_ObtenerPorUsuarioID 1
--======================================================
CREATE PROCEDURE [dbo].[AlmacenUsuario_ObtenerPorUsuarioID]
@UsuarioID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		au.AlmacenUsuarioID,
		a.AlmacenID,
		a.CodigoAlmacen,
		a.Descripcion AS Almacen,  
		us.UsuarioID,
		us.Nombre AS Usuario,
		au.Activo, a.TipoAlmacenID
	FROM AlmacenUsuario au
	inner join Almacen a on au.AlmacenID = a.AlmacenID
	inner join Usuario us on au.UsuarioID = us.UsuarioID
	WHERE au.UsuarioID = @UsuarioID
	SET NOCOUNT OFF;
END

GO
