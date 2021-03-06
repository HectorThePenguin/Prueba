USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerEntradaPorFolio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerEntradaPorFolio]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerEntradaPorFolio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 28/07/2014
-- Description: Obtiene todas las entradas de producto
-- 				puede filtrar por las coincidencias del Folio que valla tecleando
-- SpName     : exec EntradaProducto_ObtenerEntradaPorFolio 1, 1781
--======================================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerEntradaPorFolio]
@OrganizacionId INT,
@Folio INT,
@Activo INT = 2
AS 
BEGIN
	SELECT 
		EntradaProductoID,
		ContratoID,
		OrganizacionID,
		ProductoID,
		RegistroVigilanciaID,
		Folio,
		Fecha,
		FechaDestara,
		Observaciones,
		OperadorIDAnalista,
		PesoOrigen,
		PesoBonificacion,
		PesoBruto,
		PesoTara,
		Piezas,
		TipoContratoID,
		EstatusID,
		Justificacion,
		OperadorIDBascula,
		OperadorIDAlmacen,
		OperadorIDAutoriza,
		FechaInicioDescarga,
		FechaFinDescarga,
		AlmacenInventarioLoteID,
		AlmacenMovimientoID,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID,
		AlmacenMovimientoSalidaID,
		FolioOrigen,
		FechaEmbarque
		, CAST(PesoDescuento AS DECIMAL(18,2)) AS PesoDescuento,
		NotaVenta
	FROM EntradaProducto (NOLOCK)
	WHERE OrganizacionID = @OrganizacionId AND (Activo = @Activo OR @Activo = 2)
	AND Folio = @Folio
END
GO
