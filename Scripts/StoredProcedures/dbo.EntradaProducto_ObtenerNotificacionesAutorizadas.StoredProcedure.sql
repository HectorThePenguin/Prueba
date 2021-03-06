USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerNotificacionesAutorizadas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerNotificacionesAutorizadas]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerNotificacionesAutorizadas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gilberto Carranza
-- Create date: 05-12-2014
-- Description:	Actualiza las cabezas en lote
-- EntradaProducto_ObtenerNotificacionesAutorizadas 1
-- =============================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerNotificacionesAutorizadas]
@OrganizacionID INT 
AS
BEGIN	

	SET NOCOUNT ON

		DECLARE @EstatusAutorizada INT, @TipoEstatusEntradaProducto INT

		SET @EstatusAutorizada = 24
		SET @TipoEstatusEntradaProducto = 7

		SELECT EP.Folio
			,  EP.Fecha
			,  P.ProductoID
			,  P.Descripcion		AS Producto
			,  Prov.ProveedorID
			,  Prov.Descripcion		AS Proveedor
			,  EP.EntradaProductoID
		FROM EntradaProducto EP
		INNER JOIN Estatus E
			ON (EP.EstatusID = E.EstatusID
				AND E.EstatusID = @EstatusAutorizada
				AND E.TipoEstatus = @TipoEstatusEntradaProducto
				AND EP.OperadorIDAutoriza > 0)
		INNER JOIN Producto P
			ON (EP.ProductoID = P.ProductoID)
		INNER JOIN Contrato C
			ON (EP.ContratoID = C.ContratoID)
		INNER JOIN Proveedor Prov
			ON (C.ProveedorID = Prov.ProveedorID)
		WHERE EP.OrganizacionID = @OrganizacionID
			AND EP.Revisado = 0

	SET NOCOUNT OFF
END

GO
