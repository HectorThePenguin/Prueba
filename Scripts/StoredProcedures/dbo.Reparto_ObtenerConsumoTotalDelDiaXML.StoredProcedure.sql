USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerConsumoTotalDelDiaXML]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerConsumoTotalDelDiaXML]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerConsumoTotalDelDiaXML]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Jorge Luis Velazquez Araujo
-- Create date: 28/01/2015
-- Description: SP para el consumo total del dia de los corrales
-- EXEC Reparto_ObtenerConsumoTotalDelDiaXML 4, 37
-- =============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerConsumoTotalDelDiaXML]
@OrganizacionID INT,
@XmlCorrales XML,
@Fecha DATE
AS
BEGIN
SET NOCOUNT ON;
	CREATE TABLE #tCorral
	(
		CorralID INT
	)
	INSERT INTO #tCorral
	SELECT
		t.item.value('./CorralID[1]', 'INT') AS LoteID
	FROM @XmlCorrales.nodes('ROOT/Corrales') AS T (item)
SELECT
	SUM(RD.CantidadServida) ConsumoTotal,
	C.CorralID
FROM RepartoDetalle RD (NOLOCK)
INNER JOIN Reparto R (NOLOCK)
	ON RD.RepartoID = R.RepartoID
-- INNER JOIN Lote L (NOLOCK) ON R.LoteID=L.LoteID
INNER JOIN Corral C (NOLOCK)
	ON R.CorralID = C.CorralID
INNER JOIN #tCorral tc on c.CorralID = tc.CorralID
WHERE CAST(R.Fecha AS DATE) = @Fecha
AND RD.TipoServicioID IN (1, 2)
AND R.OrganizacionID = @OrganizacionID
AND R.Activo = 1
AND RD.Activo = 1
-- AND L.Activo=1
AND C.Activo = 1
group by C.CorralID
SET NOCOUNT OFF;
END

GO
