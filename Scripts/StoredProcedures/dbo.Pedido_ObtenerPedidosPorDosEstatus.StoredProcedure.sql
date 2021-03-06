USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ObtenerPedidosPorDosEstatus]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Pedido_ObtenerPedidosPorDosEstatus]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ObtenerPedidosPorDosEstatus]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 10/06/2014
-- Description: Obtiene los pedidos Por dos tipos de estatus
-- SpName     : EXEC Pedido_ObtenerPedidosPorDosEstatus 29,30,1,0,1
--======================================================
CREATE PROCEDURE [dbo].[Pedido_ObtenerPedidosPorDosEstatus]
@PedidoEstatusIdUno INT,
@PedidoEstatusIdDos INT,
@OrganizacionID INT,
@FolioPedido INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Fecha3Dias smalldatetime
	set @Fecha3Dias = GETDATE() - 3
	SELECT P.PedidoID, 
		   P.FolioPedido, 
		   O.OrganizacionID, 
		   O.Descripcion AS DescripcionOrganizacion, 
		   E.EstatusID, 
		   E.Descripcion AS DescripcionEstatus 
	  FROM Pedido P (NOLOCK)
	 INNER JOIN Organizacion O (NOLOCK) ON O.OrganizacionID = P.OrganizacionID
	 INNER JOIN Estatus E (NOLOCK) ON P.EstatusID = E.EstatusID
	 WHERE P.OrganizacionID = @OrganizacionID 
	   AND P.EstatusID IN (@PedidoEstatusIdUno, @PedidoEstatusIdDos)
	   AND (@FolioPedido = 0 OR P.FolioPedido = @FolioPedido)
	   AND P.Activo = @Activo
	   AND CAST(P.FechaPedido AS DATE) >= CAST(@Fecha3Dias AS DATE) 
	SET NOCOUNT OFF;
END

GO
