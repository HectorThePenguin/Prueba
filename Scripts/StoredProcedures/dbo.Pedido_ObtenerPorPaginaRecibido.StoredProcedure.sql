USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ObtenerPorPaginaRecibido]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Pedido_ObtenerPorPaginaRecibido]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ObtenerPorPaginaRecibido]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gilberto Carranza
-- Create date: 15-08-2014
-- Description:	Otiene un listado de Pedidos paginado
-- Pedido_ObtenerPorPaginaRecibido '', 1 , 1, 15 
-- =============================================
CREATE PROCEDURE [dbo].[Pedido_ObtenerPorPaginaRecibido]
@Descripcion VARCHAR(100)
, @OrganizacionID	INT
, @Inicio INT
, @Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @EstatusRecibido INT
	SET @EstatusRecibido = 31
	SELECT ROW_NUMBER() OVER ( ORDER BY A.Descripcion ASC) AS RowNum
		,  P.PedidoID
		,  P.FolioPedido
		,  P.FechaPedido
		,  O.Descripcion	AS Organizacion
		,  O.OrganizacionID
		,  A.AlmacenID
		,  A.Descripcion	AS Almacen
	INTO #Pedido
	FROM Pedido P
	INNER JOIN Organizacion O
		ON (P.OrganizacionID = O.OrganizacionID)
	INNER JOIN Almacen A
		ON (P.AlmacenID = A.AlmacenID
			AND O.OrganizacionID = A.OrganizacionID
			AND (@Descripcion = '' OR A.Descripcion LIKE '%' + @Descripcion + '%'))
	WHERE P.OrganizacionID = @OrganizacionID
		--AND P.EstatusID = @EstatusRecibido
	SELECT 
		PedidoID
		,  FolioPedido
		,  FechaPedido
		,  OrganizacionID
		,  Organizacion
		,  AlmacenID
		,  Almacen
	FROM #Pedido
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT 
		COUNT(PedidoID) AS TotalReg 
	FROM #Pedido
	DROP TABLE #Pedido
	SET NOCOUNT OFF;
END

GO
