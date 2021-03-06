USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TraspasoMateriaPrima_ObtenerPorFolio]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TraspasoMateriaPrima_ObtenerPorFolio]
GO
/****** Object:  StoredProcedure [dbo].[TraspasoMateriaPrima_ObtenerPorFolio]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 12/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TraspasoMateriaPrima_ObtenerPorFolio 1,13,1
--======================================================
CREATE PROCEDURE [dbo].[TraspasoMateriaPrima_ObtenerPorFolio]
@OrganizacionID int,
@FolioTraspaso int,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		tmp.TraspasoMateriaPrimaID
		,tmp.OrganizacionID
		,tmp.ContratoOrigenID
		,tmp.ContratoDestinoID
		,tmp.FolioTraspaso
		,tmp.AlmacenOrigenID
		,tmp.AlmacenDestinoID
		,tmp.InventarioLoteOrigenID
		,tmp.InventarioLoteDestinoID
		,tmp.CuentaSAPID
		,tmp.Justificacion
		,tmp.AlmacenMovimientoEntradaID
		,tmp.AlmacenMovimientoSalidaID
		,tmp.FechaMovimiento
		,tmp.Activo
		,amd.Cantidad AS CantidadEntrada		
		,amd2.Cantidad AS CantidadSalida
		,amd.ProductoID	
	FROM TraspasoMateriaPrima tmp
	INNER JOIN AlmacenMovimiento am on tmp.AlmacenMovimientoEntradaID = am.AlmacenMovimientoID
	INNER JOIN AlmacenMovimientoDetalle amd on am.AlmacenMovimientoID = amd.AlmacenMovimientoID
	INNER JOIN AlmacenMovimiento am2 on tmp.AlmacenMovimientoSalidaID = am2.AlmacenMovimientoID
	INNER JOIN AlmacenMovimientoDetalle amd2 on am2.AlmacenMovimientoID = amd2.AlmacenMovimientoID
	INNER JOIN Producto pr on amd.ProductoID = pr.ProductoID
	WHERE tmp.OrganizacionID = @OrganizacionID
	AND tmp.FolioTraspaso = @FolioTraspaso
	and tmp.Activo = @Activo	
	SET NOCOUNT OFF;
END

GO
