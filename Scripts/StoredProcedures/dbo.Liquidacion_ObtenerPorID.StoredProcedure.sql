USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Liquidacion_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Liquidacion_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Liquidacion_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 16/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Liquidacion_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[Liquidacion_ObtenerPorID]
@LiquidacionID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		L.LiquidacionID,
		L.ContratoID,
		L.OrganizacionID,
		L.TipoCambio,
		L.Folio,
		L.Fecha,
		L.IPRM,
		L.CuotaEjidal,
		L.ProEducacion,
		L.PIEAFES,
		L.Factura,
		L.Cosecha,
		L.FechaInicio,
		L.FechaFin,
		L.Activo
		, O.Descripcion		AS Organizacion
	FROM Liquidacion L
	INNER JOIN Organizacion O
		ON (L.OrganizacionID = O.OrganizacionID)
	WHERE LiquidacionID = @LiquidacionID
	SET NOCOUNT OFF;
END

GO
