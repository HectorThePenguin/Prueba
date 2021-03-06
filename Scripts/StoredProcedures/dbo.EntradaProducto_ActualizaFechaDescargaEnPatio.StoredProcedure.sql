USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ActualizaFechaDescargaEnPatio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ActualizaFechaDescargaEnPatio]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ActualizaFechaDescargaEnPatio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 04/06/2014
-- Description: Actualiza la fecha de inicio o fin de descarga
-- SpName     : exec EntradaProducto_ActualizaFechaDescargaEnPatio 1, 10
--======================================================
CREATE PROCEDURE [dbo].[EntradaProducto_ActualizaFechaDescargaEnPatio]
@EntradaProductoId INT,
@Piezas INT
AS 
BEGIN
	DECLARE @FechaInicioDescarga DATE
	DECLARE @FechaActualizada VARCHAR(11)
	SELECT @FechaActualizada = ''
	SELECT @FechaInicioDescarga = FechaInicioDescarga FROM EntradaProducto WHERE EntradaProductoID = @EntradaProductoId
	IF @FechaInicioDescarga IS NULL 
		BEGIN
			UPDATE EntradaProducto SET FechaInicioDescarga = GETDATE(), Piezas = @Piezas WHERE EntradaProductoID = @EntradaProductoId
			SELECT @FechaActualizada = 'FechaInicio'
		END
	ELSE
		BEGIN
			UPDATE EntradaProducto SET FechaFinDescarga = GETDATE(), Piezas = @Piezas WHERE EntradaProductoID = @EntradaProductoId
			SELECT @FechaActualizada = 'FechaFin'
		END
		SELECT @FechaActualizada AS FechaActualizada
END

GO
