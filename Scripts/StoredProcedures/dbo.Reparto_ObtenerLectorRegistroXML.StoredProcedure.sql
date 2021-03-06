USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerLectorRegistroXML]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerLectorRegistroXML]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerLectorRegistroXML]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Jorge Luis Velazquez Araujo
-- Create date: 28/01/2015
-- Description:	Obtiene un lector registro
-- EXEC Reparto_ObtenerLectorRegistroXML 1, 4, 1, '1900-01-01'
--=============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerLectorRegistroXML]
	@XmlLote XML,
	@OrganizacionID INT,
	@Activo INT,
	@Fecha DATE
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @FechaLector DATE
	set @FechaLector = @Fecha
	CREATE TABLE #tLote
	(
		LoteID INT
	)
	INSERT INTO #tLote
	SELECT
		t.item.value('./LoteID[1]', 'INT') AS LoteID
	FROM @XmlLote.nodes('ROOT/Lotes') AS T (item)
	SELECT 
		L.LectorRegistroID,
        L.OrganizacionID,
		L.Seccion,
        L.LoteID,
        L.Fecha,
        L.CambioFormula,
        L.Cabezas,
        L.EstadoComederoID,
        CAST(ROUND(L.CantidadOriginal * L.Cabezas, 0) AS decimal) AS CantidadOriginal,
        CAST(ROUND(L.CantidadPedido * L.Cabezas, 0) AS decimal) AS CantidadPedido
	FROM LectorRegistro L
  INNER JOIN LectorRegistroDetalle LRD ON L.LectorRegistroID = LRD.LectorRegistroID
  INNER JOIN #tLote lt ON L.LoteID = lt.LoteID
	WHERE L.OrganizacionID = @OrganizacionID
	AND L.Activo = @Activo
	AND CAST(L.Fecha AS DATE) = @FechaLector
	SELECT 
	LRD.LectorRegistroDetalleID,
        LRD.LectorRegistroID,
        LRD.TipoServicioID,
        LRD.FormulaIDAnterior,
        LRD.FormulaIDProgramada
	FROM LectorRegistro L
  INNER JOIN LectorRegistroDetalle LRD ON L.LectorRegistroID = LRD.LectorRegistroID
  INNER JOIN #tLote lt ON L.LoteID = lt.LoteID
	WHERE L.OrganizacionID = @OrganizacionID
	AND L.Activo = @Activo
	AND CAST(L.Fecha AS DATE) = @FechaLector
	SET NOCOUNT OFF;
END

GO
