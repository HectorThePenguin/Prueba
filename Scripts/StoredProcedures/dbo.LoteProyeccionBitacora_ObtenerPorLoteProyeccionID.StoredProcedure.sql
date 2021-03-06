USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteProyeccionBitacora_ObtenerPorLoteProyeccionID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteProyeccionBitacora_ObtenerPorLoteProyeccionID]
GO
/****** Object:  StoredProcedure [dbo].[LoteProyeccionBitacora_ObtenerPorLoteProyeccionID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 04/06/2015
-- Description: 
-- SpName     : LoteProyeccionBitacora_ObtenerPorLoteProyeccionID 1
--======================================================
CREATE PROCEDURE [dbo].[LoteProyeccionBitacora_ObtenerPorLoteProyeccionID]
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
	FROM LoteProyeccionBitacora
	WHERE LoteProyeccionID = @LoteProyeccionID
	SET NOCOUNT OFF;
END

GO
