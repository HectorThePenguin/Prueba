USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerPorContratoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerPorContratoID]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerPorContratoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 15/05/2014
-- Description: Obtiene una lista por ContratoID
-- SpName     : EntradaProducto_ObtenerPorContratoID 1578
--======================================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerPorContratoID]
@ContratoID INT
AS
BEGIN
	SELECT 
		EntradaProductoID,
		ContratoID,
		OrganizacionID,
		pr.ProductoID,
		pr.Descripcion AS Producto,
		sf.SubFamiliaID,
		sf.Descripcion AS Subfamilia,
		RegistroVigilanciaID,
		Folio,
		Fecha,
		FechaDestara,
		Observaciones,
		OperadorIDAnalista,
		PesoBonificacion,
		PesoOrigen,
		PesoBruto,
		PesoTara,
		Piezas,
		TipoContratoID,
		EstatusID,
		Justificacion,
		OperadorIDBascula
		OperadorIDAlmacen,
		OperadorIDAutoriza,
		FechaInicioDescarga,
		FechaFinDescarga,
		AlmacenInventarioLoteID,
		AlmacenMovimientoID,
		ep.Activo,
		ep.FechaCreacion,
		ep.UsuarioCreacionID,
		ep.FechaModificacion,
		ep.UsuarioModificacionID
	FROM EntradaProducto ep
	inner join Producto pr on ep.ProductoID = pr.ProductoID
	inner join SubFamilia sf on pr.SubFamiliaID = sf.SubFamiliaID
	WHERE ContratoID = @ContratoID
	   AND ep.Activo = 1

	   SELECT 
	    epd.EntradaProductoDetalleID
		,epd.EntradaProductoID
		,i.IndicadorID
		,i.Descripcion AS Indicador
	   FROM EntradaProducto ep
	   inner join EntradaProductoDetalle epd on ep.EntradaProductoID = epd.EntradaProductoID
	   inner join Indicador i on epd.IndicadorID = i.IndicadorID
	   WHERE ContratoID = @ContratoID
	   AND ep.Activo = 1

	   SELECT 
	    epm.EntradaProductoMuestraID
		,epm.EntradaProductoDetalleID
		,epm.Porcentaje
		,epm.Descuento
		,epm.Rechazo
	   FROM EntradaProducto ep
	   inner join EntradaProductoDetalle epd on ep.EntradaProductoID = epd.EntradaProductoID
	   inner join EntradaProductoMuestra epm on epd.EntradaProductoDetalleID = epm.EntradaProductoDetalleID
	   WHERE ContratoID = @ContratoID
	   AND ep.Activo = 1
	   

END

GO
