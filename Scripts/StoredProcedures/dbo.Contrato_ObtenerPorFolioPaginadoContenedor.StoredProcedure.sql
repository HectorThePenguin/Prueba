USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ObtenerPorFolioPaginadoContenedor]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Contrato_ObtenerPorFolioPaginadoContenedor]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ObtenerPorFolioPaginadoContenedor]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 28/07/2014
-- Description: Obtiene las entrada por folio
-- SpName     : Contrato_ObtenerPorFolioPaginadoContenedor 1, 1, '', 1, 15
--======================================================
CREATE PROCEDURE [dbo].[Contrato_ObtenerPorFolioPaginadoContenedor]
@Folio				INT
, @OrganizacionId	INT
, @Proveedor		VARCHAR(100)
, @Inicio			INT
, @Limite			INT
AS 
BEGIN
	SET NOCOUNT ON
	SELECT ROW_NUMBER() OVER (ORDER BY Pro.Descripcion ASC) AS [RowNum]
			, EP.EntradaProductoID,
			EP.OrganizacionID,
			EP.RegistroVigilanciaID,
			EP.Folio,
			EP.Fecha,
			EP.Observaciones,
			EP.PesoOrigen,
			EP.PesoBruto,
			EP.PesoTara,
			EP.Piezas,
			EP.TipoContratoID,
			EP.AlmacenInventarioLoteID,
			EP.AlmacenMovimientoID
			, EP.ProductoID
			, C.ContratoID
			, C.Cantidad
			, C.Merma
			, C.PesoNegociar
			, C.Precio
			, Pro.ProveedorID
			, Pro.CodigoSAP
			, Pro.Descripcion		AS Proveedor
		INTO #tContenedor
		FROM Contrato C
		INNER JOIN EntradaProducto EP
			ON (@Folio IN (EP.Folio, 0)
				AND C.OrganizacionID = @OrganizacionId
				AND C.ContratoID = EP.ContratoID
				AND C.ProductoID = EP.ProductoID
				AND EP.AlmacenMovimientoID > 0)
		INNER JOIN Proveedor Pro
			ON (C.ProveedorID = Pro.ProveedorID
				AND (@Proveedor = '' OR Pro.Descripcion LIKE '%' + @Proveedor + '%'))
		SELECT EntradaProductoID,
			OrganizacionID,
			RegistroVigilanciaID,
			Folio,
			Fecha,
			Observaciones,
			PesoOrigen,
			PesoBruto,
			PesoTara,
			Piezas,
			TipoContratoID,
			AlmacenInventarioLoteID,
			AlmacenMovimientoID
			, ProductoID
			, ContratoID
			, Cantidad
			, Merma
			, PesoNegociar
			, Precio
			, ProveedorID
			, CodigoSAP
			, Proveedor
		FROM #tContenedor
		WHERE RowNum BETWEEN @Inicio AND @Limite
		SELECT COUNT(EntradaProductoID) AS [TotalReg]
		FROM #tContenedor
		DROP TABLE #tContenedor
	SET NOCOUNT OFF
END

GO
