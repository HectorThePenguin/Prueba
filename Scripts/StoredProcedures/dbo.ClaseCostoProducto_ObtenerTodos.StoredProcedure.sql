USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ClaseCostoProducto_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ClaseCostoProducto_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[ClaseCostoProducto_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 02/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ClaseCostoProducto_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[ClaseCostoProducto_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ClaseCostoProductoID,
		AlmacenID,
		ProductoID,
		CuentaSAPID,
		Activo
	FROM ClaseCostoProducto
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
