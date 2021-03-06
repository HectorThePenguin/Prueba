USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LectorRegistro_ObtenerLectorComedero]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LectorRegistro_ObtenerLectorComedero]
GO
/****** Object:  StoredProcedure [dbo].[LectorRegistro_ObtenerLectorComedero]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:    Jos� Gilberto Quintero L�pez 
-- Create date: 02-04-2014  
-- Description:  Obtiene el historial de reparto por organizacionID
-- LectorRegistro_ObtenerLectorComedero 1 , '20140411'
-- =============================================  
CREATE PROCEDURE [dbo].[LectorRegistro_ObtenerLectorComedero] @OrganizacionID INT
	,@Fecha DATETIME
AS
BEGIN
	DECLARE @LectorRegistroDetalle AS TABLE (
		LectorRegistroDetalleID INT
		,LectorRegistroID INT
		,TipoServicioID INT
		,FormulaIDAnterior INT
		,FormulaIDProgramada INT
		,Activo BIT
		,Orden INT
		)
	INSERT INTO @LectorRegistroDetalle (
		LectorRegistroDetalleID
		,LectorRegistroID
		,TipoServicioID
		,FormulaIDAnterior
		,FormulaIDProgramada
		,Activo
		,Orden
		)
	SELECT a.*
	FROM (
		SELECT ld.LectorRegistroDetalleID
			,ld.LectorRegistroID
			,ld.TipoServicioID
			,ld.FormulaIDAnterior
			,ld.FormulaIDProgramada
			,ld.Activo
			,ROW_NUMBER() OVER (
				PARTITION BY ld.LectorRegistroID ORDER BY ld.LectorRegistroID
					,ld.FechaCreacion
				) AS [Orden]
		FROM LectorRegistroDetalle ld
		INNER JOIN LectorRegistro l ON l.LectorRegistroID = ld.LectorRegistroID
			AND l.OrganizacionID = @OrganizacionID
			AND @Fecha = l.Fecha
		) a
	SELECT lr.OrganizacionID AS [IdGanadera]
		,lr.Seccion AS [IdSeccion]
		,c.Codigo AS [IdCorralEngorda]
		,lo.Lote AS [IdLoteEngorda]
		,lr.Fecha AS [Fecha]		
		,ld1.FormulaIDAnterior AS [IdFormula_A]
		,ld1.FormulaIDProgramada AS [IdFormula_N]
		,ld2.FormulaIDAnterior AS [IdFormula2_A]
		,ld2.FormulaIDProgramada AS [IdFormula2_N]		
		,lr.CambioFormula AS [bCambioFormula]
		,lo.Cabezas AS [Cabezas]
		,lr.EstadoComederoID AS [IdEstadoComedero]
		,lr.CantidadOriginal AS [CantidadOriginal]
		,lr.CantidadPedido AS [CantidadPedido]
		,lr.Activo AS [IdStatus]
	FROM LectorRegistro lr
	INNER JOIN Lote lo ON lo.LoteID = lr.LoteID
	INNER JOIN Corral c ON c.CorralID = lo.CorralID
	INNER JOIN @LectorRegistroDetalle ld1 ON ld1.LectorRegistroID = lr.LectorRegistroID
		AND ld1.Orden = 1
	LEFT JOIN @LectorRegistroDetalle ld2 ON ld2.LectorRegistroID = lr.LectorRegistroID
		AND ld2.Orden = 2
	WHERE lr.OrganizacionID = @OrganizacionID
		AND @Fecha IN (lr.Fecha)
	ORDER BY lr.Fecha
END

GO
