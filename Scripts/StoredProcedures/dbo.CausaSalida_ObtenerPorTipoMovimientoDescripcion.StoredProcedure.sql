USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CausaSalida_ObtenerPorTipoMovimientoDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CausaSalida_ObtenerPorTipoMovimientoDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[CausaSalida_ObtenerPorTipoMovimientoDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CausaSalida_ObtenerPorTipoMovimientoDescripcion
--======================================================
CREATE PROCEDURE [dbo].[CausaSalida_ObtenerPorTipoMovimientoDescripcion]
@Descripcion varchar(50)
,@TipoMovimientoID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		CausaSalidaID,
		cs.Descripcion,
		tm.TipoMovimientoID,
		tm.Descripcion AS TipoMovimiento,
		cs.Activo
	FROM CausaSalida cs
	INNER JOIN TipoMovimiento tm on cs.TipoMovimientoID = tm.TipoMovimientoID
	WHERE cs.Descripcion = @Descripcion
	AND tm.TipoMovimientoID = @TipoMovimientoID
	SET NOCOUNT OFF;
END

GO
