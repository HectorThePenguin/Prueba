USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerDiasEngordaPorLoteXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerDiasEngordaPorLoteXML]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerDiasEngordaPorLoteXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Jorge Luis Velazquez Araujo
-- Create date: 28/01/2015
-- Description:	Obtiene Los dias envorda promedio de los Lotes
-- EXEC Corral_ObtenerDiasEngordaPorLoteXML 1
--=============================================
CREATE PROCEDURE [dbo].[Corral_ObtenerDiasEngordaPorLoteXML]
	@XmlLote XML
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
		--, t.item.value('./OrganizacionID[1]', 'INT') AS OrganizacionID
	FROM @XmlLote.nodes('ROOT/Lotes') AS T (item)

	DECLARE @OrganizacionID INT
	SET @OrganizacionID = (SELECT TOP 1 L.OrganizacionID FROM #tLote tL INNER JOIN Lote L ON (tL.LoteID = L.LoteID))
	
	SELECT COALESCE((SUM(DATEDIFF(DAY, EG.FechaEntrada, GETDATE()))/COUNT(1)),0) AS DiasEngorda,
	L.LoteID
	  FROM #tLote tl
	 INNER JOIN Lote L  on L.LoteID = tl.LoteID
	 INNER JOIN AnimalMovimiento AM(NOLOCK) on L.LoteID = AM.LoteID AND AM.Activo = 1
	 INNER JOIN Animal A(NOLOCK) ON A.AnimalID = AM.AnimalID
							AND A.OrganizacionIDEntrada = @OrganizacionID
	 INNER JOIN EntradaGanado EG ON A.FolioEntrada = EG.FolioEntrada
									AND A.OrganizacionIDEntrada = EG.OrganizacionID									
	 group by L.LoteID  

	SET NOCOUNT OFF;
END

GO
