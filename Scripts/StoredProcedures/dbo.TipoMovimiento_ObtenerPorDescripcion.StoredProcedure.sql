USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoMovimiento_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoMovimiento_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[TipoMovimiento_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoMovimiento_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[TipoMovimiento_ObtenerPorDescripcion]
@Descripcion varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TipoMovimientoID,
		TM.Descripcion,
		EsGanado,
		EsProducto,
		EsEntrada,
		EsSalida,
		ClaveCodigo,
		TM.TipoPolizaID,
		TP.Descripcion TipoPoliza,
		TM.Activo
	FROM TipoMovimiento TM
	INNER JOIN TipoPoliza TP 
		ON TP.TipoPolizaID = TM.TipoPolizaID
	WHERE TM.Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
