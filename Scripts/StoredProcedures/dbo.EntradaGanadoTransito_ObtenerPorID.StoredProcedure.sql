USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoTransito_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanadoTransito_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoTransito_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 25/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : EntradaGanadoTransito_ObtenerPorID 4
--======================================================
CREATE PROCEDURE [dbo].[EntradaGanadoTransito_ObtenerPorID]
@EntradaGanadoTransitoID int
AS 
BEGIN
	SET NOCOUNT ON;
	
	SELECT EGT.EntradaGanadoTransitoID
		,EGT.LoteID
		,EGT.Cabezas
		,EGT.Peso
		,EGT.Activo
		,L.Lote
		,C.Codigo
		,C.CorralID
	INTO #tEntradaGanadoTransito
	FROM EntradaGanadoTransito EGT
	INNER JOIN Lote L ON (EGT.LoteID = L.LoteID)
	INNER JOIN Corral C ON (L.CorralID = C.CorralID)
	WHERE EGT.EntradaGanadoTransitoID = @EntradaGanadoTransitoID

	SELECT EntradaGanadoTransitoID
		,LoteID
		,Cabezas
		,Peso
		,Activo
		,Lote
		,Codigo
		,CorralID
	FROM #tEntradaGanadoTransito

	SELECT EGTD.EntradaGanadoTransitoDetalleID
		,  EGTD.EntradaGanadoTransitoID
		,  EGTD.CostoID
		,  EGTD.Importe
	FROM #tEntradaGanadoTransito EGT
	INNER JOIN EntradaGanadoTransitoDetalle EGTD
		ON (EGT.EntradaGanadoTransitoID = EGTD.EntradaGanadoTransitoID)

	DROP TABLE #tEntradaGanadoTransito

	SET NOCOUNT OFF;
END

GO
