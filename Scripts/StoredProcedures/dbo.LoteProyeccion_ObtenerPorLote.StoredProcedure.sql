USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteProyeccion_ObtenerPorLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteProyeccion_ObtenerPorLote]
GO
/****** Object:  StoredProcedure [dbo].[LoteProyeccion_ObtenerPorLote]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Roque Solis
-- Create date: 26/02/2014
-- Origen: APInterfaces
-- Description:	Obtener proyeccion por lote.
-- execute LoteProyeccion_ObtenerPorLote 1, 4;
-- =============================================
CREATE PROCEDURE [dbo].[LoteProyeccion_ObtenerPorLote]
@LoteID INT,
@OrganizacionID INT
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
	WHERE LoteID = @LoteID
	AND OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

GO
