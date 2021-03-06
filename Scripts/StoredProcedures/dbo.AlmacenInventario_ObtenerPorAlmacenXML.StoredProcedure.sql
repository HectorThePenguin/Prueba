USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventario_ObtenerPorAlmacenXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventario_ObtenerPorAlmacenXML]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventario_ObtenerPorAlmacenXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Jorge Luis Velazquez Araujo
-- Create date: 03/02/2015
-- Description:	Obtiene los inventarios de los almacenes en un XML
-- EXEC AlmacenInventario_ObtenerPorAlmacenXML 
--=============================================
CREATE PROCEDURE [dbo].[AlmacenInventario_ObtenerPorAlmacenXML]
	@XmlAlmacen XML	
AS
BEGIN
SET NOCOUNT ON;
CREATE TABLE #tAlmacen(AlmacenID INT)
INSERT INTO #tAlmacen
	SELECT
		t.item.value('./AlmacenID[1]', 'INT') AS AlmacenID
	FROM @XmlAlmacen.nodes('ROOT/Almacenes') AS T (item)
SELECT
	ai.AlmacenInventarioID,
	ai.AlmacenID,
	ai.ProductoID,
	ai.Minimo,
	ai.Maximo,
	ai.PrecioPromedio,
	ai.Cantidad,
	ai.Importe,
	ai.DiasReorden,
	ai.CapacidadAlmacenaje
FROM AlmacenInventario ai 
inner join #tAlmacen al on ai.AlmacenID = al.AlmacenID
INNER JOIN Almacen a on al.AlmacenID = a.AlmacenID
where a.Activo = 1
SET NOCOUNT OFF;
END

GO
