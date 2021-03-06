USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimiento_ObtenerMovimientosFiltros]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimiento_ObtenerMovimientosFiltros]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimiento_ObtenerMovimientosFiltros]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jorge Luis Velazquez Araujo
-- Create date: 16/08/2014
-- Description:  Obtiene los movimientos para generar
--				 la poliza
-- AlmacenMovimiento_ObtenerMovimientosFiltros 4, 1,3,1,1
-- =============================================
CREATE PROCEDURE [dbo].[AlmacenMovimiento_ObtenerMovimientosFiltros]
@AlmacenID			INT
,@OrganizacionID	INT
,@FolioMovimiento bigint
,@TipoMovimientoID int
,@EstatusID INT
AS
BEGIN
	SET NOCOUNT ON
		DECLARE @Fecha DATE
		SET @Fecha = cast(getdate() AS DATE)
			SELECT 
				a.AlmacenID
				,a.Descripcion as Almacen
				,a.OrganizacionID
				,AM.AlmacenMovimientoID				
				,  AM.TipoMovimientoID
				,  AM.FechaMovimiento
				,  AM.Observaciones
				,  AMD.AlmacenMovimientoDetalleID
				,  AMD.AlmacenInventarioLoteID
				,  AMD.Cantidad
				,  AMD.Importe
				,  AMD.Piezas
				,  AMD.Precio
				,  P.ProductoID
				,  P.Descripcion		AS Producto
				,  SF.SubFamiliaID
				,  SF.Descripcion		AS SubFamilia
				,  F.FamiliaID
				,  F.Descripcion		AS Familia
			FROM Almacen A(NOLOCK)
			INNER JOIN AlmacenMovimiento AM(NOLOCK)
				ON (A.AlmacenID = AM.AlmacenID
					AND A.AlmacenID = @AlmacenID
					AND A.OrganizacionID = @OrganizacionID)
			INNER JOIN AlmacenMovimientoDetalle AMD(NOLOCK)
				ON (AM.AlmacenMovimientoID = AMD.AlmacenMovimientoID)
			INNER JOIN Producto P(NOLOCK)
				ON (AMD.ProductoID = P.ProductoID)
			INNER JOIN SubFamilia SF(NOLOCK)
				ON (P.SubFamiliaID = SF.SubFamiliaID)
			INNER JOIN Familia F(NOLOCK)
				ON (SF.FamiliaID = F.FamiliaID)
			WHERE AM.TipoMovimientoID = @TipoMovimientoID
				AND am.Status = @EstatusID
				AND  CAST(AM.FechaMovimiento AS DATE) = @Fecha
	SET NOCOUNT OFF
END

GO
