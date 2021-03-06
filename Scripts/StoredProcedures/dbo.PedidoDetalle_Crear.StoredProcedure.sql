USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PedidoDetalle_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PedidoDetalle_Crear]
GO
/****** Object:  StoredProcedure [dbo].[PedidoDetalle_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Pedro Delgado
-- Create date: 29/06/2014
-- Description:  Guardar el pedidodetalle
-- Origen: APInterfaces
/*
PedidoDetalle_Crear '<ROOT>
  <PedidoDetalle>
    <PedidoID>7</PedidoID>
    <ProductoID>82</ProductoID>
    <CantidadSolicitada>1</CantidadSolicitada>
    <AlmacenInventarioLoteID>2</AlmacenInventarioLoteID>
    <Observaciones />
    <UsuarioCreacionID>1</UsuarioCreacionID>
  </PedidoDetalle>
  <PedidoDetalle>
    <PedidoID>7</PedidoID>
    <ProductoID>83</ProductoID>
    <CantidadSolicitada>1</CantidadSolicitada>
    <AlmacenInventarioLoteID>2</AlmacenInventarioLoteID>
    <Observaciones />
    <UsuarioCreacionID>1</UsuarioCreacionID>
  </PedidoDetalle>
</ROOT>'
*/
-- =============================================
CREATE PROCEDURE [dbo].[PedidoDetalle_Crear]
@XMLPedidoDetalle XML
AS
BEGIN
	INSERT INTO PedidoDetalle 
	(PedidoID, ProductoID,CantidadSolicitada,InventarioLoteIDDestino,FechaCreacion,UsuarioCreacionID,Activo)
	SELECT 
			PedidoID  = T.item.value('./PedidoID[1]', 'INT'),
			ProductoID  = T.item.value('./ProductoID[1]', 'INT'),
			CantidadSolicitada    = T.item.value('./CantidadSolicitada[1]', 'DECIMAL(14,2)'),
			AlmacenInventarioLoteID  = T.item.value('./AlmacenInventarioLoteID[1]', 'INT'),
			FechaCreacion = GETDATE(),
			UsuarioCreacionID = T.item.value('./UsuarioCreacionID[1]', 'INT'),
			Activo  = 1
	FROM  @XMLPedidoDetalle.nodes('ROOT/PedidoDetalle') AS T(item)
END

GO
