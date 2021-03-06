USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ObtenerPorFiltro]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Pedido_ObtenerPorFiltro]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ObtenerPorFiltro]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jesus Alvarez
-- Create date: 24/06/2014
-- Description:	Obtiene los pedidos por folio paginado para los estatus recibidos
-- EXEC Pedido_ObtenerPorFiltro 0, 1, '<ROOT><Datos><estatusPedido>29</estatusPedido></Datos><Datos><estatusPedido>30</estatusPedido></Datos></ROOT>', 1, 1, 15
-- =============================================
CREATE PROCEDURE [dbo].[Pedido_ObtenerPorFiltro] 
	@Folio INT,
	@OrganizacionID INT,
	@xmlEstatusPedido XML,
	@Activo BIT,
	@Inicio INT,
	@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #tEstatusPedidos
	(
		EstatusID INT
	)
	INSERT INTO #tEstatusPedidos
	SELECT DISTINCT T.N.value('./estatusPedido[1]','INT') AS EstatusPedido
	FROM @xmlEstatusPedido.nodes('/ROOT/Datos') as T(N)
	SELECT Distinct  
			P.PedidoID,
			P.OrganizacionID,
			P.AlmacenID,
			P.FolioPedido,
			P.FechaPedido,
			P.Observaciones,
			P.EstatusID,
			E.Descripcion as DescripcionEstatus,
			P.Activo,
			P.FechaCreacion,
			P.UsuarioCreacionID,
			P.FechaModificacion,
			P.UsuarioModificacionID
    INTO #PEDIDOSTemporal
	FROM Pedido P
	INNER JOIN #tEstatusPedidos EP ON EP.EstatusID = P.EstatusID
    INNER JOIN Estatus E ON P.EstatusID = E.EstatusID
	INNER JOIN PedidoDetalle PD ON (P.PedidoID = PD.PedidoID AND PD.Activo = @Activo)
	INNER JOIN Producto PRO ON (PD.ProductoID = PRO.ProductoID AND PRO.Activo = @Activo)
			WHERE P.OrganizacionID = @OrganizacionID
		AND P.Activo = @Activo
		AND (@Folio = 0 OR P.FolioPedido LIKE '%' + CAST(@Folio AS VARCHAR(15)) + '%')
		AND CAST(P.FechaPedido AS DATE) > CAST(GETDATE()-2 AS DATE)
	SELECT ROW_NUMBER() OVER (
			ORDER BY P.FolioPedido ASC
			) AS RowNum, 
			P.PedidoID,
			P.OrganizacionID,
			P.AlmacenID,
			P.FolioPedido,
			P.FechaPedido,
			P.Observaciones,
			P.EstatusID,
			P.DescripcionEstatus,
			P.Activo,
			P.FechaCreacion,
			P.UsuarioCreacionID,
			P.FechaModificacion,
			P.UsuarioModificacionID
    INTO #PEDIDOS
	FROM #PEDIDOSTemporal P
	SELECT  PedidoID,
			OrganizacionID,
			AlmacenID,
			FolioPedido,
			FechaPedido,
			Observaciones,
			EstatusID,
			DescripcionEstatus,
			Activo,
			FechaCreacion,
			UsuarioCreacionID,
			FechaModificacion,
			UsuarioModificacionID
	  FROM #PEDIDOS
	  WHERE RowNum BETWEEN @Inicio AND @Limite;
	  SELECT COUNT(FolioPedido) AS TotalReg
	  FROM #PEDIDOS;
	  DROP TABLE #PEDIDOS;
	  SET NOCOUNT OFF;		  
END

GO
