USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerRepartoFechaLoteXML]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerRepartoFechaLoteXML]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerRepartoFechaLoteXML]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Jorge Luis Velazquez Araujo
-- Create date: 28/01/2015
-- Description: SP para consultar el reparto de la fecha actual por los Lotes
-- EXEC Reparto_ObtenerRepartoFechaLoteXML 4, 1, '2014-03-27'
-- =============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerRepartoFechaLoteXML]
@OrganizacionID INT,
@XmlLote XML,
--@LoteID INT,
@Fecha DATETIME
AS
BEGIN
SET NOCOUNT ON;
CREATE TABLE #tLote
	(
		LoteID INT
	)
INSERT INTO #tLote
	SELECT
		t.item.value('./LoteID[1]', 'INT') AS LoteID
	FROM @XmlLote.nodes('ROOT/Lotes') AS T (item)
	SELECT re.RepartoID,
		   re.OrganizacionID,
           re.LoteID,
           re.Fecha,
		   re.PesoInicio,
		   re.PesoProyectado,
		   re.DiasEngorda,
		   re.PesoRepeso
	FROM Reparto re
	INNER JOIN #tLote tl ON re.LoteID = tl.LoteID
	WHERE OrganizacionID = @OrganizacionID
	AND CAST(Fecha AS DATE) = CAST(@Fecha AS DATE)
	AND Activo= 1;
	SELECT 
		RD.RepartoDetalleID,
		RD.RepartoID,
		RD.TipoServicioID,
		RD.FormulaIDProgramada,
		RD.FormulaIDServida,
		RD.CantidadProgramada,
		RD.CantidadServida,
		RD.HoraReparto,
		RD.CostoPromedio,
		RD.Importe,
		RD.Servido,
		RD.Cabezas,
		RD.EstadoComederoID,
		RD.CamionRepartoID,
		RD.Observaciones,
		RD.Ajuste,
		F.TipoFormulaID
	FROM Reparto re
	INNER JOIN #tLote tl ON re.LoteID = tl.LoteID
	INNER JOIN RepartoDetalle rd on re.RepartoID = rd.RepartoID
	LEFT JOIN Formula F (NOLOCK) ON RD.FormulaIDServida = F.FormulaID
	WHERE re.OrganizacionID = @OrganizacionID
	AND CAST(re.Fecha AS DATE) = CAST(@Fecha AS DATE)	
	AND re.Activo= 1
	AND rd.Activo = 1
	SET NOCOUNT OFF;
END

GO
