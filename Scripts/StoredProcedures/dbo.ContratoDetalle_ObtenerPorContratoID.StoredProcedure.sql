USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ContratoDetalle_ObtenerPorContratoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ContratoDetalle_ObtenerPorContratoID]
GO
/****** Object:  StoredProcedure [dbo].[ContratoDetalle_ObtenerPorContratoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 20/05/2014
-- Description: Obtiene contratos detalle por ContratoID
-- Modifico   : Jose Angel Rodriguez
-- Create date: 22/08/2014
-- Descripcion: Validacion de indicadores
-- SpName     : ContratoDetalle_ObtenerPorContratoID 8
--======================================================
CREATE PROCEDURE [dbo].[ContratoDetalle_ObtenerPorContratoID]
@ContratoID INT
AS
BEGIN
CREATE TABLE #contratohumedadtemp(
	FechaInicio varchar(10),
	PorcentajeHumedad	decimal(10,2),
	ContratoID	int
)
INSERT INTO #contratohumedadtemp
SELECT TOP 1 convert(varchar(10),CH.FechaInicio,103),ISNULL(CH.PorcentajeHumedad,0.00) ,CH.ContratoID 
FROM contratohumedad CH (NOLOCK)
WHERE CH.ContratoID = @ContratoID
AND CH.FechaInicio < getdate()
GROUP BY CH.FechaInicio,CH.PorcentajeHumedad,CH.ContratoID
declare @variablevalidar int 
set @variablevalidar = (select ContratoID from #contratohumedadtemp)
IF isnull(@variablevalidar,0) = 0 
BEGIN
	SELECT 
		CD.ContratoDetalleID,
		CD.ContratoID,
		CD.IndicadorID,
		I.Descripcion,
		CD.Activo
		,CD.PorcentajePermitido
		,PorcentajeHumedad =  0.00
		,FechaInicio = convert(varchar(10),getdate()+1,103)
	FROM Contrato c	 (NOLOCK)
	INNER JOIN ContratoDetalle CD (NOLOCK) ON c.ContratoID = CD.ContratoID
	INNER JOIN Indicador I ON I.IndicadorID = CD.IndicadorID
	WHERE CD.ContratoID = @ContratoID
END
ELSE
	SELECT 
		CD.ContratoDetalleID,
		CD.ContratoID,
		CD.IndicadorID,
		I.Descripcion,
		CD.Activo
		,CD.PorcentajePermitido
		,PorcentajeHumedad =  tmp.PorcentajeHumedad
		,FechaInicio = tmp.FechaInicio
	FROM Contrato c	 (NOLOCK)
	INNER JOIN ContratoDetalle CD (NOLOCK) ON c.ContratoID = CD.ContratoID
	INNER JOIN Indicador I ON I.IndicadorID = CD.IndicadorID
	INNER JOIN #contratohumedadtemp tmp ON CD.ContratoID = tmp.ContratoID
	WHERE CD.ContratoID = @ContratoID
DROP TABLE #contratohumedadtemp
END

GO
