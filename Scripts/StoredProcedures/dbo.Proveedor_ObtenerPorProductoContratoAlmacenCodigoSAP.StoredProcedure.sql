USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorProductoContratoAlmacenCodigoSAP]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_ObtenerPorProductoContratoAlmacenCodigoSAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorProductoContratoAlmacenCodigoSAP]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 10/10/2014
-- Description:  Obtiene un proveedor por producto
-- Proveedor_ObtenerPorProductoContratoAlmacenCodigoSAP '0000300065', 0, 0, 1
-- =============================================
CREATE PROCEDURE [dbo].[Proveedor_ObtenerPorProductoContratoAlmacenCodigoSAP] @CodigoSAP VARCHAR(20)
	,@OrganizacionID INT
	,@ProductoID INT
	,@AlmacenID INT
	,@Activo INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT P.ProveedorID
		,P.Descripcion
		,P.CodigoSAP
		,TP.TipoProveedorID
		,TP.Descripcion AS TipoProveedor
	FROM Proveedor P
	INNER JOIN TipoProveedor TP ON (P.TipoProveedorID = TP.TipoProveedorID)
	LEFT JOIN Contrato C ON (P.ProveedorID = C.ProveedorID AND @ProductoID IN (C.ProductoID,0))
	INNER JOIN ProveedorAlmacen pa ON pa.ProveedorID = p.ProveedorID AND pa.AlmacenID = @AlmacenID
	WHERE P.CodigoSAP = @CodigoSAP
		AND P.Activo = @Activo
		AND @OrganizacionID IN (C.OrganizacionID,0)
		AND (@OrganizacionID = 0 OR C.Activo = 1)
	GROUP BY P.ProveedorID
		,P.Descripcion
		,P.CodigoSAP
		,TP.TipoProveedorID
		,TP.Descripcion
	SET NOCOUNT OFF;
END

GO
