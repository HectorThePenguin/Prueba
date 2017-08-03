USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Accion_ObtenerPoDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Accion_ObtenerPoDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Accion_ObtenerPoDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramón Abel Atondo Echavarria
-- Create date: 15/03/2016
-- Description: SP par obtener los registros de Acciones solo descripcion.
-- SpName     : dbo.Accion_ObtenerPorDescripcion
-- --======================================================
CREATE PROCEDURE [dbo].[Accion_ObtenerPorDescripcion]
@Descripcion varchar(255)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		AccionID,
		Descripcion,
		Activo
	FROM Accion
	WHERE Descripcion = @Descripcion
	SET NOCOUNT OFF;
END
