USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CausaSalida_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CausaSalida_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[CausaSalida_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CausaSalida_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[CausaSalida_ObtenerPorDescripcion]
@Descripcion varchar(50)
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
	SET NOCOUNT OFF;
END

GO
