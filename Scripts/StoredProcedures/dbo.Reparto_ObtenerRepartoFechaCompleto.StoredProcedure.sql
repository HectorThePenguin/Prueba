USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerRepartoFechaCompleto]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerRepartoFechaCompleto]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerRepartoFechaCompleto]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Gilberto Carranza
-- Create date: 2014/08/22
-- Description: SP para consultar el reparto de la fecha actual
-- Reparto_ObtenerRepartoFechaCompleto '<ROOT><Lotes><LoteID>1298</LoteID></Lotes></ROOT>', '2014-06-18'
-- =============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerRepartoFechaCompleto]
@XmlLote XML,
@Fecha DATETIME
AS
BEGIN
	SET NOCOUNT ON;
		CREATE TABLE #tReparto
		(
			RepartoID			BIGINT
			, OrganizacionID	INT
			, LoteID			INT
			, Fecha				SMALLDATETIME
			, PesoInicio		INT
			, PesoProyectado	INT
			, DiasEngorda		INT
			, PesoRepeso		INT
		)
		DECLARE @Activo BIT
		SET @Activo = 1
		INSERT INTO #tReparto
		SELECT R.RepartoID,
			   R.OrganizacionID,
			   R.LoteID,
			   R.Fecha,
			   R.PesoInicio,
			   R.PesoProyectado,
			   R.DiasEngorda,
			   R.PesoRepeso
		FROM Reparto R
		INNER JOIN 
		(
			SELECT T.N.value('./LoteID[1]','INT') AS LoteID
			FROM @XmlLote.nodes('/ROOT/Lotes') as T(N)
		) A ON (R.LoteID = A.LoteID)
		WHERE CAST(Fecha AS DATE) = CAST(@Fecha AS DATE)
			AND Activo= @Activo
		SELECT RepartoID,
			   OrganizacionID,
			   LoteID,
			   Fecha,
			   PesoInicio,
			   PesoProyectado,
			   DiasEngorda,
			   PesoRepeso
		FROM #tReparto
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
			F.TipoFormulaID
		FROM RepartoDetalle RD (NOLOCK)
		LEFT JOIN Formula F (NOLOCK) 
			ON (RD.FormulaIDServida = F.FormulaID)
		INNER JOIN #tReparto tR
			ON (RD.RepartoID = tR.RepartoID)
	SET NOCOUNT OFF;
END

GO
