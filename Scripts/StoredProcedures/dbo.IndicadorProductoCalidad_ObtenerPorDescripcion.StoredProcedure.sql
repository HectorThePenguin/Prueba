USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoCalidad_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorProductoCalidad_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoCalidad_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 03/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorProductoCalidad_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[IndicadorProductoCalidad_ObtenerPorDescripcion]
@IndicadorProductoCalidadID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		IndicadorProductoCalidadID,
		IndicadorID,
		ProductoID,
		Activo
	FROM IndicadorProductoCalidad
	WHERE IndicadorProductoCalidadID = @IndicadorProductoCalidadID
	SET NOCOUNT OFF;
END

GO
