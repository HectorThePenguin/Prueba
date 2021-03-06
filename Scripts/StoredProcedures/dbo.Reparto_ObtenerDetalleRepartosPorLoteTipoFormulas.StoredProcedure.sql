USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerDetalleRepartosPorLoteTipoFormulas]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerDetalleRepartosPorLoteTipoFormulas]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerDetalleRepartosPorLoteTipoFormulas]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Roque.Solis
-- Create date: 2014/03/20
-- Description: SP para consultar 
-- Origen     : APInterfaces
-- EXEC Reparto_ObtenerDetalleRepartosPorLoteTipoFormulas 1,4,
-- =============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerDetalleRepartosPorLoteTipoFormulas]
    @LoteId INT,
	@OrganizacionID INT,
	@XMLTiposFormula XML
AS
BEGIN
	CREATE TABLE #tTiposFormula
	(
		TipoFormulaID INT
	)
	INSERT INTO #tTiposFormula
	SELECT 
		t.item.value('./TipoFormulaID[1]', 'INT') AS TipoFormulaID
	FROM @XMLTiposFormula.nodes('ROOT/TiposFormulas') AS T(item)
		SELECT RD.RepartoDetalleID, 
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
				RD.Prorrateo,
				F.TipoFormulaID
		FROM Reparto R (NOLOCK)
		INNER JOIN RepartoDetalle RD (NOLOCK) ON R.RepartoID=RD.RepartoID AND RD.Activo=1
		INNER JOIN Formula F (NOLOCK) ON RD.FormulaIDServida=F.FormulaID AND F.Activo=1
		WHERE R.LoteID= @LoteId
		AND F.TipoFormulaID IN(SELECT TipoFormulaID FROM #tTiposFormula)
		AND R.OrganizacionID = @OrganizacionID
		AND R.Activo = 1 AND CAST(R.Fecha AS DATE) = CAST(GETDATE() AS DATE)
END

GO
