USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerCancelacionPorDescripcionProducto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerCancelacionPorDescripcionProducto]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerCancelacionPorDescripcionProducto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Pedro Delgado
-- Create date: 2014/11/26
-- Description: Procedimiento para obtener los folios a cancelar
/*EntradaProducto_ObtenerCancelacionPorDescripcionProducto 
	'',20,
	'<ROOT><Familia><FamiliaID>1</FamiliaID></Familia></ROOT>',
	'<ROOT><SubFamilia><SubFamiliaID>24</SubFamiliaID></SubFamilia></ROOT>',1,1,15
*/
--=============================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerCancelacionPorDescripcionProducto]
@Descripcion VARCHAR(50),
@OrganizacionID INT,
@Fecha DATE,
@TipoMovimientoID INT,
@XMLFamilias XML,
@XMLSubfamilias XML,
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ROW_NUMBER() OVER (
				ORDER BY EP.EntradaProductoID ASC
				) AS RowNum,
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
		EP.PesoBonificacion,
		EP.PesoOrigen,
		EP.PesoBruto,
		EP.PesoTara,
		EP.PesoDescuento,
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
		EP.AlmacenMovimientoID,
		EP.Activo,
		EP.FechaCreacion,
		EP.UsuarioCreacionID,
		EP.FechaModificacion,
		EP.UsuarioModificacionID,
		SF.FamiliaID,
		P.Descripcion AS ProductoDescripcion,
		AM.TipoMovimientoID,
		AM.AlmacenID,
		AMD.Cantidad,
		AMD.Importe
	INTO #EntradaProducto
	FROM EntradaProducto (NOLOCK) EP
	INNER JOIN AlmacenInventarioLote (NOLOCK) AML ON (EP.AlmacenInventarioLoteID = AML.AlmacenInventarioLoteID)
	INNER JOIN AlmacenMovimiento (NOLOCK) AM ON (EP.AlmacenMovimientoID = AM.AlmacenMovimientoID)
	INNER JOIN AlmacenMovimientoDetalle (NOLOCK) AMD ON (AM.AlmacenMovimientoID = AMD.AlmacenMovimientoID)
	INNER JOIN Producto (NOLOCK) P ON (P.ProductoID = EP.ProductoID)
	INNER JOIN SubFamilia (NOLOCK) SF ON (SF.SubFamiliaID = P.SubFamiliaID)
	WHERE 
		AM.TipoMovimientoID = @TipoMovimientoID
		AND (P.Descripcion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')
		AND (SF.FamiliaID IN (SELECT FamiliaID = T.item.value('./FamiliaID[1]', 'INT')
													FROM  @XMLFamilias.nodes('ROOT/Familia') AS T(item))
				 OR SF.SubFamiliaID IN (SELECT SubFamiliaID = T.item.value('./SubFamiliaID[1]', 'INT')
													FROM  @XMLSubFamilias.nodes('ROOT/SubFamilia') AS T(item)))
		AND EP.OrganizacionID = @OrganizacionID
		AND EP.Activo = 1
		AND AML.Activo = 1
		AND CAST(EP.Fecha AS DATE) >= CAST(@Fecha  AS DATE)
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
		PesoDescuento,
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
		FamiliaID,
		ProductoDescripcion,
		TipoMovimientoID,
		AlmacenID,
		Cantidad,
		Importe
	FROM #EntradaProducto
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT COUNT(EntradaProductoID) AS TotalReg
	FROM #EntradaProducto
	DROP TABLE #EntradaProducto
	SET NOCOUNT OFF;
END

GO
