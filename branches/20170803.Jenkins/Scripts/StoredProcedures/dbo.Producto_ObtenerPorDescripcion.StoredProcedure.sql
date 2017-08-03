USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 14/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Producto_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorDescripcion]
@Descripcion varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ProductoID,
		Descripcion,
		SubFamiliaID,
		UnidadID,
		Activo
	FROM Producto
	WHERE Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
