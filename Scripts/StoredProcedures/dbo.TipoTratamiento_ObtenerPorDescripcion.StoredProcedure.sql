USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoTratamiento_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoTratamiento_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[TipoTratamiento_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 14/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoTratamiento_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[TipoTratamiento_ObtenerPorDescripcion]
@Descripcion varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TipoTratamientoID,
		Descripcion,
		Activo
	FROM TipoTratamiento
	WHERE Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
