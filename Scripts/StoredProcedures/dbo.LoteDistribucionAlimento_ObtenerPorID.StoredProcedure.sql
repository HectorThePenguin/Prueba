USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteDistribucionAlimento_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteDistribucionAlimento_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[LoteDistribucionAlimento_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 18/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : LoteDistribucionAlimento_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[LoteDistribucionAlimento_ObtenerPorID]
@LoteDistribucionAlimentoID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		LoteDistribucionAlimentoID,
		LoteID,
		TipoServicioID,
		EstatusDistribucionID,
		Fecha,
		Activo
	FROM LoteDistribucionAlimento
	WHERE LoteDistribucionAlimentoID = @LoteDistribucionAlimentoID
	SET NOCOUNT OFF;
END

GO
