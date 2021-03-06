USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerEntradaProductosParaAyuda]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerEntradaProductosParaAyuda]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerEntradaProductosParaAyuda]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 28/07/2014
-- Description: Obtiene todas las entradas de producto para mostrarlas a la ayuda
-- SpName     : exec EntradaProducto_ObtenerEntradaProductosParaAyuda 1
--======================================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerEntradaProductosParaAyuda]
@OrganizacionId INT
AS 
BEGIN
	SELECT EP.EntradaProductoID, EP.ContratoID, EP.OrganizacionID, EP.ProductoID, EP.RegistroVigilanciaID, EP.Folio,
		EP.Fecha,EP.FechaDestara,EP.Observaciones,EP.OperadorIDAnalista,EP.PesoOrigen,EP.PesoBruto,
		EP.PesoTara,EP.Piezas,EP.TipoContratoID,EP.EstatusID,EP.Justificacion,EP.OperadorIDBascula,
		EP.OperadorIDAlmacen,EP.OperadorIDAutoriza,EP.FechaInicioDescarga, EP.FechaFinDescarga,
		EP.AlmacenInventarioLoteID, EP.AlmacenMovimientoID, EP.Activo,
		EP.FechaCreacion, EP.UsuarioCreacionID, C.Folio AS FolioContrato, P.Descripcion AS DescripcionProducto, PROV.Descripcion AS DescripcionProveedor
	FROM EntradaProducto (NOLOCK) AS EP
	LEFT JOIN Contrato (NOLOCK) AS C ON (C.ContratoID = EP.ContratoID)
	INNER JOIN Producto (NOLOCK) AS P ON (P.ProductoID = EP.ProductoID)
	INNER JOIN RegistroVigilancia (NOLOCK) AS RV ON (RV.RegistroVigilanciaID = EP.RegistroVigilanciaID)
	INNER JOIN Proveedor (NOLOCK) AS PROV ON (PROV.ProveedorID = RV.ProveedorIDMateriasPrimas)
	WHERE EP.OrganizacionID = @OrganizacionId
END

GO
