USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerPesoLlegadaPorLoteXML]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerPesoLlegadaPorLoteXML]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerPesoLlegadaPorLoteXML]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Jorge Luis Velazquez Araujo
-- Create date: 2015/01/28
-- Description: SP para el consumo total del dia
-- EXEC Reparto_ObtenerPesoLlegadaPorLoteXML 1437, 4
-- =============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerPesoLlegadaPorLoteXML]
@XmlLote XML,
@OrganizacionID INT
AS
BEGIN
CREATE TABLE #tLote
	(
		LoteID INT
	)
INSERT INTO #tLote
	SELECT
		t.item.value('./LoteID[1]', 'INT') AS LoteID
	FROM @XmlLote.nodes('ROOT/Lotes') AS T (item)
SELECT
	L.LoteID,
	COUNT(A.AnimalID) Total,
	SUM(A.PesoLlegada) PesoLlegada,
	SUM(AM.Peso) Peso
FROM Animal A (NOLOCK)
INNER JOIN AnimalMovimiento AM (NOLOCK)
	ON A.AnimalID = AM.AnimalID
INNER JOIN Lote L (NOLOCK)
	ON AM.LoteId = L.LoteId
	INNER JOIN #tLote tL (NOLOCK)
	ON tL.LoteId = L.LoteId
WHERE L.OrganizacionID = @OrganizacionID
AND A.Activo = 1
AND AM.Activo = 1
AND L.Activo = 1
group by L.LoteID
SET NOCOUNT OFF;
END

GO
