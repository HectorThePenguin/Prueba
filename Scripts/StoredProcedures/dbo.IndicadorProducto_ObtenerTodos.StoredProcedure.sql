USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProducto_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorProducto_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProducto_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 02/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorProducto_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[IndicadorProducto_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		IndicadorProductoID,
		IndicadorID,
		ProductoID,
		Activo
	FROM IndicadorProducto
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
