USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ClaseCostoProducto_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ClaseCostoProducto_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[ClaseCostoProducto_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 02/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ClaseCostoProducto_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[ClaseCostoProducto_ObtenerPorID]
@ClaseCostoProductoID int
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
	WHERE ClaseCostoProductoID = @ClaseCostoProductoID
	SET NOCOUNT OFF;
END

GO
