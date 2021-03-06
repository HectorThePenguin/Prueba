USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RepartoAlimentoDetalle_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RepartoAlimentoDetalle_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[RepartoAlimentoDetalle_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 14/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : RepartoAlimentoDetalle_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[RepartoAlimentoDetalle_ObtenerPorID]
@RepartoAlimentoDetalleID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		RepartoAlimentoDetalleID,
		RepartoAlimentoID,
		FolioReparto,
		FormulaIDRacion,
		Tolva,
		KilosEmbarcados,
		KilosRepartidos,
		Sobrante,
		PesoFinal
		CorralIDInicio,
		CorralIDFinal,
		HoraRepartoInicio,
		HoraRepartoFinal,
		Observaciones,
		Activo
	FROM RepartoAlimentoDetalle
	WHERE RepartoAlimentoDetalleID = @RepartoAlimentoDetalleID
	SET NOCOUNT OFF;
END

GO
