USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Liquidacion_ObtenerDatosEntradaContrato]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Liquidacion_ObtenerDatosEntradaContrato]
GO
/****** Object:  StoredProcedure [dbo].[Liquidacion_ObtenerDatosEntradaContrato]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza 
-- Create date: 17/12/2014
-- Description: 
-- SpName     : Liquidacion_ObtenerDatosEntradaContrato 5701, 1, 16
--======================================================
CREATE PROCEDURE [dbo].[Liquidacion_ObtenerDatosEntradaContrato]
@ProveedorID		INT
, @OrganizacionID	INT
, @ContratoID		INT
AS
BEGIN

	SET NOCOUNT ON

		DECLARE @IndicadorHumedad	INT
			  , @IndicadorImpurezas INT
			  , @IndicadorCalor		INT
			  , @IndicadorTotales	INT
		SET @IndicadorHumedad = 4
		SET @IndicadorImpurezas = 5
		SET @IndicadorCalor = 7
		SET @IndicadorTotales = 8

		SELECT EP.Fecha
			,  EP.Folio													AS Ticket
			,  RV.Camion												AS Placa
			,  EP.PesoOrigen											AS PesoComersin
			,  CAST(ISNULL(EPM.Porcentaje, 0) AS DECIMAL(18,2))			AS Humedad
			,  CAST(ISNULL(EPMImp.Porcentaje, 0) AS DECIMAL(18,2))		AS Impureza
			,  CAST(ISNULL(EPMCalor.Porcentaje, 0) AS DECIMAL(18,2))	AS Calor
			,  CAST(ISNULL(EPMTotal.Porcentaje, 0) AS DECIMAL(18,2))	AS Total
			,  CAST(ISNULL(EPM.Descuento, 0) AS DECIMAL(18,2))			AS PorcentajeHumedad
			,  CAST(ISNULL(EPMImp.Descuento, 0) AS DECIMAL(18,2))		AS PorcentajeImpureza
			,  CAST(ISNULL(EPMCalor.Descuento, 0) AS DECIMAL(18,2))		AS PorcentajeCalor
			,  CAST(ISNULL(EPMTotal.Descuento, 0) AS DECIMAL(18,2))		AS PorcentajeTotal
			,  C.Precio
			,  TC.Cambio
			,  EP.EntradaProductoID
			,  ISNULL(LD.LiquidacionDetalleID, 0)						AS LiquidacionDetalleID
			,  ISNULL(LD.LiquidacionID, 0)								AS LiquidacionID
		FROM Contrato C
		INNER JOIN EntradaProducto EP
			ON (C.ContratoID = EP.ContratoID)
		INNER JOIN RegistroVigilancia RV
			ON (C.ContratoID = RV.ContratoID
				AND EP.RegistroVigilanciaID = RV.RegistroVigilanciaID)
		LEFT OUTER JOIN  EntradaProductoDetalle EPD
			ON (EP.EntradaProductoID = EPD.EntradaProductoID
				AND EPD.IndicadorID = @IndicadorHumedad)
		LEFT OUTER JOIN  EntradaProductoMuestra EPM
			ON (EPD.EntradaProductoDetalleID = EPM.EntradaProductoDetalleID
				AND EPM.EsOrigen = 0)
		LEFT OUTER JOIN  EntradaProductoDetalle EPDImp
			ON (EP.EntradaProductoID = EPDImp.EntradaProductoID
				AND EPDImp.IndicadorID = @IndicadorImpurezas)
		LEFT OUTER JOIN  EntradaProductoMuestra EPMImp
			ON (EPDImp.EntradaProductoDetalleID = EPMImp.EntradaProductoDetalleID
				AND EPMImp.EsOrigen = 0)
		LEFT OUTER JOIN  EntradaProductoDetalle EPDCalor
			ON (EP.EntradaProductoID = EPDCalor.EntradaProductoID
				AND EPDCalor.IndicadorID = @IndicadorCalor)
		LEFT OUTER JOIN  EntradaProductoMuestra EPMCalor
			ON (EPDCalor.EntradaProductoDetalleID = EPMCalor.EntradaProductoDetalleID
				AND EPMCalor.EsOrigen = 0)
		LEFT OUTER JOIN  EntradaProductoDetalle EPDTotal
			ON (EP.EntradaProductoID = EPDTotal.EntradaProductoID
				AND EPDTotal.IndicadorID = @IndicadorTotales)
		LEFT OUTER JOIN  EntradaProductoMuestra EPMTotal
			ON (EPDTotal.EntradaProductoDetalleID = EPMTotal.EntradaProductoDetalleID
				AND EPMTotal.EsOrigen = 0)
		LEFT OUTER JOIN LiquidacionDetalle LD
			ON (EP.EntradaProductoID = LD.EntradaProductoID)
		INNER JOIN TipoCambio TC
			ON (C.TipoCambioID = TC.TipoCambioID)
		WHERE C.ProveedorID = @ProveedorID
			AND C.OrganizacionID = @OrganizacionID
			AND C.ContratoID = @ContratoID

	SET NOCOUNT OFF
END
GO
