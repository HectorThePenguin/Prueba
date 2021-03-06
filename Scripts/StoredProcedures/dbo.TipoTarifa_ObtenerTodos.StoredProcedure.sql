USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoTarifa_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoTarifa_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[TipoTarifa_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 15/08/2014 12:00:00 a.m.
-- Description: Obtiene una lista de tipo de tarifa
-- SpName     : TipoTarifa_ObtenerTodos 
--======================================================
CREATE PROCEDURE [dbo].[TipoTarifa_ObtenerTodos]
AS
BEGIN
	SELECT 
		TipoTarifaID,
		Descripcion,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID
	FROM TipoTarifa (NOLOCK)
END

GO
