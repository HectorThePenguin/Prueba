USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_ObtenerFechaUltimoMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalMovimiento_ObtenerFechaUltimoMovimiento]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_ObtenerFechaUltimoMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 09/01/2014
-- Description: Sp para obtener las fechas de cierre de los lotes en base a su ultimo animal en entrar
-- AnimalMovimiento_ObtenerFechaUltimoMovimiento
--=============================================
CREATE PROCEDURE [dbo].[AnimalMovimiento_ObtenerFechaUltimoMovimiento] @OrganizacionID INT
	,@XmlLotes XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Lotes AS TABLE ([LoteID] INT)

	INSERT @Lotes ([LoteID])
	SELECT [LoteID] = t.item.value('./LoteID[1]', 'INT')
	FROM @XmlLotes.nodes('ROOT/Lotes') AS t(item)
	
	SELECT
	am.LoteID
	,MAX(am.FechaMovimiento) AS [FechaMovimiento]	
	FROM AnimalMovimiento am(NOLOCK)
	INNER JOIN @Lotes lo ON am.LoteID = lo.LoteID
	WHERE am.OrganizacionID = @OrganizacionID
	AND am.Activo = 1
	GROUP BY am.LoteID

	SET NOCOUNT OFF;
END
GO
