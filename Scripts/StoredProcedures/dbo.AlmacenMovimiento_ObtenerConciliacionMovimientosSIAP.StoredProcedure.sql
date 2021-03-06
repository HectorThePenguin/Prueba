USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimiento_ObtenerConciliacionMovimientosSIAP]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimiento_ObtenerConciliacionMovimientosSIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimiento_ObtenerConciliacionMovimientosSIAP]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 10/10/2014
-- Description:  Obtiene un proveedor por producto
-- AlmacenMovimiento_ObtenerConciliacionMovimientosSIAP 1, '20150301', '20150313'
-- =============================================
CREATE PROCEDURE [dbo].[AlmacenMovimiento_ObtenerConciliacionMovimientosSIAP]
@OrganizacionID INT
, @FechaInicial DATE
, @FechaFinal DATE
AS
BEGIN
	SET NOCOUNT ON;

		CREATE TABLE #tAlmacenMovimiento
		(
			AlmacenMovimientoID		BIGINT
			,  AlmacenID			INT
			,  TipoMovimientoID		INT
			,  ProveedorID			INT
			,  FolioMovimiento		BIGINT
			,  Observaciones		VARCHAR(255)
			,  FechaMovimiento		DATE
			,  Status				INT
			,  AnimalMovimientoID	BIGINT
			,  OrganizacionID		INT
			--,  Almacen				VARCHAR(255)
		)

		INSERT INTO #tAlmacenMovimiento
		SELECT AM.AlmacenMovimientoID
			,  AM.AlmacenID
			,  AM.TipoMovimientoID
			,  AM.ProveedorID
			,  AM.FolioMovimiento
			,  AM.Observaciones
			,  AM.FechaMovimiento
			,  AM.Status
			,  AM.AnimalMovimientoID
			,  A.OrganizacionID
			--,  A.Descripcion			AS Almacen
		FROM AlmacenMovimiento AM
		INNER JOIN Almacen A
			ON (AM.AlmacenID = A.AlmacenID
				AND A.OrganizacionID = @OrganizacionID)
		WHERE CAST(AM.FechaMovimiento AS DATE) BETWEEN @FechaInicial AND @FechaFinal
			AND AM.UsuarioCreacionID <> 1

		SELECT DISTINCT AM.AlmacenMovimientoID
			,  AM.AlmacenID
			,  AM.TipoMovimientoID
			,  AM.ProveedorID
			,  AM.FolioMovimiento
			,  AM.Observaciones
			,  AM.FechaMovimiento
			,  AM.Status
			,  AM.AnimalMovimientoID
			,  TM.Descripcion			AS TipoMovimiento
			,  TM.EsEntrada
			,  TM.EsGanado
			,  TM.EsProducto
			,  TM.EsSalida
			,  TM.TipoPolizaID
			,  AM.OrganizacionID
			--,  AM.Almacen
		FROM #tAlmacenMovimiento AM
		INNER JOIN TipoMovimiento TM
			ON (AM.TipoMovimientoID = TM.TipoMovimientoID)
		WHERE AM.OrganizacionID = @OrganizacionID
		ORDER BY AM.AlmacenMovimientoID

		SELECT DISTINCT AMD.AlmacenMovimientoDetalleID
			,  AMD.AlmacenMovimientoID
			,  ISNULL(AMD.AlmacenInventarioLoteID, 0)	AS AlmacenInventarioLoteID
			,  ISNULL(AMD.ContratoID, 0)	AS ContratoID
			,  AMD.Piezas
			,  ISNULL(AMD.TratamientoID, 0)	AS TratamientoID
			,  AMD.ProductoID
			,  AMD.Precio
			,  AMD.Cantidad
			,  AMD.Importe
			,  ISNULL(TT.TipoTratamientoID, 0)	AS TipoTratamientoID
			,  TT.Descripcion		AS TipoTratamiento
			,  AMD.FechaCreacion
		FROM #tAlmacenMovimiento AM
		INNER JOIN AlmacenMovimientoDetalle AMD
			ON (AM.AlmacenMovimientoID = AMD.AlmacenMovimientoID)
		LEFT OUTER JOIN Tratamiento T
			ON (AMD.TratamientoID = T.TratamientoID)
		LEFT OUTER JOIN TipoTratamiento TT
			ON (T.TipoTratamientoID = TT.TipoTratamientoID)
		WHERE AM.OrganizacionID = @OrganizacionID
		ORDER BY AMD.AlmacenMovimientoDetalleID

		DROP TABLE #tAlmacenMovimiento

	SET NOCOUNT OFF;
END

GO
