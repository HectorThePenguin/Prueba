USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoMovimiento_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoMovimiento_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[TipoMovimiento_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoMovimiento_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[TipoMovimiento_ObtenerPorID]
@TipoMovimientoID int
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
		TM.Activo,
		TP.Descripcion AS TipoPoliza
	FROM TipoMovimiento TM
	INNER JOIN TipoPoliza TP
		ON (TM.TipoPolizaID = TP.TipoPolizaID)
	WHERE TM.TipoMovimientoID = @TipoMovimientoID
	SET NOCOUNT OFF;
END

GO
