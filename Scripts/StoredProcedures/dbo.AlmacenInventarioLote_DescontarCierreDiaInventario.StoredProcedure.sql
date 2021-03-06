USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_DescontarCierreDiaInventario]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventarioLote_DescontarCierreDiaInventario]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_DescontarCierreDiaInventario]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 02/07/2014
-- Description: Obtiene los Productos filtrados por la Familia
-- SpName     : AlmacenInventarioLote_DescontarCierreDiaInventario 80
--======================================================
CREATE PROCEDURE [dbo].[AlmacenInventarioLote_DescontarCierreDiaInventario] @AlmacenInventarioLotesXML XML
AS
SET NOCOUNT ON;
DECLARE @AlmacenInventarioLote AS TABLE (
	AlmacenInventarioLoteID INT
	,DiferenciaCantidad DECIMAL(18, 2)
	,DiferenciaPiezas INT
	,EsEntrada BIT
	,UsuarioModificacionID INT
	)
INSERT @AlmacenInventarioLote (
	AlmacenInventarioLoteID
	,DiferenciaCantidad
	,DiferenciaPiezas
	,EsEntrada
	,UsuarioModificacionID
	)
SELECT AlmacenInventarioLoteID = t.item.value('./AlmacenInventarioLoteID[1]', 'INT')
	,DiferenciaCantidad = t.item.value('./DiferenciaCantidad[1]', 'decimal(18,2)')
	,DiferenciaPiezas = t.item.value('./DiferenciaPiezas[1]', 'INT')
	,EsEntrada = t.item.value('./EsEntrada[1]', 'BIT')
	,UsuarioModificacionID = t.item.value('./UsuarioModificacionID[1]', 'INT')
FROM @AlmacenInventarioLotesXML.nodes('ROOT/AlmacenInventarioLote') AS T(item)
UPDATE ail
SET ail.Cantidad = (ail.Cantidad - ailtemp.DiferenciaCantidad)
	,ail.Importe = (ail.Cantidad - ailtemp.DiferenciaCantidad) * ail.PrecioPromedio
	,ail.Piezas = (ail.Piezas - ailtemp.DiferenciaPiezas)
	,ail.UsuarioModificacionID = ailtemp.UsuarioModificacionID
	,ail.FechaModificacion = GETDATE()
FROM AlmacenInventarioLote ail
INNER JOIN @AlmacenInventarioLote ailtemp ON ail.AlmacenInventarioLoteID = ailtemp.AlmacenInventarioLoteID
where ailtemp.EsEntrada = 0
UPDATE ail
SET ail.Cantidad = (ail.Cantidad + ailtemp.DiferenciaCantidad)
	,ail.Importe = (ail.Cantidad + ailtemp.DiferenciaCantidad) * ail.PrecioPromedio
	,ail.Piezas = (ail.Piezas + ailtemp.DiferenciaPiezas)
	,ail.UsuarioModificacionID = ailtemp.UsuarioModificacionID
	,ail.FechaModificacion = GETDATE()
FROM AlmacenInventarioLote ail
INNER JOIN @AlmacenInventarioLote ailtemp ON ail.AlmacenInventarioLoteID = ailtemp.AlmacenInventarioLoteID
where ailtemp.EsEntrada = 1
UPDATE ail
SET ail.FechaFin = GETDATE()
	,ail.Activo = 0	
FROM AlmacenInventarioLote ail
INNER JOIN @AlmacenInventarioLote ailtemp ON ail.AlmacenInventarioLoteID = ailtemp.AlmacenInventarioLoteID
WHERE ailtemp.EsEntrada = 0
and AIL.Cantidad = 0
SET NOCOUNT OFF;

GO
