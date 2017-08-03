USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanadoTransito_ObtenerDatosFactura]    Script Date: 21/04/2016 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[SalidaGanadoTransito_ObtenerDatosFactura]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanadoTransito_ObtenerDatosFactura]    Script Date: 21/04/2016 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Torres Lugo Manuel
-- Create date: 21/04/2016
-- Description: Obtiene los parametros para generar la factura de salidaganadomuerte
-- SalidaGanadoTransito_ObtenerDatosFactura 53, 1
-- =============================================
CREATE PROCEDURE [dbo].[SalidaGanadoTransito_ObtenerDatosFactura]
@Folio INT,
@Activo BIT
AS
BEGIN
  SELECT 
	SGT.Serie,
	SGT.FolioFactura,
	SGT.FechaCreacion,
	C.CodigoSAP,
	C.Descripcion,
	C.RFC,
	C.Calle,
	C.CodigoPostal,
	C.Poblacion,
	C.Estado,
	C.Pais,
	C.MetodoPagoID,
	C.CondicionPago,
	C.DiasPago,
	SGT.NumCabezas,
	SGT.Importe
FROM SalidaGanadoTransito AS SGT 
INNER JOIN Cliente AS C ON C.ClienteID = SGT.ClienteID
WHERE SGT.Folio = @Folio
AND SGT.Venta = @Activo
END