USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoCalidad_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorProductoCalidad_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoCalidad_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 03/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorProductoCalidad_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[IndicadorProductoCalidad_ObtenerPorID]
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
