USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ActualizarDescuentoEntradaProductoMuestra]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ActualizarDescuentoEntradaProductoMuestra]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ActualizarDescuentoEntradaProductoMuestra]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Edgar Villarreal
-- Create date: 19/12/2014
-- Description:	Actualiza descuento de  de entrada producto muestra
--EXEC EntradaProducto_ActualizarDescuentoEntradaProductoMuestra 1400
--======================================================
CREATE PROCEDURE [dbo].[EntradaProducto_ActualizarDescuentoEntradaProductoMuestra]
@EntradaProductoID INT,
@Activo INT,
@descuento DECIMAL(10,2) 
AS 
BEGIN
	
	DECLARE @EntradaProductoDetalleID INT;

	SET @EntradaProductoDetalleID = (SELECT TOP 1 EntradaProductoDetalleID 
												FROM EntradaProductoDetalle 
												WHERE EntradaProductoID = @EntradaProductoID
															AND Activo = @Activo)
	

	UPDATE EntradaProductoMuestra
	SET Descuento = @descuento
	WHERE EntradaProductoDetalleID = @EntradaProductoDetalleID
				AND Activo = @Activo;

END

GO
