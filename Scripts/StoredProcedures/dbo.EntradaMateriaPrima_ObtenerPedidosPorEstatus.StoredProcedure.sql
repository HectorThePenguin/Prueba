USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaMateriaPrima_ObtenerPedidosPorEstatus]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaMateriaPrima_ObtenerPedidosPorEstatus]
GO
/****** Object:  StoredProcedure [dbo].[EntradaMateriaPrima_ObtenerPedidosPorEstatus]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Roque Solis
-- Create date: 10/06/2014
-- Description: Obtiene los pedidos parciales
-- SpName     : EXEC EntradaMateriaPrima_ObtenerPedidosPorEstatus 30,4,1
--======================================================
CREATE PROCEDURE [dbo].[EntradaMateriaPrima_ObtenerPedidosPorEstatus]
@OrganizacionID INT,
@FolioPedido INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT P.PedidoID, 
		   P.FolioPedido, 
		   O.OrganizacionID, 
		   O.Descripcion AS DescripcionOrganizacion, 
		   E.EstatusID, 
		   E.Descripcion AS DescripcionEstatus 
	  FROM Pedido P
	 INNER JOIN Organizacion O ON O.OrganizacionID = P.OrganizacionID
	 INNER JOIN Estatus E ON P.EstatusID = E.EstatusID
	 WHERE P.OrganizacionID = @OrganizacionID 
	   AND (CAST (P.FolioPedido AS VARCHAR) LIKE '%'+CAST (@FolioPedido AS VARCHAR)+'%' OR @FolioPedido = 0)
	   AND CAST(P.FechaPedido AS DATE) >= CAST(GETDATE()-3 AS DATE)
	   AND P.Activo = @Activo
	SET NOCOUNT OFF;
END

GO
