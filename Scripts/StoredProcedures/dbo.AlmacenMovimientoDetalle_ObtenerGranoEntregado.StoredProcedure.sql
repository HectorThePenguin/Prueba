USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoDetalle_ObtenerGranoEntregado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimientoDetalle_ObtenerGranoEntregado]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoDetalle_ObtenerGranoEntregado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 23/12/2014
-- Description:  Obtiene la cantidad de granos entregados a planta
-- AlmacenMovimientoDetalle_ObtenerGranoEntregado '2014-11-21 11:33:34.927', '2014-12-23 11:33:34.927', 1
-- =============================================
CREATE PROCEDURE [dbo].[AlmacenMovimientoDetalle_ObtenerGranoEntregado]
@FechaInicio				DATETIME
,@FechaFin					DATETIME
,@OrganizacionID			INT
AS
BEGIN
	SET NOCOUNT ON
			DECLARE @SubFamiliaGranos			INT
			DECLARE @FamiliaMateriaPrima		INT
			DECLARE @TipoMovimientoPaseProceso	INT
			DECLARE @EstatusAplicado			INT
			SET @SubFamiliaGranos = 1
			SET @FamiliaMateriaPrima = 1
			SET @TipoMovimientoPaseProceso = 25
			SET @EstatusAplicado = 21
			SELECT AMD.AlmacenMovimientoID
				,  AMD.AlmacenInventarioLoteID
				,  AMD.AlmacenMovimientoDetalleID
				,  AMD.ProductoID
				,  AMD.Cantidad
				,  AMD.Precio
				,  AMD.Importe
				,  AM.Status
				,  AM.TipoMovimientoID
				,  AM.FechaMovimiento
			FROM Almacen A
			INNER JOIN AlmacenMovimiento AM
				ON (A.AlmacenID = AM.AlmacenID
					AND A.OrganizacionID = @OrganizacionID
					AND AM.FechaMovimiento BETWEEN @FechaInicio AND @FechaFin)
			INNER JOIN AlmacenMovimientoDetalle AMD
				ON (AM.AlmacenMovimientoID = AMD.AlmacenMovimientoID
					AND AM.TipoMovimientoID = @TipoMovimientoPaseProceso
					AND AM.Status = @EstatusAplicado)
			INNER JOIN Producto P
				ON (AMD.ProductoID = P.ProductoID)
			INNER JOIN SubFamilia SF	
				ON (P.SubFamiliaID = SF.SubFamiliaID
					AND SF.SubFamiliaID = @SubFamiliaGranos
					AND SF.FamiliaID = @FamiliaMateriaPrima)
	SET NOCOUNT OFF
END

GO
