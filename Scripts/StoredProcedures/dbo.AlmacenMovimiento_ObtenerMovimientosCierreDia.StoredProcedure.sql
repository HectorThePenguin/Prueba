IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[AlmacenMovimiento_ObtenerMovimientosCierreDia]')
		)
	DROP PROCEDURE [dbo].AlmacenMovimiento_ObtenerMovimientosCierreDia
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 07/07/2014
-- Description:  Obtiene los movimientos para generar
--				 la poliza
-- AlmacenMovimiento_ObtenerMovimientosCierreDia 1, 1, '20150113'
-- =============================================
CREATE PROCEDURE dbo.AlmacenMovimiento_ObtenerMovimientosCierreDia
@AlmacenID			INT
,@OrganizacionID	INT
,@FechaMovimiento	DATE = NULL
AS
BEGIN

	SET NOCOUNT ON

		DECLARE @SalidaPorConsumo INT, @EstatusAplicado INT
		SET @SalidaPorConsumo = 12
		SET @EstatusAplicado = 21

		IF (@FechaMovimiento IS NULL)
		BEGIN
			SET @FechaMovimiento = GETDATE()
		END

		DECLARE @Fecha DATE
		SET @Fecha = @FechaMovimiento

		DECLARE @Mes INT
		SET @Mes = MONTH(GETDATE())

			SELECT AM.AlmacenMovimientoID
				,  AM.TipoMovimientoID
				,  AM.FolioMovimiento
				,  CAST(CONVERT(CHAR(8), AM.FechaMovimiento, 112) AS DATETIME)	AS FechaMovimiento
				,  AM.Observaciones
				,  AMD.AlmacenMovimientoDetalleID
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
				,  A.AlmacenID
				,  A.Descripcion		AS Almacen
				,  A.OrganizacionID
				,  T.TratamientoID
				,  T.CodigoTratamiento
				,  TT.TipoTratamientoID
				,  TT.Descripcion		AS TipoTratamiento
			FROM Almacen A(NOLOCK)
			INNER JOIN AlmacenMovimiento AM(NOLOCK)
				ON (A.AlmacenID = AM.AlmacenID
					AND A.AlmacenID = @AlmacenID
					AND A.OrganizacionID = @OrganizacionID
					AND AM.Status = @EstatusAplicado
					AND AM.PolizaGenerada = 0)
			INNER JOIN AlmacenMovimientoDetalle AMD(NOLOCK)
				ON (AM.AlmacenMovimientoID = AMD.AlmacenMovimientoID)
			INNER JOIN Producto P(NOLOCK)
				ON (AMD.ProductoID = P.ProductoID)
			INNER JOIN SubFamilia SF(NOLOCK)
				ON (P.SubFamiliaID = SF.SubFamiliaID)
			INNER JOIN Familia F(NOLOCK)
				ON (SF.FamiliaID = F.FamiliaID)
			INNER JOIN Tratamiento T(NOLOCK)
				ON (AMD.TratamientoID = T.TratamientoID)
			INNER JOIN TipoTratamiento TT(NOLOCK)
				ON (T.TipoTratamientoID = TT.TipoTratamientoID)
			WHERE AM.TipoMovimientoID = @SalidaPorConsumo
				AND  CAST(AM.FechaMovimiento AS DATE) = @Fecha
	
	SET NOCOUNT OFF
END
