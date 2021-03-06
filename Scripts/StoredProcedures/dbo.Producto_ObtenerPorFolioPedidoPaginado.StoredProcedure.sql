USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorFolioPedidoPaginado]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorFolioPedidoPaginado]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorFolioPedidoPaginado]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/06/16
-- Description: Procedimiento almacenado para obtener folios de pase a proceso
-- Producto_ObtenerPorFolioPedidoPaginado 1, '', 1, 1, 15
--=============================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorFolioPedidoPaginado]
@PedidoID INT,
@Descripcion VARCHAR(50),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ROW_NUMBER() OVER (
			ORDER BY Pro.Descripcion ASC
			) AS RowNum, 
		   Pro.ProductoID
		,  Pro.Descripcion	AS Producto
		,  sb.Descripcion	AS SubFamilia
	INTO #ProductosPedidos
	FROM Pedido P
	INNER JOIN PedidoDetalle PD
		ON (P.PedidoID = PD.PedidoID
			AND P.PedidoID = @PedidoID)
	INNER JOIN Producto Pro
		ON (PD.ProductoID = Pro.ProductoID
			AND (@Descripcion = '' OR Pro.Descripcion LIKE '%' + @Descripcion + '%'))
	INNER JOIN SubFamilia SB
		ON (Pro.SubFamiliaID = sb.SubFamiliaID)
	SELECT ProductoID
		,  Producto
		,  SubFamilia
	FROM #ProductosPedidos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT COUNT(ProductoID) AS TotalReg
	FROM #ProductosPedidos
	DROP TABLE #ProductosPedidos
	SET NOCOUNT OFF;
END

GO
