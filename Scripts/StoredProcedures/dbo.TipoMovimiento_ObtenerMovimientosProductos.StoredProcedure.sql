USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoMovimiento_ObtenerMovimientosProductos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoMovimiento_ObtenerMovimientosProductos]
GO
/****** Object:  StoredProcedure [dbo].[TipoMovimiento_ObtenerMovimientosProductos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Roque Solis
-- Create date: 20/06/2014
-- Description: Obtiene los tipo de movimiento para producto
-- Origen     : Apinterfaces
-- SpName     : EXEC TipoMovimiento_ObtenerMovimientosProductos 1
--======================================================
CREATE PROCEDURE [dbo].[TipoMovimiento_ObtenerMovimientosProductos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		TM.TipoMovimientoID,
		TM.Descripcion,
		TM.EsGanado,
		TM.EsProducto,
		TM.EsEntrada,
		TM.EsSalida,
		TM.ClaveCodigo,
		TM.TipoPolizaID,
		TP.Descripcion as [TipoPoliza],
		TM.Activo
	FROM TipoMovimiento TM 
	INNER JOIN TipoPoliza TP on TP.TipoPolizaID = TM.TipoPolizaID
	WHERE (TM.Activo = @Activo OR @Activo IS NULL)
	AND TM.EsProducto = 1
	SET NOCOUNT OFF;
END

GO
