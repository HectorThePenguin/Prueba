USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificio_ObtenerDatosFacturaPorOrdenSacrificio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificio_ObtenerDatosFacturaPorOrdenSacrificio]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificio_ObtenerDatosFacturaPorOrdenSacrificio]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--=============================================
-- Author     : Ramses Santos
-- Create date: 2014/08/18
-- Description: Sp para obtener los datos para generar la factura.
-- exec LoteSacrificio_ObtenerDatosFacturaPorOrdenSacrificio 140
--=============================================
CREATE PROCEDURE [dbo].[LoteSacrificio_ObtenerDatosFacturaPorOrdenSacrificio] 
@OrdenSacrificioId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT LS.Serie, LS.Folio, LS.Fecha,
	  --Datos Cliente
	  Cte.CodigoSAP, Cte.Descripcion AS NombreCliente, Cte.RFC, Cte.Calle, Cte.CodigoPostal, Cte.Poblacion, Cte.Estado, 
	  Cte.Pais, MP.MetodoPagoID AS MetodoPago, Cte.CondicionPago, Cte.DiasPago,
   	  dbo.ObtenerTipoGanadoFactura(LS.LoteSacrificioID) AS TipoGanado,     
      COUNT(LSD.AnimalID) AS Cabezas,
      LS.PesoCanal AS PesoNoqueo,     
      LS.PesoPiel AS PesoPiel,    
      LS.PesoVisceras AS Visceras,    
	  LS.ImporteCanal, LS.ImportePiel, LS.ImporteVisera,
      ROUND(CAST(ISNULL((LS.ImporteCanal / NULLIF(LS.PesoCanal, 0)), 0) AS DECIMAL(18,2)), 2) AS PrecioNetoCanal,    
      ROUND(CAST(ISNULL((LS.ImportePiel / NULLIF(LS.PesoPiel, 0)), 0) AS DECIMAL(18,2)), 2) AS PrecioNetoPiel,    
      ROUND(CAST(ISNULL((LS.ImporteVisera / NULLIF(LS.PesoVisceras, 0)), 0) AS DECIMAL(18,2)), 2) AS PrecioNetoViscera    
	  FROM LoteSacrificio (NOLOCK) AS LS
      INNER JOIN LoteSacrificioDetalle LSD (NOLOCK) ON LS.LoteSacrificioID = LSD.LoteSacrificioID
	  LEFT JOIN Cliente (NOLOCK) AS Cte ON (Cte.ClienteID = LS.ClienteID)
	  LEFT JOIN MetodoPago (NOLOCK) AS MP ON (Cte.MetodoPagoID = MP.MetodoPagoID)
	  WHERE LS.OrdenSacrificioID = @OrdenSacrificioId
   	  GROUP BY LS.Serie, LS.Folio, LS.Fecha,    
      Cte.CodigoSAP, Cte.Descripcion, Cte.RFC, Cte.Calle, Cte.CodigoPostal, Cte.Poblacion, Cte.Estado,     
      Cte.Pais, MP.MetodoPagoID, Cte.CondicionPago, Cte.DiasPago,    
      LS.LoteSacrificioID, LS.PesoCanal, LS.PesoPiel, LS.PesoVisceras, 
      LS.ImporteCanal, LS.ImportePiel, LS.ImporteVisera
	SET NOCOUNT OFF;
END

GO
