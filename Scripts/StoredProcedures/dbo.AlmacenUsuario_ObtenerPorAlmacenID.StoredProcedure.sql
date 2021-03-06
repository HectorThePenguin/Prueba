USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenUsuario_ObtenerPorAlmacenID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenUsuario_ObtenerPorAlmacenID]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenUsuario_ObtenerPorAlmacenID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 11/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : AlmacenUsuario_ObtenerPorAlmacenID
--======================================================
CREATE PROCEDURE [dbo].[AlmacenUsuario_ObtenerPorAlmacenID]
@AlmacenID int
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
		au.Activo
	FROM AlmacenUsuario au
	inner join Almacen a on au.AlmacenID = a.AlmacenID
	inner join Usuario us on au.UsuarioID = us.UsuarioID
	WHERE a.AlmacenID = @AlmacenID
	SET NOCOUNT OFF;
END

GO
