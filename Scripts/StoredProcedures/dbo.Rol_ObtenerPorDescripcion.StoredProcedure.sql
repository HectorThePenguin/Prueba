USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Rol_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Rol_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Rol_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Rol_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[Rol_ObtenerPorDescripcion]
@Descripcion varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		RolID,
		Descripcion,
		Activo
	FROM Rol
	WHERE Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
