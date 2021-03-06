USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Liquidacion_ObtenerDatosLiquidacionPorFolio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Liquidacion_ObtenerDatosLiquidacionPorFolio]
GO
/****** Object:  StoredProcedure [dbo].[Liquidacion_ObtenerDatosLiquidacionPorFolio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza 
-- Create date: 17/12/2014
-- Description: 
-- SpName     : Liquidacion_ObtenerDatosLiquidacionPorFolio 1, 1
--======================================================
CREATE PROCEDURE [dbo].[Liquidacion_ObtenerDatosLiquidacionPorFolio]
@Folio				BIGINT
, @OrganizacionID	INT
AS
BEGIN

	SET NOCOUNT ON

		DECLARE @IndicadorHumedad	INT
			  , @IndicadorImpurezas INT
			  , @IndicadorCalor		INT
			  , @IndicadorTotales	INT
			  , @LiquidacionID		INT

		SET @IndicadorHumedad = 4
		SET @IndicadorImpurezas = 5
		SET @IndicadorCalor = 7
		SET @IndicadorTotales = 8

		CREATE TABLE #tLiquidacion
		(
			LiquidacionID		INT
			,  ContratoID		INT
			,  OrganizacionID	INT
			,  TipoCambio		DECIMAL(10, 4)
			,  Folio			BIGINT
			,  Fecha			SMALLDATETIME
			,  IPRM				DECIMAL(10, 4)
			,  CuotaEjidal		DECIMAL(10, 4)
			,  ProEducacion		DECIMAL(10, 4)
			,  PIEAFES			DECIMAL(10, 4)
			,  Factura			VARCHAR(50)
			,  Cosecha			CHAR(3)
			,  FechaInicio		SMALLDATETIME
			,  FechaFin			SMALLDATETIME
		)

		INSERT INTO #tLiquidacion
		SELECT L.LiquidacionID
				,  L.ContratoID
				,  L.OrganizacionID
				,  L.TipoCambio
				,  L.Folio
				,  L.Fecha
				,  L.IPRM
				,  L.CuotaEjidal
				,  L.ProEducacion
				,  L.PIEAFES
				,  L.Factura
				,  L.Cosecha
				,  L.FechaInicio
				,  L.FechaFin
		FROM Liquidacion L
		WHERE L.Folio = @Folio 
			AND L.OrganizacionID = @OrganizacionID

		SELECT L.LiquidacionID
			,  L.ContratoID
			,  L.OrganizacionID
			,  L.TipoCambio
			,  L.Folio
			,  L.Fecha
			,  L.IPRM
			,  L.CuotaEjidal
			,  L.ProEducacion
			,  L.PIEAFES
			,  L.Factura
			,  L.Cosecha
			,  YEAR(L.FechaInicio)			AS AnioInicio
			,  YEAR(L.FechaFin)				AS AnioFin
			,  O.Descripcion				AS Organizacion
			,  P.Descripcion				AS Proveedor
			,  P.CodigoSAP
			,  P.ProveedorID
			,  Prod.Descripcion				AS Producto
			,  Prod.ProductoID
			,  C.Folio						AS FolioContrato
			,  C.FolioAserca
			,  ISNULL(C.FolioCobertura, 0)	AS FolioCobertura
		FROM #tLiquidacion L
		INNER JOIN Organizacion O
			ON (L.OrganizacionID = O.OrganizacionID)
		INNER JOIN Contrato C
			ON (L.ContratoID = C.ContratoID)
		INNER JOIN Proveedor P
			ON (C.ProveedorID = P.ProveedorID)
		INNER JOIN Producto Prod
			ON (C.ProductoID = Prod.ProductoID)

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
		INNER JOIN #tLiquidacion L
			ON (LD.LiquidacionID = L.LiquidacionID)
		INNER JOIN TipoCambio TC
			ON (C.TipoCambioID = TC.TipoCambioID)

	SET NOCOUNT OFF
END
GO
