USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Pedido_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Octavio Quintero
-- Create date: 12/06/2014
-- Description: Obtiene todos los pedidos 
-- SpName     : EXEC Pedido_ObtenerTodos 1,1
--======================================================
CREATE PROCEDURE [dbo].[Pedido_ObtenerTodos]
	@Activo BIT,
	@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT P.PedidoID, 
		   P.OrganizacionID,
		   P.AlmacenID,
		   P.FolioPedido,
		   P.FechaPedido,
		   P.EstatusID,
		   P.Activo,
		   P.FechaCreacion,
		   P.UsuarioCreacionID,
		   P.FechaModificacion,
		   P.UsuarioModificacionID,
		   E.Descripcion AS DescripcionEstatus,
		   P.Observaciones
	  FROM Pedido P (NOLOCK),
		   Estatus E (NOLOCK)
	 WHERE P.EstatusID = E.EstatusID
		AND P.Activo = @Activo
		AND P.OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

GO
