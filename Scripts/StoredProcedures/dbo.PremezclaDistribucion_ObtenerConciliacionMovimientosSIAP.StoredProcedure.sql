USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PremezclaDistribucion_ObtenerConciliacionMovimientosSIAP]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PremezclaDistribucion_ObtenerConciliacionMovimientosSIAP]
GO
/****** Object:  StoredProcedure [dbo].[PremezclaDistribucion_ObtenerConciliacionMovimientosSIAP]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 10/10/2014
-- Description:  Obtiene un proveedor por producto
-- PremezclaDistribucion_ObtenerConciliacionMovimientosSIAP '20151021', '20151021',2
-- =============================================
CREATE PROCEDURE [dbo].[PremezclaDistribucion_ObtenerConciliacionMovimientosSIAP]
@FechaInicial DATE
, @FechaFinal DATE
, @OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
		CREATE TABLE #tPremezclaDistribucion
		(
			PremezclaDistribucionID			INT
			, ProductoID						INT
			, FechaEntrada					SMALLDATETIME
			, CantidadExistente				BIGINT
			, CostoUnitario					DECIMAL(18,2)
			, AlmacenMovimientoID			BIGINT
			, ProveedorID					INT
			, IVA							BIT
			, Producto						VARCHAR(250)
			, UnidadID						INT
			, SubFamiliaID					INT
			, CodigoSAP						VARCHAR(250)
			, Proveedor						VARCHAR(250)
		)
		INSERT INTO #tPremezclaDistribucion
		SELECT PD.PremezclaDistribucionID
			,  PD.ProductoID
			,  PD.FechaEntrada
			,  PD.CantidadExistente
			,  PD.CostoUnitario
			,  PDD.AlmacenMovimientoID
			,  PD.ProveedorID
			,  PD.IVA
			,  P.Descripcion	AS Producto
			,  P.UnidadID
			,  P.SubFamiliaID
			,  Prov.CodigoSAP
			,  Prov.Descripcion	AS Proveedor
		FROM PremezclaDistribucion PD
		INNER JOIN Producto P
			ON (PD.ProductoID = P.ProductoID)
		INNER JOIN Proveedor Prov
			ON (PD.ProveedorID = Prov.ProveedorID)
		INNER JOIN PremezclaDistribucionDetalle PDD
			ON (PD.PremezclaDistribucionID = PDD.PremezclaDistribucionID
				AND PDD.OrganizacionID = @OrganizacionID)
		WHERE CAST(PD.FechaEntrada AS DATE) BETWEEN @FechaInicial AND @FechaFinal

		SELECT PremezclaDistribucionID
			,  ProductoID
			,  FechaEntrada
			,  CantidadExistente
			,  CostoUnitario
			,  AlmacenMovimientoID
			,  ProveedorID
			,  IVA
			,  Producto
			,  UnidadID
			,  SubFamiliaID
			,  CodigoSAP
			,  Proveedor
		FROM #tPremezclaDistribucion
		SELECT PDD.PremezclaDistribucionDetalleID
			,  PDD.PremezclaDistribucionID
			,  PDD.OrganizacionID
			,  PDD.CantidadASurtir
			,  A.AlmacenID
			,  AIL.Lote
		FROM #tPremezclaDistribucion PD
		INNER JOIN PremezclaDistribucionDetalle PDD
			ON (PD.PremezclaDistribucionID = PDD.PremezclaDistribucionID
				AND PDD.OrganizacionID = @OrganizacionID)
		INNER JOIN AlmacenMovimiento AM
			ON (PD.AlmacenMovimientoID = AM.AlmacenMovimientoID)
		INNER JOIN AlmacenMovimientoDetalle AMD
			ON (AM.AlmacenMovimientoID = AMD.AlmacenMovimientoID)
		INNER JOIN AlmacenInventarioLote AIL
			ON (AMD.AlmacenInventarioLoteID = AIL.AlmacenInventarioLoteID)
		INNER JOIN Almacen A
			ON (AM.AlmacenID = A.AlmacenID)
		DROP TABLE #tPremezclaDistribucion
	SET NOCOUNT OFF;
END

GO
