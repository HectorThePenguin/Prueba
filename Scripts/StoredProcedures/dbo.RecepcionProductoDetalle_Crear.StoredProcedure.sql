USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RecepcionProductoDetalle_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RecepcionProductoDetalle_Crear]
GO
/****** Object:  StoredProcedure [dbo].[RecepcionProductoDetalle_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 20/02/2014
-- Description:	Guarda el detalle de una recepcion
/*RecepcionProductoDetalle_Crear	'
	<ROOT>
		<RecepcionProductoDetalle>
			<RecepcionProductoID>1</RecepcionProductoID>
			<ProductoID>32</ProductoID>
			<Cantidad>123</Cantidad>
			<PrecioPromedio>12.3</PrecioPromedio>
			<Importe>12.3</Importe>
			<Activo>1</Activo>
			<UsuarioCreacionID>1</UsuarioCreacionID>
		</RecepcionProductoDetalle>
		<RecepcionProductoDetalle>
			<RecepcionProductoID>1</RecepcionProductoID>
			<ProductoID>23</ProductoID>
			<Cantidad>1</Cantidad>
			<PrecioPromedio>2</PrecioPromedio>
			<Importe>2</Importe>
			<Activo>1</Activo>
			<UsuarioCreacionID>1</UsuarioCreacionID>
		</RecepcionProductoDetalle>
	</ROOT>
'*/
--======================================================
CREATE PROCEDURE [dbo].[RecepcionProductoDetalle_Crear]
@XMLRecepcionProductoDetalle XML
AS 
BEGIN
	INSERT INTO RecepcionProductoDetalle
	(RecepcionProductoID,ProductoID,Cantidad,PrecioPromedio,Importe,Activo,FechaCreacion,UsuarioCreacionID)
	SELECT 
		RecepcionProductoID  = T.item.value('./RecepcionProductoID[1]', 'INT'),
		ProductoID  = T.item.value('./ProductoID[1]', 'INT'),
		Cantidad    = T.item.value('./Cantidad[1]', 'DECIMAL(18,2)'),
		PrecioPromedio    = T.item.value('./PrecioPromedio[1]', 'DECIMAL(18,4)'),
		Importe  = T.item.value('./Importe[1]', 'DECIMAL(24,2)'),
		Activo  = T.item.value('./Activo[1]', 'BIT'),
		FechaCreacion = GETDATE(),
		UsuarioCreacionID = T.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM  @XMLRecepcionProductoDetalle.nodes('ROOT/RecepcionProductoDetalle') AS T(item)
END

GO
