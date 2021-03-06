USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerEntradaEstatus]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerEntradaEstatus]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerEntradaEstatus]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 14/05/2014
-- Description: Obtiene todas las entradas de producto
-- 				puede filtrar por EstatusID
-- SpName     : exec EntradaProducto_ObtenerEntradaEstatus 26,1
--======================================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerEntradaEstatus]
@EstatusID INT,
@OrganizacionID INT
AS 
BEGIN
	SELECT 
		EP.EntradaProductoID,
		EP.ContratoID,
		EP.OrganizacionID,
		EP.ProductoID,
		EP.RegistroVigilanciaID,
		EP.Folio,
		EP.Fecha,
		EP.FechaDestara,
		EP.Observaciones,
		EP.OperadorIDAnalista,
		EP.PesoOrigen,
		EP.PesoBruto,
		EP.PesoTara,
		EP.Piezas,
		EP.TipoContratoID,
		EP.EstatusID,
		EP.Justificacion,
		EP.OperadorIDBascula,
		EP.OperadorIDAlmacen,
		EP.OperadorIDAutoriza,
		EP.FechaInicioDescarga,
		EP.FechaFinDescarga,
		EP.AlmacenInventarioLoteID,
		EP.AlmacenMovimientoID,
		EP.Activo,
		EP.FechaCreacion,
		EP.UsuarioCreacionID,
		EP.FechaModificacion,
		EP.UsuarioModificacionID,
		E.Descripcion
	FROM EntradaProducto EP (NOLOCK)
	INNER JOIN Estatus (NOLOCK) E ON E.EstatusID = EP.EstatusID
	WHERE EP.EstatusID = @EstatusID AND EP.Activo = 1
	AND EP.OrganizacionID = @OrganizacionId
END

GO
