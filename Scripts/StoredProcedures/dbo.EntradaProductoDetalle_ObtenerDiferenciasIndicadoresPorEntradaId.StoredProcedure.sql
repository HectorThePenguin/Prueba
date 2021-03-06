USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProductoDetalle_ObtenerDiferenciasIndicadoresPorEntradaId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProductoDetalle_ObtenerDiferenciasIndicadoresPorEntradaId]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProductoDetalle_ObtenerDiferenciasIndicadoresPorEntradaId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Alejandro Quiroz	
-- Create date: 21/08/2014
-- Description: Consulta los indicadores de una entrada y los porcentajes del contrato detalle.
-- SpName     : exec EntradaProductoDetalle_ObtenerDiferenciasIndicadoresPorEntradaId 1
--======================================================
CREATE PROCEDURE [dbo].[EntradaProductoDetalle_ObtenerDiferenciasIndicadoresPorEntradaId]
@EntradaProductoId INT
AS 
BEGIN
	SELECT I.IndicadorID, I.Descripcion, EPM.Porcentaje AS PorcentajeMuestra, CD.PorcentajePermitido AS PorcentajeContrato
	INTO #Consulta
	FROM EntradaProducto AS EP 
	INNER JOIN EntradaProductoDetalle AS EPD ON EPD.EntradaProductoID = EP.EntradaProductoID
	INNER JOIN EntradaProductoMuestra AS EPM ON EPD.EntradaProductoDetalleID = EPM.EntradaProductoDetalleID
	INNER JOIN ContratoDetalle AS CD ON (CD.ContratoID = EP.ContratoID AND EPD.IndicadorID = CD.IndicadorID)
	INNER JOIN Indicador AS I ON EPD.IndicadorID = I.IndicadorID
	WHERE EP.EntradaProductoID = @EntradaProductoId
	and EPM.EsOrigen = 0
	
	SELECT 
		tmpGeneral.IndicadorID, tmpGeneral.Descripcion,SUM(PorcentajeMuestra)/(SELECT COUNT(*) FROM #Consulta tmp WHERE tmp.IndicadorID = tmpGeneral.IndicadorID) AS PorcentajeMuestra,PorcentajeContrato
	FROM #Consulta tmpGeneral
	GROUP BY tmpGeneral.IndicadorID,tmpGeneral.Descripcion,tmpGeneral.PorcentajeContrato
END
GO
