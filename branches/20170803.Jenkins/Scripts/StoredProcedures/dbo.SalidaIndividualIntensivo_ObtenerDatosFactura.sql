USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualIntensivo_ObtenerDatosFactura]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaIndividualIntensivo_ObtenerDatosFactura]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualIntensivo_ObtenerDatosFactura]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Daniel Benitez
-- Create date: 14/12/2015
-- Description:  Obtiene los datos de la venta de ganado intensivo para generar el xml de la factura.
-- SalidaIndividualIntensivo_ObtenerDatosFactura 64, 1
-- =============================================
CREATE PROCEDURE [dbo].[SalidaIndividualIntensivo_ObtenerDatosFactura]
	@FolioTicket INT
	, @OrganizacionID INT
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @EntradaGanadoID INT
	DECLARE @CorralID INT
	DECLARE @LoteID INT
	DECLARE @TipoGanado INT
	DECLARE @TipoGanadoDesc Varchar(100)
	
	SELECT @LoteID = loteid FROM salidaganadointensivo WHERE folioticket = @FolioTicket
	
	SELECT @CorralID = CorralID FROM lote WHERE loteid =  @LoteID
		
	SELECT @EntradaGanadoID = eng.EntradaGanadoID
	FROM EntradaGanado (NOLOCK) AS eng 
	WHERE eng.LoteID = @LoteID AND eng.CorralID = @CorralID 
	AND eng.OrganizacionID = @OrganizacionID AND eng.Activo = 1
		
	SELECT @TipoGanado = det.tipoganadoid, @TipoGanadoDesc = tg.Descripcion
	FROM entradaganado eg
	INNER JOIN entradaganadocosteo egc ON eg.EntradaGanadoId = egc.EntradaGanadoId
	INNER JOIN entradadetalle det ON det.EntradaGanadoCosteoID = egc.EntradaGanadoCosteoID 
	INNER JOIN tipoganado tg ON tg.tipoganadoid = det.tipoganadoid
	WHERE eg.entradaganadoid = @EntradaGanadoID	
		 
	SELECT SG.FolioTicket
	, SG.Fecha FechaVenta
	, Cte.CodigoSAP
	, Cte.Descripcion AS NombreCliente
	, Cte.RFC
	, Cte.Calle
	, Cte.CodigoPostal
	, Cte.Poblacion
	, Cte.Estado
	, Cte.Pais
	, MP.MetodoPagoID AS MetodoPago
	, Cte.CondicionPago
	, Cte.DiasPago
	, @TipoGanadoDesc AS DescripcionProducto	 
	, CP.Precio AS CausaPrecio
	, CP.CausaPrecioID
	, CAST(SG.Cabezas AS INT) Cabezas
	, SGP.PesoBruto
	, SGP.PesoTara
	, SG.FolioFactura
		FROM salidaganadointensivo (NOLOCK) AS SG
		INNER JOIN Cliente (NOLOCK) AS Cte 
			ON (Cte.ClienteID = SG.ClienteID)
		INNER JOIN MetodoPago (NOLOCK) AS MP 
			ON (Cte.MetodoPagoID = MP.MetodoPagoID)
		INNER JOIN SalidaGanadoIntensivoPesaje SGP
			ON (SG.SalidaGanadoIntensivoId = SGP.SalidaGanadoIntensivoId)
		INNER JOIN CausaPrecio CP
			ON (SG.CausaPrecioID = CP.CausaPrecioID)
		WHERE SG.FolioTicket = @FolioTicket
			AND SG.OrganizacionID = @OrganizacionID
	
	SET NOCOUNT OFF

END

GO
