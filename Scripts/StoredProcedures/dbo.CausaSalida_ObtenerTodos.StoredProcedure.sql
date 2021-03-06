USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CausaSalida_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CausaSalida_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[CausaSalida_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CausaSalida_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[CausaSalida_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		cs.CausaSalidaID,
		cs.Descripcion,
		tm.TipoMovimientoID,
		tm.Descripcion AS TipoMovimiento,
		cs.Activo
	FROM CausaSalida cs
	INNER JOIN TipoMovimiento tm ON cs.TipoMovimientoID = tm.TipoMovimientoID
	WHERE cs.Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
