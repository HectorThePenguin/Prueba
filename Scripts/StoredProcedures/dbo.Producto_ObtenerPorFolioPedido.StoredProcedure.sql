USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorFolioPedido]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorFolioPedido]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorFolioPedido]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/06/16
-- Description: Procedimiento almacenado para obtener folios de pase a proceso
-- Producto_ObtenerPorFolioPedido 1, 80
--=============================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorFolioPedido]
@PedidoID INT,
@ProductoID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Pro.ProductoID
		,  Pro.Descripcion	AS Producto
		,  sb.Descripcion	AS SubFamilia
	FROM Pedido P
	INNER JOIN PedidoDetalle PD
		ON (P.PedidoID = PD.PedidoID
			AND P.Activo = 1
			AND P.PedidoID = @PedidoID)
	INNER JOIN Producto Pro
		ON (PD.ProductoID = Pro.ProductoID
			AND Pro.ProductoID = @ProductoID)
	INNER JOIN SubFamilia SB
		ON (Pro.SubFamiliaID = sb.SubFamiliaID)
	SET NOCOUNT OFF;
END

GO
