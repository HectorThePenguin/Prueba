USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerTodosPorFiltroFolio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerTodosPorFiltroFolio]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerTodosPorFiltroFolio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 14/05/2014
-- Description: Obtiene todas las entradas de producto
-- 				puede filtrar por las coincidencias del Folio que valla tecleando
-- SpName     : exec EntradaProducto_ObtenerTodosPorFiltroFolio 1, 12345
--======================================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerTodosPorFiltroFolio]
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
	AND (CAST (Folio AS VARCHAR) LIKE '%'+CAST (@Folio AS VARCHAR)+'%' OR @Folio = 0)
END

GO
