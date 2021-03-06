USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventario_DescontarCierreDiaInventario]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventario_DescontarCierreDiaInventario]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventario_DescontarCierreDiaInventario]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 02/07/2014
-- Description: Obtiene los Productos filtrados por la Familia
-- SpName     : AlmacenInventario_DescontarCierreDiaInventario 80
--======================================================
CREATE PROCEDURE [dbo].[AlmacenInventario_DescontarCierreDiaInventario] 
@AlmacenInventarioXML XML
AS
SET NOCOUNT ON;
	DECLARE @AlmacenInventario AS TABLE (
		AlmacenID INT
		,ProductoID INT
		,DiferenciaCantidad decimal(18,2)		
		,UsuarioModificacionID INT
		,EsEntrada BIT
		)
		INSERT @AlmacenInventario (
		AlmacenID
		,ProductoID
		,DiferenciaCantidad
		,UsuarioModificacionID	
		,EsEntrada	
		)
	SELECT AlmacenID = t.item.value('./AlmacenID[1]', 'INT')
		,ProductoID = t.item.value('./ProductoID[1]', 'INT')
		,DiferenciaCantidad = t.item.value('./DiferenciaCantidad[1]', 'decimal(18,2)')
		,UsuarioModificacionID = t.item.value('./UsuarioModificacionID[1]', 'INT')		
		,EsEntrada = t.item.value('./EsEntrada[1]', 'BIT')		
	FROM @AlmacenInventarioXML.nodes('ROOT/AlmacenInventario') AS T(item)
UPDATE ai
set ai.Cantidad = (ai.Cantidad - ailtemp.DiferenciaCantidad )
,ai.Importe = (ai.Cantidad - ailtemp.DiferenciaCantidad ) * ai.PrecioPromedio
,ai.UsuarioModificacionID = ailtemp.UsuarioModificacionID
,ai.FechaModificacion = GETDATE()
FROM AlmacenInventario ai
INNER JOIN @AlmacenInventario ailtemp on (ai.AlmacenID = ailtemp.AlmacenID AND ai.ProductoID = ailtemp.ProductoID)
where ailtemp.EsEntrada = 0
UPDATE ai
set ai.Cantidad = (ai.Cantidad + ailtemp.DiferenciaCantidad )
,ai.Importe = (ai.Cantidad + ailtemp.DiferenciaCantidad ) * ai.PrecioPromedio
,ai.UsuarioModificacionID = ailtemp.UsuarioModificacionID
,ai.FechaModificacion = GETDATE()
FROM AlmacenInventario ai
INNER JOIN @AlmacenInventario ailtemp on (ai.AlmacenID = ailtemp.AlmacenID AND ai.ProductoID = ailtemp.ProductoID)
where ailtemp.EsEntrada = 1
SET NOCOUNT OFF;

GO
