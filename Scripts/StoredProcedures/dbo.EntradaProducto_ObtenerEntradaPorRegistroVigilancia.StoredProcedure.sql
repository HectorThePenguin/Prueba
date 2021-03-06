USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerEntradaPorRegistroVigilancia]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerEntradaPorRegistroVigilancia]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerEntradaPorRegistroVigilancia]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jose Angel Rodriguez
-- Create date: 23/08/2014
-- Description: Obtiene todas las entradas de producto
-- 				puede filtrar por las coincidencias del RegistroVigilancia
-- SpName     : exec EntradaProducto_ObtenerEntradaPorRegistroVigilancia 1, 1781,1
--======================================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerEntradaPorRegistroVigilancia]
@OrganizacionId INT,
@Folio INT,
@Activo INT = 2
AS 

BEGIN

	SELECT distinct
		ep.EntradaProductoID,
		ep.ContratoID,
		ep.OrganizacionID,
		ep.ProductoID,
		ep.RegistroVigilanciaID,
		ep.Folio,
		ep.Fecha,
		ep.FechaDestara,
		ep.Observaciones,
		ep.OperadorIDAnalista,
		ep.PesoOrigen,
		ep.PesoBruto,
		ep.PesoTara,
		ep.Piezas,
		ep.TipoContratoID,
		ep.EstatusID,
		ep.Justificacion,
		ep.OperadorIDBascula,
		ep.OperadorIDAlmacen,
		ep.OperadorIDAutoriza,
		ep.FechaInicioDescarga,
		ep.FechaFinDescarga,
		ep.AlmacenInventarioLoteID,
		ep.AlmacenMovimientoID,
		ep.Activo,
		ep.FechaCreacion,
		ep.UsuarioCreacionID,
		ep.FechaModificacion,
		ep.UsuarioModificacionID,
		ep.PesoBonificacion,
		ep.AlmacenMovimientoSalidaID,
		ep.FolioOrigen,
		ep.FechaEmbarque
		, CAST(EP.PesoDescuento AS DECIMAL(18,2)) AS PesoDescuento
	FROM EntradaProducto ep (NOLOCK)
	INNER JOIN RegistroVigilancia rv on ep.RegistroVigilanciaID = rv.RegistroVigilanciaID
	WHERE ep.OrganizacionID = @OrganizacionId AND (ep.Activo = @Activo OR @Activo = 2)
	AND rv.RegistroVigilanciaID = @Folio

END

GO
