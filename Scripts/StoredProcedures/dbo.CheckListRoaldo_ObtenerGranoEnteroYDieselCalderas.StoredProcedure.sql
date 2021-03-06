USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoaldo_ObtenerGranoEnteroYDieselCalderas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoaldo_ObtenerGranoEnteroYDieselCalderas]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoaldo_ObtenerGranoEnteroYDieselCalderas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 01/07/2014
-- Description:  Obtener CheckList
-- CheckListRoaldo_ObtenerGranoEnteroYDieselCalderas 1, '20141223'
-- =============================================
CREATE PROCEDURE [dbo].[CheckListRoaldo_ObtenerGranoEnteroYDieselCalderas]
 @OrganizacionID INT
 , @FechaInicio  DATETIME
 AS
 BEGIN
	SET NOCOUNT ON
		DECLARE @EstatusRecibidoPedido		INT
		    ,   @EstatusRecibidoSolicitud	INT
			,   @FamiliaMateriaPrima		INT
			,	@SubFamiliaGranos			INT
			,   @FamiliaCombustibles		INT
			,	@SubFamiliaGasolinaDiesel	INT
		SELECT @EstatusRecibidoPedido = 31
			 , @FamiliaMateriaPrima = 1
			 , @SubFamiliaGranos = 1
			 , @FamiliaCombustibles = 4
			 , @SubFamiliaGasolinaDiesel = 22
			 , @EstatusRecibidoSolicitud = 38
		DECLARE @CantidadEntregada DECIMAL(14,2)
			,   @Cantidad		   DECIMAL(18,2)
			,   @FechaTurno		   CHAR(8)
		SET @FechaTurno = CONVERT(CHAR(8), @FechaInicio, 112)
		SELECT @CantidadEntregada = ISNULL(SUM(PMP.CantidadEntregada), 0)
		FROM ProgramacionMateriaPrima PMP
		INNER JOIN PedidoDetalle PD
			ON (PMP.PedidoDetalleID = PD.PedidoDetalleID)
		INNER JOIN Pedido P
			ON (PD.PedidoID = P.PedidoID
				AND P.EstatusID = @EstatusRecibidoPedido
				AND P.OrganizacionID = @OrganizacionID
				AND PMP.OrganizacionID = P.OrganizacionID
				AND CONVERT(CHAR(8), P.FechaPedido, 112) = @FechaTurno)
		INNER JOIN Producto Pro
			ON (PD.ProductoID = Pro.ProductoID)
		INNER JOIN SubFamilia SF
			ON (Pro.SubFamiliaID = SF.SubFamiliaID
				AND SF.SubFamiliaID = @SubFamiliaGranos)
		INNER JOIN Familia F
			ON (SF.FamiliaID = F.FamiliaID
				AND F.FamiliaID = @FamiliaMateriaPrima)
		SELECT @Cantidad = ISNULL(SUM(SPD.Cantidad), 0)
		FROM SolicitudProducto SP
		INNER JOIN SolicitudProductoDetalle SPD
			ON (SP.SolicitudProductoID = SPD.SolicitudProductoID
				AND SP.EstatusID = @EstatusRecibidoSolicitud
				AND SP.OrganizacionID = @OrganizacionID
				AND CONVERT(CHAR(8), SP.FechaEntrega, 112) = @FechaTurno)
		INNER JOIN Producto Pro
			ON (SPD.ProductoID = Pro.ProductoID)
		INNER JOIN SubFamilia SF
			ON (Pro.SubFamiliaID = SF.SubFamiliaID
				AND SF.SubFamiliaID = @SubFamiliaGasolinaDiesel)
		INNER JOIN Familia F
			ON (SF.FamiliaID = F.FamiliaID
				AND F.FamiliaID = @FamiliaCombustibles)
		SELECT @CantidadEntregada	AS CantidadEntregada
			 , @Cantidad			AS Cantidad
	SET NOCOUNT OFF
END

GO
