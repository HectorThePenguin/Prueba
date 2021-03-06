USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CausaSalida_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CausaSalida_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[CausaSalida_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CausaSalida_ObtenerPorID 1
--======================================================
CREATE PROCEDURE [dbo].[CausaSalida_ObtenerPorID] @CausaSalidaID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT cs.CausaSalidaID
		,cs.Descripcion
		,cs.TipoMovimientoID
		,tm.Descripcion AS [TipoMovimiento]
		,cs.Activo
	FROM CausaSalida cs
	INNER JOIN TipoMovimiento tm ON tm.TipoMovimientoID = cs.TipoMovimientoID
	WHERE CausaSalidaID = @CausaSalidaID
	SET NOCOUNT OFF;
END

GO
