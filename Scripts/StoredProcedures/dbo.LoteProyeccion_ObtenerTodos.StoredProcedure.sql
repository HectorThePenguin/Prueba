USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteProyeccion_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteProyeccion_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[LoteProyeccion_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 15/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : LoteProyeccion_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[LoteProyeccion_ObtenerTodos]
@Activo BIT = NULL
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
	SET NOCOUNT OFF;
END

GO
