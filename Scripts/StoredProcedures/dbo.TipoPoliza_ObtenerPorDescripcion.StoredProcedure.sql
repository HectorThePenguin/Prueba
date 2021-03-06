USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoPoliza_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoPoliza_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[TipoPoliza_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoPoliza_ObtenerPorDescripcion ''
--======================================================
CREATE PROCEDURE [dbo].[TipoPoliza_ObtenerPorDescripcion]
@Descripcion varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TipoPolizaID,
		Descripcion,
		ClavePoliza,
		Activo
	FROM TipoPoliza
	WHERE Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
