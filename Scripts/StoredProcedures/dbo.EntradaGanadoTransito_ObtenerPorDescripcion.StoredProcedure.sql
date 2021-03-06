USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoTransito_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanadoTransito_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoTransito_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 25/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : EntradaGanadoTransito_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[EntradaGanadoTransito_ObtenerPorDescripcion]
@EntradaGanadoTransitoID int
AS 
BEGIN
	SET NOCOUNT ON;
	SELECT
		EGT.EntradaGanadoTransitoID,
		EGT.LoteID,
		EGT.Cabezas,
		EGT.Importe,
		EGT.Peso,
		EGT.Activo
		, L.Lote
	FROM EntradaGanadoTransito EGT
	INNER JOIN Lote L
		ON (EGT.LoteID = L.LoteID)
	WHERE EGT.EntradaGanadoTransitoID = @EntradaGanadoTransitoID
	SET NOCOUNT OFF;
END

GO
