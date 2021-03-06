USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ObtenerFolioPorFolioPedido]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Pedido_ObtenerFolioPorFolioPedido]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ObtenerFolioPorFolioPedido]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/06/16
-- Description: Procedimiento almacenado para obtener folios de pase a proceso
-- Pedido_ObtenerFolioPorFolioPedido 1, 63
--=============================================
CREATE PROCEDURE [dbo].[Pedido_ObtenerFolioPorFolioPedido]
@OrganizacionID INT,
@FolioPedido INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @EstatusRecibido INT, @TipoEstatusRecibido INT
	SET @EstatusRecibido = 30
	SET @TipoEstatusRecibido = 8
	SELECT P.FolioPedido
		,  A.Descripcion
		,  P.PedidoID
	FROM Pedido P
	INNER JOIN Estatus E
		ON (P.EstatusID = E.EstatusID
			AND P.OrganizacionID = @OrganizacionID
			AND P.Activo = 1
			AND P.FolioPedido = @FolioPedido
			AND E.EstatusID = @EstatusRecibido)
	INNER JOIN TipoEstatus TE
		ON (E.TipoEstatus = TE.TipoEstatusID
			AND TE.TipoEstatusID = @TipoEstatusRecibido)
	INNER JOIN Almacen A
		ON (P.AlmacenID = A.AlmacenID)
	SET NOCOUNT OFF;
END

GO
