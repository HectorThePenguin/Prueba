USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerEntradaProductoPorId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerEntradaProductoPorId]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerEntradaProductoPorId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 28/07/2014
-- Description: Obtiene la entrada de producto por su identificados
-- SpName     : exec EntradaProducto_ObtenerEntradaProductoPorId 1781
--======================================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerEntradaProductoPorId]
@EntradaProductoId INT
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
		PesoBonificacion,
		PesoOrigen,
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
		, CAST(PesoDescuento AS DECIMAL(18,2)) AS PesoDescuento
	FROM EntradaProducto (NOLOCK)
	WHERE Activo = 1 AND EntradaProductoID = @EntradaProductoId
END
GO
