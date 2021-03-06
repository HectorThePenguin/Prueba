USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerFolioPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerFolioPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerFolioPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Roque.Solis
-- Create date: 2014/05/26
-- Description: Sp para obtener los folios de las entradas por pagina
-- EntradaProducto_ObtenerFolioPorPagina 1, 4,26, 1, 1, 20
--=============================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerFolioPorPagina] 
@Folio INT,
@OrganizacionID INT,
@DescripcionProducto VARCHAR(50),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ROW_NUMBER() OVER (
			ORDER BY EP.Folio ASC
			) AS RowNum, 
		EP.EntradaProductoID,
		EP.ContratoID,
		EP.OrganizacionID,
		EP.ProductoID,
		P.Descripcion AS ProductoDescripcion,
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
	INTO #DatosEntradaProducto
	FROM EntradaProducto (NOLOCK) EP
	INNER JOIN Producto (NOLOCK) P ON (EP.ProductoID = P.ProductoID) 
	LEFT JOIN Contrato (NOLOCK) C ON (EP.ContratoID = C.ContratoID)
	WHERE (@Folio = 0 OR EP.Folio LIKE '%' + CAST(@Folio AS VARCHAR(15)) + '%')
	AND (@DescripcionProducto = '' OR P.Descripcion LIKE '%' + CAST(@DescripcionProducto AS VARCHAR(50)) + '%')
	AND EP.Activo = @Activo
	AND EP.OrganizacionID = @OrganizacionID
	AND ((EP.PesoOrigen>0 AND C.PesoNegociar = 'Origen') OR (EP.PesoOrigen>=0 AND C.PesoNegociar = 'Destino')  OR EP.ContratoID IS NULL)
	AND	EP.PesoBruto>0
	AND	EP.PesoTara>0
	AND EP.AlmacenMovimientoID IS NULL
	AND EP.EntradaProductoID NOT IN (SELECT tmpEPC.EntradaProductoID 
									   FROM EntradaProductoCosto AS tmpEPC 
									   WHERE tmpEPC.EntradaProductoID = EP.EntradaProductoID)
	SELECT EntradaProductoID,
		   ContratoID,
		   OrganizacionID,
		   ProductoID,
		   ProductoDescripcion,
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
		   AlmacenMovimientoID
	FROM #DatosEntradaProducto
	WHERE RowNum BETWEEN @Inicio
	AND @Limite
	SELECT COUNT(Folio) AS TotalReg
	FROM #DatosEntradaProducto
	DROP TABLE #DatosEntradaProducto
	SET NOCOUNT OFF;
END

GO
