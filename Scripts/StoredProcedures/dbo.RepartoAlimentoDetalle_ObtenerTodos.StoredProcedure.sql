USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RepartoAlimentoDetalle_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RepartoAlimentoDetalle_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[RepartoAlimentoDetalle_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 14/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : RepartoAlimentoDetalle_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[RepartoAlimentoDetalle_ObtenerTodos]
@Activo BIT = NULL
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
		PesoFinal,
		CorralIDInicio,
		CorralIDFinal,
		HoraRepartoInicio,
		HoraRepartoFinal,
		Observaciones,
		Activo
	FROM RepartoAlimentoDetalle
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
