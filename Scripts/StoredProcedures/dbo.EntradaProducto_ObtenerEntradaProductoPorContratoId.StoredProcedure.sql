USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerEntradaProductoPorContratoId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerEntradaProductoPorContratoId]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerEntradaProductoPorContratoId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 22/08/2014
-- Description: Obtiene todas las entradas de producto
-- 				puede filtrar por EstatusID
-- SpName     : exec EntradaProducto_ObtenerEntradaProductoPorContratoId 1
--======================================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerEntradaProductoPorContratoId]
@ContratoID INT,
@OrganizacionID INT,
@ProductoID INT,
@Activo INT
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
	WHERE ContratoID = @ContratoID
	AND OrganizacionID = @OrganizacionID
	AND ProductoID = @ProductoID
	AND Activo = @Activo
END

GO
