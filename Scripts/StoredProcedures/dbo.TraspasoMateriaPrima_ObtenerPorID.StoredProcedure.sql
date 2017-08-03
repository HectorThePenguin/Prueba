USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TraspasoMateriaPrima_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TraspasoMateriaPrima_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[TraspasoMateriaPrima_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 02/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TraspasoMateriaPrima_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[TraspasoMateriaPrima_ObtenerPorID]
@TraspasoMateriaPrimaID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TraspasoMateriaPrimaID
		,OrganizacionID
		,ContratoOrigenID
		,ContratoDestinoID
		,FolioTraspaso
		,AlmacenOrigenID
		,AlmacenDestinoID
		,InventarioLoteOrigenID
		,InventarioLoteDestinoID
		,CuentaSAPID
		,Justificacion
		,AlmacenMovimientoEntradaID
		,AlmacenMovimientoSalidaID
		,FechaMovimiento
		,Activo
	FROM TraspasoMateriaPrima
	WHERE TraspasoMateriaPrimaID = @TraspasoMateriaPrimaID
	SET NOCOUNT OFF;
END

GO
