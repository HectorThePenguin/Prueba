USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ActualizarMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ActualizarMovimiento]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ActualizarMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 05/06/2014
-- Description: Actualiza el movimiento de almacen en entrada producto
-- SpName     : exec EntradaProducto_ActualizarMovimiento 1, 4
--======================================================
CREATE PROCEDURE [dbo].[EntradaProducto_ActualizarMovimiento]
@EntradaProductoID INT,
@AlmacenMovimientoID BIGINT,
@AlmacenMovimientoSalidaID BIGINT 
AS
BEGIN
	UPDATE EntradaProducto
	SET AlmacenMovimientoID = @AlmacenMovimientoID
		, AlmacenMovimientoSalidaID = @AlmacenMovimientoSalidaID
	WHERE EntradaProductoID = @EntradaProductoID
END
GO
