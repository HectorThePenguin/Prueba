USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenInventarioXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenInventarioXML]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenInventarioXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Jorge Luis Velazquez Araujo
-- Create date: 03/02/2015
-- Description:	Obtiene los inventarios lotes de los almacenes en un XML
-- EXEC AlmacenInventarioLote_ObtenerPorAlmacenInventarioXML 
--=============================================
CREATE PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenInventarioXML]
	@XmlAlmacenInventario XML	
AS
BEGIN
SET NOCOUNT ON;
CREATE TABLE #tAlmacenInventario
(
	AlmacenInventarioID INT
)
INSERT INTO #tAlmacenInventario
	SELECT
		t.item.value('./AlmacenInventarioID[1]', 'INT') AS AlmacenInventarioID
	FROM @XmlAlmacenInventario.nodes('ROOT/AlmacenesInventario') AS T (item)
SELECT
	ail.AlmacenInventarioLoteID,
	ail.AlmacenInventarioID,
	ail.Lote,
	ail.Cantidad,
	ail.PrecioPromedio,
	ail.Piezas,
	ail.Importe,
	ail.FechaInicio,
	ail.FechaFin,
	ail.Activo
FROM AlmacenInventarioLote ail
INNER JOIN #tAlmacenInventario ai
	ON ail.AlmacenInventarioID = ai.AlmacenInventarioID
SET NOCOUNT OFF;
END

GO
