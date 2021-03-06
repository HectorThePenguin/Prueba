USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorIDOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerPorIDOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorIDOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Marco Zamora
-- Origen:   APInterfaces
-- Create date: 20/11/2013
-- Description:  Metodo para Obtener las entradas de Ganado por Organizacion
-- EntradaGanado_ObtenerPorIDOrganizacion 5,1
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerPorIDOrganizacion]
 @OrganizacionID INT,
 @TipoCorralID INT
AS
BEGIN 

  SELECT 
   eg.EntradaGanadoID,
   eg.FolioEntrada,
   eg.OrganizacionID,
   eg.OrganizacionOrigenID,
   eg.FechaEntrada,
   eg.FolioOrigen,
   eg.FechaSalida,
   eg.CamionID,
   eg.ChoferID,
   eg.JaulaID,
   eg.CabezasOrigen,
   eg.CabezasRecibidas,
   eg.OperadorID,
   eg.PesoBruto,
   eg.PesoTara,
   eg.EsRuteo,
   eg.Fleje,
   eg.CheckList,
   eg.CorralID,
   eg.LoteID,
   eg.Observacion,
   eg.ImpresionTicket,
   eg.Costeado,
   eg.Manejado,
   eg.Activo,
   c.Codigo CodigoCorral,
   o.Descripcion OrganizacionOrigen,
   eg.EmbarqueID,
   l.lote,
   /* Si es compra directa se vaa entrada detalle sino a interfazSalidaAnimal*/
	CASE WHEN o.TipoOrganizacionID = 3
	THEN
		CAST(COALESCE((	
			SELECT SUM(ED.PesoOrigen)
				FROM EntradaDetalle ED
			 INNER JOIN EntradaGanadoCosteo EGC ON EGC.EntradaGanadoCosteoID = ED.EntradaGanadoCosteoID 
			 WHERE EGC.EntradaGanadoID = eg.EntradaGanadoID
		 ), 0 ) AS INT)
	ELSE
		CAST(COALESCE((	
			SELECT SUM(PesoOrigen) 
				FROM InterfaceSalidaAnimal 
				 WHERE OrganizacionID = eg.OrganizacionOrigenID
					 AND SalidaID = eg.FolioOrigen
		 ), 0 ) AS INT)
	END AS PesoOrigen
  FROM EntradaGanado eg
  INNER JOIN Corral c ON c.CorralID = eg.CorralID
  INNER JOIN Lote l ON eg.LoteID = l.LoteID
  INNER JOIN Organizacion o ON o.OrganizacionID = eg.OrganizacionOrigenID
  LEFT JOIN EvaluacionCorral ec ON ec.LoteID = eg.LoteID
  WHERE eg.OrganizacionID = @OrganizacionID 
  AND l.Activo = 1
  --AND eg.Costeado = 1
  AND ec.FolioEvaluacion IS NULL
  AND c.TipoCorralID =  @TipoCorralID
  --AND NOT EXISTS (
		--SELECT DISTINCT LoteID
		--FROM EntradaGanado EGT
		--WHERE EGT.LoteID = l.LoteID
		--AND EGT.Costeado != 1
		--AND EGT.EntradaGanadoID != eg.EntradaGanadoID
		--)
END
GO
