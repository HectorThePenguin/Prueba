USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ObtenerRecibido]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Pedido_ObtenerRecibido]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ObtenerRecibido]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gilberto Carranza
-- Create date: 15-08-2014
-- Description:	Otiene Pedidos Recibidos
-- Pedido_ObtenerRecibido 1, 54
-- =============================================
CREATE PROCEDURE [dbo].[Pedido_ObtenerRecibido]
@OrganizacionID	INT
, @FolioPedido	INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @EstatusRecibido INT
	SET @EstatusRecibido = 31
	SELECT P.PedidoID
		,  P.FolioPedido
		,  P.FechaPedido
		,  O.Descripcion	AS Organizacion
		,  O.OrganizacionID
		,  A.AlmacenID
		,  A.Descripcion	AS Almacen
	FROM Pedido P
	INNER JOIN Organizacion O
		ON (P.OrganizacionID = O.OrganizacionID
			AND P.OrganizacionID = @OrganizacionID
			--AND P.EstatusID = @EstatusRecibido
			AND P.FolioPedido = @FolioPedido)
	INNER JOIN Almacen A
		ON (P.AlmacenID = A.AlmacenID
			AND O.OrganizacionID = A.OrganizacionID)
	SET NOCOUNT OFF;
END

GO
