USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerEntradaProductoAyuda]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerEntradaProductoAyuda]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerEntradaProductoAyuda]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 25/08/2014
-- Description: Obtiene todas las entradas de producto
-- 				puede filtrar por las coincidencias del Folio que valla tecleando
-- SpName     : exec EntradaProducto_ObtenerEntradaProductoAyuda 1, 0, 2
--======================================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerEntradaProductoAyuda]
@OrganizacionId INT,
@Folio INT,
@TipoContrato INT
AS 
BEGIN
SELECT 
		EP.EntradaProductoID,
		EP.ContratoID,
		EP.OrganizacionID,
		EP.ProductoID,
		EP.RegistroVigilanciaID,
		EP.Folio,
		C.Folio AS FolioContrato,
		P.Descripcion AS NombreProducto,
		RV.Transportista
	FROM EntradaProducto (NOLOCK) AS EP
	LEFT JOIN Contrato (NOLOCK) AS C ON (EP.ContratoID = C.ContratoID)
	INNER JOIN Producto (NOLOCK) AS P ON (P.ProductoID = EP.ProductoID)
	INNER JOIN RegistroVigilancia AS RV ON (RV.RegistroVigilanciaID = EP.RegistroVigilanciaID)
	WHERE EP.OrganizacionID = @OrganizacionId AND EP.Activo = 1 AND RV.Activo = 1
	AND (CAST (EP.Folio AS VARCHAR) LIKE '%'+CAST (@Folio AS VARCHAR)+'%' OR @Folio = 0)
	AND EP.PesoTara = 0 AND (EP.TipoContratoId <> @TipoContrato OR EP.ContratoID IS NULL)
	AND( (EP.EstatusID = 26 OR EP.EstatusID = 24 OR (EP.EstatusID = 27 AND P.SubFamiliaID = 2 AND dbo.ObtenerCantidadMuestrasPorPedido(EP.EntradaProductoID) > 15))
	AND EP.PesoBruto = 0
	OR (EP.PesoBruto > 0 AND EP.AlmacenInventarioLoteID <> 0 AND EP.FechaInicioDescarga IS NOT NULL AND EP.FechaFinDescarga IS NOT NULL)
	)
END

GO
