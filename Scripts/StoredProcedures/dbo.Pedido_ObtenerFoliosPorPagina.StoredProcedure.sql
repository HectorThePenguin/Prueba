USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ObtenerFoliosPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Pedido_ObtenerFoliosPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ObtenerFoliosPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Octavio Quintero>
-- Create date: <13/06/2014>
-- Description:	Obtiene los pedidos por folio paginado
-- EXEC Pedido_ObtenerFoliosPorPagina 1,1,28,1,1,10,  10,1,29,1, 15, 25
-- =============================================
CREATE PROCEDURE [dbo].[Pedido_ObtenerFoliosPorPagina] 
	@Folio INT,
	@OrganizacionID INT,
	@EstatusID INT,
	@Activo BIT,
	@Inicio INT,
	@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
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
			E.Descripcion as DescripcionEstatus,
			P.Activo,
			P.FechaCreacion,
			P.UsuarioCreacionID,
			P.FechaModificacion,
			P.UsuarioModificacionID
    INTO #PEDIDOS
	FROM Pedido P,
		 Estatus E
	WHERE P.EstatusID = E.EstatusID
		AND P.OrganizacionID = @OrganizacionID
		AND P.EstatusID = @EstatusID
		AND P.Activo = @Activo
		AND (@Folio = 0 OR P.FolioPedido LIKE '%' + CAST(@Folio AS VARCHAR(15)) + '%');
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
