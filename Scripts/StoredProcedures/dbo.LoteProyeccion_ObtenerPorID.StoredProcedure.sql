USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteProyeccion_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteProyeccion_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[LoteProyeccion_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 15/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : LoteProyeccion_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[LoteProyeccion_ObtenerPorID]
@LoteProyeccionID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		LoteProyeccionID,
		LoteID,
		OrganizacionID,
		Frame,
		GananciaDiaria,
		ConsumoBaseHumeda,
		Conversion,
		PesoMaduro,
		PesoSacrificio,
		DiasEngorda,
		FechaEntradaZilmax
	FROM LoteProyeccion
	WHERE LoteProyeccionID = @LoteProyeccionID
	SET NOCOUNT OFF;
END

GO
