USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerEntradaAyuda]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerEntradaAyuda]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerEntradaAyuda]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jose Angel Rodriguez
-- Create date: 25/0/2014
-- Description: Obtiene todas las entradas de producto
-- 				puede filtrar por las coincidencias del organizacon y filtrado por estatus 
--				Autorizado,Aprobado,Pendiente por Autorizar
-- SpName     : exec EntradaProducto_ObtenerEntradaAyuda 
--======================================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerEntradaAyuda]
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
		,PesoBonificacion
	FROM EntradaProducto (NOLOCK)
	WHERE OrganizacionID = @OrganizacionId AND (Activo = @Activo OR @Activo = 2)
	and EstatusID in (24,26,27)
END

GO
