USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoParametro_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoParametro_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[TipoParametro_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: obtiene un tipo de parametro por descripcion
-- SpName     : TipoParametro_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[TipoParametro_ObtenerPorDescripcion]
@Descripcion varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TipoParametroID,
		Descripcion,
		Activo
	FROM TipoParametro
	WHERE Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
