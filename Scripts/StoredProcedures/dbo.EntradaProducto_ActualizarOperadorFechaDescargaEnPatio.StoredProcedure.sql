USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ActualizarOperadorFechaDescargaEnPatio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ActualizarOperadorFechaDescargaEnPatio]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ActualizarOperadorFechaDescargaEnPatio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 05/12/2014
-- Description: Actualiza la entrada de producto cuando se llega con el producto antes de descargarlo.
-- SpName     : exec EntradaProducto_ActualizarOperadorFechaDescargaEnPatio 1, 1
--======================================================
CREATE PROCEDURE [dbo].[EntradaProducto_ActualizarOperadorFechaDescargaEnPatio]
@EntradaProductoId INT,
@OperadorIdAlmacen INT 
AS 
BEGIN
	UPDATE EntradaProducto SET
	OperadorIDAlmacen = @OperadorIdAlmacen, FechaInicioDescarga = GETDATE() WHERE EntradaProductoID = @EntradaProductoId
END

GO
