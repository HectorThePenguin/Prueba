USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ObtenerPorFiltroCancelacion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Pedido_ObtenerPorFiltroCancelacion]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ObtenerPorFiltroCancelacion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Pedro Delgado
-- Create date: 19/12/2014
-- Description:	Obtiene los pedidos por folio paginado para los estatus recibidos
-- ============================================
CREATE PROCEDURE [dbo].[Pedido_ObtenerPorFiltroCancelacion]
@Folio INT,
@OrganizacionID INT,
@XMLEstatus XML,
@Fecha DATETIME,
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
		O.Descripcion,
		P.AlmacenID,
		P.FolioPedido,
		P.FechaPedido,
		P.Observaciones,
		P.EstatusID,
		E.Descripcion AS DescripcionEstatus,
		P.Activo,
		P.FechaCreacion,
		P.UsuarioCreacionID,
		P.FechaModificacion,
		P.UsuarioModificacionId
	INTO #Pedido
	FROM Pedido (NOLOCK) P
	INNER JOIN Organizacion (NOLOCK) O ON (P.OrganizacionID = O.OrganizacionID)
	INNER JOIN Estatus (NOLOCK) E ON (P.EstatusID = E.EstatusID)
	WHERE P.OrganizacionID = @OrganizacionID
				AND E.EstatusID IN (SELECT DISTINCT EstatusID = T.N.value('./EstatusID[1]','INT')
													FROM @XMLEstatus.nodes('/ROOT/EstatusPedido') as T(N))
				AND FechaPedido >= @Fecha
				AND P.Activo = 1
				AND (CAST(P.FolioPedido AS VARCHAR(12)) LIKE '%'+ CAST(@Folio AS VARCHAR(12)) +'%' OR @Folio = 0)
	SELECT  PedidoID,
			OrganizacionID,
			Descripcion,
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
			UsuarioModificacionId
	  FROM #Pedido
	  WHERE RowNum BETWEEN @Inicio AND @Limite;
	  SELECT COUNT(FolioPedido) AS TotalReg
	  FROM #Pedido;
	  DROP TABLE #Pedido;
	  SET NOCOUNT OFF;	
END

GO
