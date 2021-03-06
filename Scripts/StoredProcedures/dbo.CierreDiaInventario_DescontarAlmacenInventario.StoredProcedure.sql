USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CierreDiaInventario_DescontarAlmacenInventario]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CierreDiaInventario_DescontarAlmacenInventario]
GO
/****** Object:  StoredProcedure [dbo].[CierreDiaInventario_DescontarAlmacenInventario]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Gilberto Carranza
-- Create date: 19/08/2014
-- Description:  Descuenta inventario del almacen
-- =============================================
CREATE PROCEDURE [dbo].[CierreDiaInventario_DescontarAlmacenInventario]
@XmlDescontarAlmacenInventario XML
AS
BEGIN
	/* Se crea tabla temporal para almacenar el XML */
	DECLARE @AlmacenMovimientoDetalleTem AS TABLE
	(
		AlmacenID INT,
		ProductoID INT,
		Precio DECIMAL(10,4),
		Cantidad DECIMAL(14,2),
		Importe DECIMAL(17,2),
		AlmacenMovimientoID BIGINT,
		UsuarioCreacionID INT
	)
	UPDATE AI
	SET Cantidad = T.Cantidad
		, Importe = T.Importe
		, UsuarioModificacionID = T.UsuarioCreacionID
		, FechaModificacion = GETDATE()
	FROM AlmacenInventario AI
	INNER JOIN
	(
		SELECT 
			AlmacenID  = T.item.value('./AlmacenID[1]', 'INT'),
			ProductoID    = T.item.value('./ProductoID[1]', 'INT'),
			Precio    = T.item.value('./Precio[1]', 'DECIMAL(10,4)'),
			Cantidad  = T.item.value('./Cantidad[1]', 'DECIMAL(14,2)'),
			Importe   = T.item.value('./Importe[1]', 'DECIMAL(17,2)'),
			AlmacenMovimientoID    = T.item.value('./AlmacenMovimientoID[1]', 'BIGINT'),
			UsuarioCreacionID  = T.item.value('./UsuarioCreacionID[1]', 'INT')
		FROM  @XmlDescontarAlmacenInventario.nodes('ROOT/AlmacenInventario') AS T(item)
	) T ON (AI.AlmacenID = T.AlmacenID
			AND AI.ProductoID = T.ProductoID)
END

GO
