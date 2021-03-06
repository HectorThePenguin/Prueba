USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProductoDetalle_GuardarDetalle]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SolicitudProductoDetalle_GuardarDetalle]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProductoDetalle_GuardarDetalle]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 31/07/2014
-- Description: Agrega los detalles 
-- SpName     : SolicitudProductoDetalle_GuardarDetalle
--======================================================
CREATE PROCEDURE [dbo].[SolicitudProductoDetalle_GuardarDetalle] @SolicitudProductoDetalleXML XML
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #SolicitudProductoDetalle (
		SolicitudProductoDetalleID INT
		,SolicitudProductoID INT
		,ProductoID INT
		,Cantidad DECIMAL(18,2)
		,CamionRepartoID INT
		,EstatusID INT
		,Activo BIT
		,UsuarioCreacionID INT
		,UsuarioModificacionID INT
		)
	INSERT #SolicitudProductoDetalle (
		SolicitudProductoDetalleID
		,SolicitudProductoID
		,ProductoID
		,Cantidad
		,CamionRepartoID
		,EstatusID
		,Activo
		,UsuarioCreacionID
		,UsuarioModificacionID
		)
	SELECT SolicitudProductoDetalleID = t.item.value('./SolicitudProductoDetalleID[1]', 'INT')
		,SolicitudProductoID = t.item.value('./SolicitudProductoID[1]', 'INT')
		,ProductoID = t.item.value('./ProductoID[1]', 'INT')
		,Cantidad = t.item.value('./Cantidad[1]', 'decimal(18,2)')
		,CamionRepartoID = t.item.value('./CamionRepartoID[1]', 'INT')
		,EstatusID = t.item.value('./EstatusID[1]', 'INT')
		,Activo = t.item.value('./Activo[1]', 'BIT')
		,UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'INT')
		,UsuarioModificacionID = t.item.value('./UsuarioModificacionID[1]', 'INT')
	FROM @SolicitudProductoDetalleXML.nodes('ROOT/SolicitudProductoDetalle') AS T(item)
	update #SolicitudProductoDetalle set CamionRepartoID = null
	where CamionRepartoID = 0
	UPDATE ai
	SET ai.ProductoID = ad.ProductoID
		,ai.Cantidad = ad.Cantidad
		,ai.CamionRepartoID = ad.CamionRepartoID
		,ai.EstatusID = ad.EstatusID
		,ai.Activo = ad.Activo
		,ai.FechaModificacion = GETDATE()
		,ai.UsuarioModificacionID = ad.UsuarioModificacionID
	FROM SolicitudProductoDetalle ai
	INNER JOIN #SolicitudProductoDetalle ad ON (ai.SolicitudProductoDetalleID = ad.SolicitudProductoDetalleID)
	/* Cambio por Cesar Vega: Insertar producto en almacen destino cuando este no existe */
	INSERT SolicitudProductoDetalle (
		SolicitudProductoID
		,ProductoID
		,Cantidad
		,CamionRepartoID
		,EstatusID
		,Activo
		,FechaCreacion
		,UsuarioCreacionID
		)
	SELECT SolicitudProductoID
		,ProductoID
		,Cantidad
		,CamionRepartoID
		,EstatusID
		,Activo
		,getdate()
		,UsuarioCreacionID
	FROM #SolicitudProductoDetalle aim
	WHERE aim.SolicitudProductoDetalleID = 0
	SET NOCOUNT OFF;
	DROP TABLE #SolicitudProductoDetalle
END

GO
