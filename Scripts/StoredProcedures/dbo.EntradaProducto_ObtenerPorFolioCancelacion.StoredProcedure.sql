USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerPorFolioCancelacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerPorFolioCancelacion]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerPorFolioCancelacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Pedro Delgado
-- Create date: 2014/11/27
-- Description: Sp para obtener los folios de las entradas por pagina
-- EXEC EntradaProducto_ObtenerPorFolio 1234, 4,1
--=============================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerPorFolioCancelacion] 
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
	SET NOCOUNT OFF;
END

GO
