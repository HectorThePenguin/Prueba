USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerPorFolio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerPorFolio]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerPorFolio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Roque.Solis
-- Create date: 2014/05/26
-- Description: Sp para obtener los folios de las entradas por pagina
-- EXEC EntradaProducto_ObtenerPorFolio 3161, 1,1
--=============================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerPorFolio] 
@Folio INT,
@OrganizacionID INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
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
		EP.AlmacenMovimientoID
	FROM EntradaProducto EP
	WHERE EP.Folio = @Folio 
	AND EP.Activo = @Activo
	AND EP.OrganizacionID = @OrganizacionID
	AND EP.EntradaProductoID NOT IN (SELECT tmpEPC.EntradaProductoID 
									   FROM EntradaProductoCosto AS tmpEPC 
									   WHERE tmpEPC.EntradaProductoID = EP.EntradaProductoID)
	SET NOCOUNT OFF;
END

GO
