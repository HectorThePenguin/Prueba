USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoProceso_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoProceso_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[TipoProceso_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 27/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoProceso_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[TipoProceso_ObtenerPorDescripcion]
@Descripcion varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TipoProcesoID,
		Descripcion,
		Activo
	FROM TipoProceso
	WHERE Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
