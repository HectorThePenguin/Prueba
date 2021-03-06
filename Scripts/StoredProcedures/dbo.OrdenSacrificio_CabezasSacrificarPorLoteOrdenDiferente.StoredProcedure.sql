USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[OrdenSacrificio_CabezasSacrificarPorLoteOrdenDiferente]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[OrdenSacrificio_CabezasSacrificarPorLoteOrdenDiferente]
GO
/****** Object:  StoredProcedure [dbo].[OrdenSacrificio_CabezasSacrificarPorLoteOrdenDiferente]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Roque.Solis
-- Create date: 2014/02/27
-- Description: SP para consultar el numero de cabezas de un lote en otras ordenes de sacrificio
-- Origen     : APInterfaces
-- EXEC OrdenSacrificio_CabezasSacrificarPorLoteOrdenDiferente 1438,1
-- =============================================
CREATE PROCEDURE [dbo].[OrdenSacrificio_CabezasSacrificarPorLoteOrdenDiferente]
    @LoteId INT,
	@OrdenRepartoActual INT
AS
BEGIN
	SELECT ISNULL(SUM(CabezasSacrificio),0) Cabezas
	FROM OrdenSacrificioDetalle os
	LEFT JOIN LoteSacrificio ls
	   ON ls.LoteID = os.LoteID AND ls.OrdenSacrificioID = os.OrdenSacrificioID
	WHERE os.LoteID = @LoteId
	AND os.OrdenSacrificioID != @OrdenRepartoActual
	AND os.Activo=1
	AND ls.LoteSacrificioID IS NULL
END

GO
