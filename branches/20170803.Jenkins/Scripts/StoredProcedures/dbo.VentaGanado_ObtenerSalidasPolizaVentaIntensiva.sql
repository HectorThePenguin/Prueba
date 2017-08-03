USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[VentaGanado_ObtenerSalidasPolizaVentaIntensiva]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[VentaGanado_ObtenerSalidasPolizaVentaIntensiva]
GO
/****** Object:  StoredProcedure [dbo].[VentaGanado_ObtenerSalidasPolizaVentaIntensiva]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hugo Castillo
-- Create date: 03-12-2015
-- Description:	Obtiene los datos para generar la poliza de salida
-- VentaGanado_ObtenerSalidasPolizaVentaIntensiva 54, 2
-- =============================================
CREATE PROCEDURE [dbo].[VentaGanado_ObtenerSalidasPolizaVentaIntensiva]
@FolioTicket INT
, @OrganizacionID INT
AS
BEGIN

	SET NOCOUNT ON

		SELECT DISTINCT SGI.FolioTicket
			,  SGI.FolioFactura
			,  SGI.Fecha FechaVenta
			,  SGI.ClienteID
			,  SGI.Cabezas
			,  SGIP.PesoBruto
			,  SGIP.PesoTara
			,  C.CodigoSAP
			,  CP.Precio			
			,  CP.CausaPrecioID
			, GIC.Importe
			, GIC.CostoID
		FROM SalidaGanadoIntensivo SGI
		INNER JOIN Cliente C
			ON (SGI.ClienteID = C.ClienteID)
		INNER JOIN SalidaGanadoIntensivoPesaje SGIP
			ON (SGI.SalidaGanadoIntensivoID = SGIP.SalidaGanadoIntensivoID)
		INNER JOIN GanadoIntensivoCosto GIC
			ON (SGI.SalidaGanadoIntensivoID = GIC.SalidaGanadoIntensivoID)
		INNER JOIN CausaPrecio CP
			ON (SGI.CausaPrecioID = CP.CausaPrecioID)
		WHERE SGI.FolioTicket = @FolioTicket
			AND SGI.OrganizacionID = @OrganizacionID

	SET NOCOUNT OFF

END

GO
