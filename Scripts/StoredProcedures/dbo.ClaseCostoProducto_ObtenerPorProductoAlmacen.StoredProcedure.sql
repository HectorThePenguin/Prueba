USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ClaseCostoProducto_ObtenerPorProductoAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ClaseCostoProducto_ObtenerPorProductoAlmacen]
GO
/****** Object:  StoredProcedure [dbo].[ClaseCostoProducto_ObtenerPorProductoAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 11/08/2014
-- Description:  Obtiene los productos por almacen
-- ClaseCostoProducto_ObtenerPorProductoAlmacen 1, 100
-- =============================================
CREATE PROCEDURE [dbo].[ClaseCostoProducto_ObtenerPorProductoAlmacen]
@AlmacenID INT,
@ProductoID INT
AS
BEGIN
	SET NOCOUNT ON
		SELECT ClaseCostoProductoID
			,  AlmacenID
			,  ProductoID
			,  C.CuentaSAPID
			,  C.Descripcion		AS CuentaSAPDescripcion
			,  C.CuentaSAP			AS CuentaSAP
		FROM ClaseCostoProducto CP
		INNER JOIN CuentaSAP C
			ON (CP.CuentaSAPID = C.CuentaSAPID)
		WHERE AlmacenID = @AlmacenID
			AND ProductoID = @ProductoID
	SET NOCOUNT OFF
END

GO
