USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PremezclaDistribucion_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PremezclaDistribucion_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[PremezclaDistribucion_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 10/10/2014
-- Description:  Obtiene un proveedor por producto
-- PremezclaDistribucion_ObtenerPorID 574
-- =============================================
CREATE PROCEDURE [dbo].[PremezclaDistribucion_ObtenerPorID]
@PremezclaDistribucionID int

AS
BEGIN
	SET NOCOUNT ON;		
		
		SELECT PD.PremezclaDistribucionID
			,  PD.ProductoID
			,  PD.FechaEntrada
			,  PD.CantidadExistente
			,  PD.CostoUnitario			
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
		WHERE pd.PremezclaDistribucionID = @PremezclaDistribucionID

		
		SELECT PDD.PremezclaDistribucionDetalleID
			,  PDD.PremezclaDistribucionID
			,  PDD.OrganizacionID
			,  PDD.CantidadASurtir
			,  A.AlmacenID
			,  AIL.Lote
			,  pdd.AlmacenMovimientoID
		FROM PremezclaDistribucionDetalle PDD			
		INNER JOIN AlmacenMovimiento AM
			ON (PDD.AlmacenMovimientoID = AM.AlmacenMovimientoID)
		INNER JOIN AlmacenMovimientoDetalle AMD
			ON (AM.AlmacenMovimientoID = AMD.AlmacenMovimientoID)
		INNER JOIN AlmacenInventarioLote AIL
			ON (AMD.AlmacenInventarioLoteID = AIL.AlmacenInventarioLoteID)
		INNER JOIN Almacen A
			ON (AM.AlmacenID = A.AlmacenID)
		where PDD.PremezclaDistribucionID = 	@PremezclaDistribucionID	
		
		SELECT  
		PremezclaDistribucionCostoID
		,PremezclaDistribucionID
		,co.CostoID
		,co.Descripcion AS Costo
		,TieneCuenta
		,pr.ProveedorID
		,pr.Descripcion AS Proveedor
		,pr.CodigoSAP AS ProveedorSAP
		,cs.CuentaSAPID
		,cs.CuentaSAP
		,CuentaProvision
		,Importe
		,Iva
		,Retencion
		 FROM PremezclaDistribucionCosto pdc
		 inner join Costo co on pdc.CostoID = co.CostoID
		 left join Proveedor pr on pdc.ProveedorID = pr.ProveedorID
		 left join CuentaSAP cs on pdc.CuentaProvision = cs.CuentaSAP
		where pdc.PremezclaDistribucionID = @PremezclaDistribucionID	


	SET NOCOUNT OFF;
END

GO
