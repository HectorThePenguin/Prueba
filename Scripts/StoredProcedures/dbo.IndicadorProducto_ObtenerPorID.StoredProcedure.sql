USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProducto_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorProducto_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProducto_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 02/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorProducto_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[IndicadorProducto_ObtenerPorID]
@IndicadorProductoID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		IndicadorProductoID,
		IndicadorID,
		ProductoID,
		Activo
	FROM IndicadorProducto
	WHERE IndicadorProductoID = @IndicadorProductoID
	SET NOCOUNT OFF;
END

GO
