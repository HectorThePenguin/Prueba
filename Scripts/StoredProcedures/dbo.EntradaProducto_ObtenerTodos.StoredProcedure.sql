USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 14/05/2014
-- Description: Obtiene todas las entradas de producto
-- 				puede filtrar por EstatusID
-- SpName     : exec EntradaProducto_ObtenerTodos 1
--======================================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerTodos]
@OrganizacionId INT,
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
		UsuarioModificacionID
	FROM EntradaProducto (NOLOCK)
	WHERE OrganizacionID = @OrganizacionId AND (Activo = @Activo OR @Activo = 2)
END

GO
