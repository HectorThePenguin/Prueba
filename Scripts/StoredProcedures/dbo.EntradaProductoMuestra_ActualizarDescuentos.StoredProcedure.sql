USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProductoMuestra_ActualizarDescuentos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProductoMuestra_ActualizarDescuentos]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProductoMuestra_ActualizarDescuentos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 23/05/2014
-- Description:	Actualiza los descuentos de las muestras
/*EntradaProductoMuestra_ActualizarDescuentos '
	<ROOT>
  <EntradaProductoMuestra>
    <EntradaProductoDetalleID>4</EntradaProductoDetalleID>
    <Descuento>12.34</Descuento>
    <UsuarioModificacionID>5</UsuarioModificacionID>
	<EsOrigen>1<EsOrigen>
  </EntradaProductoMuestra>
</ROOT>
'*/
--======================================================
CREATE PROCEDURE [dbo].[EntradaProductoMuestra_ActualizarDescuentos]
@XMLMuestras XML
AS
BEGIN
	UPDATE EPM
	SET
		EPM.Descuento = TMP.Descuento,
		EPM.FechaModificacion = GETDATE(),
		EPM.UsuarioModificacionID = TMP.UsuarioModificacionID,
		EPM.EsOrigen = TMP.EsOrigen
	FROM EntradaProductoMuestra EPM
	INNER JOIN (
		SELECT 
			EntradaProductoDetalleID = T.item.value('./EntradaProductoDetalleID[1]', 'INT'),
			Descuento  = T.item.value('./Descuento[1]', 'DECIMAL(10,2)'),
			UsuarioModificacionID = T.item.value('./UsuarioModificacionID[1]', 'INT'),
			EsOrigen = T.item.value('./EsOrigen[1]', 'INT')
		FROM  @XMLMuestras.nodes('ROOT/EntradaProductoMuestra') AS T(item)
	) TMP ON (TMP.EntradaProductoDetalleID = EPM.EntradaProductoDetalleID)
END

GO
