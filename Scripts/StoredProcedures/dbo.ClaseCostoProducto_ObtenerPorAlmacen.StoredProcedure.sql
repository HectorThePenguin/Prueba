USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ClaseCostoProducto_ObtenerPorAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ClaseCostoProducto_ObtenerPorAlmacen]
GO
/****** Object:  StoredProcedure [dbo].[ClaseCostoProducto_ObtenerPorAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 11/08/2014
-- Description:  Obtiene los productos por almacen
-- ClaseCostoProducto_ObtenerPorAlmacen 1
-- =============================================
CREATE PROCEDURE [dbo].[ClaseCostoProducto_ObtenerPorAlmacen]
@AlmacenID INT
AS
BEGIN
	SET NOCOUNT ON
		SELECT ClaseCostoProductoID
			,  AlmacenID
			,  ProductoID
			,  CuentaSAPID
		FROM ClaseCostoProducto
		WHERE AlmacenID = @AlmacenID
	SET NOCOUNT OFF
END

GO
