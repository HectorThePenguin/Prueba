USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoMovimiento_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoMovimiento_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[TipoMovimiento_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoMovimiento_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[TipoMovimiento_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		tm.TipoMovimientoID,
		tm.Descripcion,
		tm.EsGanado,
		tm.EsProducto,
		tm.EsEntrada,
		tm.EsSalida,
		tm.ClaveCodigo,
		tm.TipoPolizaID,
		tp.Descripcion as [TipoPoliza],
		tm.Activo
	FROM TipoMovimiento tm 
	INNER JOIN TipoPoliza tp on tp.TipoPolizaID = tm.TipoPolizaID
	WHERE tm.Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
