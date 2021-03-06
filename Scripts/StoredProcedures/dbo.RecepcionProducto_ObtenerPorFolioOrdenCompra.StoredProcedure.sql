USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RecepcionProducto_ObtenerPorFolioOrdenCompra]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RecepcionProducto_ObtenerPorFolioOrdenCompra]
GO
/****** Object:  StoredProcedure [dbo].[RecepcionProducto_ObtenerPorFolioOrdenCompra]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pedro Delgado>
-- Create date: <23/07/2014>
-- Description:	Obtiene la recepcion de producto por folio de orden de compra.
/*RecepcionProducto_ObtenerPorFolioOrdenCompra */
-- =============================================
CREATE PROCEDURE [dbo].[RecepcionProducto_ObtenerPorFolioOrdenCompra]
@FolioOrdenCompra VARCHAR(6),
@Activo BIT
AS
BEGIN
	SELECT 
		RecepcionProductoID,
		AlmacenID,
		FolioRecepcion,
		FolioOrdenCompra,
		FechaSolicitud,
		ProveedorID,
		FechaRecepcion,
		AlmacenMovimientoID,
		Factura,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
	FROM RecepcionProducto (NOLOCK)
	WHERE FolioOrdenCompra = @FolioOrdenCompra 
			AND Activo = @Activo
END

GO
