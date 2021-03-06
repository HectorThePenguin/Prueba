USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProductoMuestra_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProductoMuestra_Crear]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProductoMuestra_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Octavio Quintero>
-- Create date: <02/06/2014>
-- Description:	Guarda las muestras capturadas en la entrada del producto.
/*EntradaProductoMuestra_Crear '
	<ROOT>
		<EntradaProductoMuestra>
			<EntradaProductoDetalleID></EntradaProductoDetalleID>
			<Porcentaje>12</Porcentaje>
			<Descuento>13</Descuento>
			<Rechazo></Rechazo>
			<UsuarioCreacionID></UsuarioCreacionID>
		</EntradaProductoMuestra>
	</ROOT>
'*/
-- =============================================
CREATE PROCEDURE [dbo].[EntradaProductoMuestra_Crear]
	@XMLMuestras XML 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		ROW_NUMBER () OVER (
			ORDER BY T.item.value('./EntradaProductoDetalleID[1]', 'INT') ASC
			) AS RowNum,
		ProductoDetalleID = T.item.value('./EntradaProductoDetalleID[1]', 'INT'),
		UsuarioCreacionID   = T.item.value('./UsuarioCreacionID[1]','INT'),
		Porcentaje    = T.item.value('./Porcentaje[1]', 'DECIMAL(10,2)'),
		Descuento  = T.item.value('./Descuento[1]', 'DECIMAL(10,2)'),
		Rechazo = T.item.value('./Rechazo[1]', 'INT'),
		esOrigen = T.item.value('./esOrigen[1]', 'INT')
	INTO #TmpMuestras
	FROM  @XMLMuestras.nodes('ROOT/EntradaProductoMuestra') AS T(item)
	
  INSERT INTO EntradaProductoMuestra (EntradaProductoDetalleID,Porcentaje,
	Descuento,Rechazo,Activo,FechaCreacion,UsuarioCreacionID,EsOrigen)
	SELECT 
		TOP 1
		ProductoDetalleID,
		Porcentaje,
		Descuento,
		Rechazo,
		1,
		GETDATE(),
		UsuarioCreacionID,
		esOrigen
	FROM  #TmpMuestras
	 
	INSERT INTO EntradaProductoMuestra (EntradaProductoDetalleID,Porcentaje,
	Descuento,Rechazo,Activo,FechaCreacion,UsuarioCreacionID,EsOrigen)
	SELECT 
		ProductoDetalleID,
		Porcentaje,
		0,
		Rechazo,
		1,
		GETDATE(),
		UsuarioCreacionID,
		EsOrigen
	FROM  #TmpMuestras
	WHERE RowNum  > 1

END

GO
