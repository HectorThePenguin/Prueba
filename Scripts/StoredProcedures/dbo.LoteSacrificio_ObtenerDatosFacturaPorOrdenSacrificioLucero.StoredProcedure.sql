USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificio_ObtenerDatosFacturaPorOrdenSacrificioLucero]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificio_ObtenerDatosFacturaPorOrdenSacrificioLucero]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificio_ObtenerDatosFacturaPorOrdenSacrificioLucero]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--=============================================
-- Author     : Ramses Santos
-- Create date: 2014/08/18
-- Description: Sp para obtener los datos para generar la factura.
-- exec LoteSacrificio_ObtenerDatosFacturaPorOrdenSacrificioLucero 140
--=============================================
CREATE PROCEDURE [dbo].[LoteSacrificio_ObtenerDatosFacturaPorOrdenSacrificioLucero] @OrganizacionID INT
	,@XmlInterface XML
AS
BEGIN
	SET NOCOUNT ON;

 	CREATE TABLE #tInterfaceDetalle (Id BIGINT, Serie VARCHAR(2), Folio VARCHAR(10))      

	INSERT INTO #tInterfaceDetalle
	SELECT t.item.value('./Id[1]', 'BIGINT') AS Id
		  ,t.item.value('./Serie[1]', 'VARCHAR(2)') AS Serie
		  ,t.item.value('./Folio[1]', 'VARCHAR(15)') AS Folio
	FROM @XmlInterface.nodes('ROOT/InterfaceDetalleID') AS T(item)

	SELECT LS.Serie
		,LS.Folio
		,LS.Fecha
		,
		--Datos Cliente
		Cte.CodigoSAP
		,Cte.Descripcion AS NombreCliente
		,Cte.RFC
		,Cte.Calle
		,Cte.CodigoPostal
		,Cte.Poblacion
		,Cte.Estado
		,Cte.Pais
		,MP.MetodoPagoID AS MetodoPago
		,Cte.CondicionPago
		,Cte.DiasPago
  		,dbo.ObtenerTipoGanadoFactura(LS.LoteSacrificioID) AS TipoGanado    
  		,COUNT(LSD.AnimalID) AS Cabezas  
  		,LS.PesoCanal AS PesoNoqueo  
  		,LS.PesoPiel AS PesoPiel        
  		,LS.PesoVisceras AS Visceras        
		,LS.ImporteCanal
		,LS.ImportePiel
		,LS.ImporteVisera
  		,ROUND(CAST(ISNULL((LS.ImporteCanal / NULLIF(LS.PesoCanal, 0)), 0) AS DECIMAL(18, 2)), 2) AS PrecioNetoCanal    
  		,ROUND(CAST(ISNULL((LS.ImportePiel / NULLIF(LS.PesoPiel, 0)), 0) AS DECIMAL(18, 2)), 2) AS PrecioNetoPiel    
  		,ROUND(CAST(ISNULL((LS.ImporteVisera / NULLIF(LS.PesoVisceras, 0)), 0) AS DECIMAL(18, 2)), 2) AS PrecioNetoViscera    
	FROM LoteSacrificioLucero(NOLOCK) AS LS
 	INNER JOIN LoteSacrificioLuceroDetalle LSD (NOLOCK) ON LS.LoteSacrificioID = LSD.LoteSacrificioID    
	INNER JOIN Lote L ON (
			LS.LoteID = L.LoteID
			AND L.OrganizacionID = @OrganizacionID
			)
 	INNER JOIN #tInterfaceDetalle x 
       ON (LS.InterfaceSalidaTraspasoDetalleID = x.Id
      AND LS.Serie = x.Serie
      AND LS.Folio = x.Folio)      
	LEFT JOIN Cliente(NOLOCK) AS Cte ON (Cte.ClienteID = LS.ClienteID)
	LEFT JOIN MetodoPago(NOLOCK) AS MP ON (Cte.MetodoPagoID = MP.MetodoPagoID)
GROUP BY LS.Serie, LS.Folio, LS.Fecha,        
      Cte.CodigoSAP, Cte.Descripcion, Cte.RFC, Cte.Calle, Cte.CodigoPostal, Cte.Poblacion, Cte.Estado,         
      Cte.Pais, MP.MetodoPagoID, Cte.CondicionPago, Cte.DiasPago,        
      LS.LoteSacrificioID, LS.PesoCanal, LS.PesoPiel, LS.PesoVisceras,     
      LS.ImporteCanal, LS.ImportePiel, LS.ImporteVisera    
	DROP TABLE #tInterfaceDetalle

	SET NOCOUNT OFF;
END
GO
