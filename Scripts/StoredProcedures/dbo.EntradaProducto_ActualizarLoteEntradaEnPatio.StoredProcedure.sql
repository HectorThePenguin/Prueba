USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ActualizarLoteEntradaEnPatio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ActualizarLoteEntradaEnPatio]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ActualizarLoteEntradaEnPatio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 20/05/2014
-- Description: Actualiza la entrada de producto cuando se llega con el producto antes de descargarlo.
-- SpName     : exec EntradaProducto_ActualizarLoteEntradaEnPatio 1, 4
--======================================================
CREATE PROCEDURE [dbo].[EntradaProducto_ActualizarLoteEntradaEnPatio]
@EntradaProductoId INT,
@AlmacenInventaioLoteId INT
AS 
BEGIN
	UPDATE EntradaProducto SET AlmacenInventarioLoteID = @AlmacenInventaioLoteId
	WHERE EntradaProductoID = @EntradaProductoId
END

GO
