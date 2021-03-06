USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Liquidacion_ObtenerFolioLiquidacionPorFolio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Liquidacion_ObtenerFolioLiquidacionPorFolio]
GO
/****** Object:  StoredProcedure [dbo].[Liquidacion_ObtenerFolioLiquidacionPorFolio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 17/12/2014
-- Description: 
-- SpName     : Liquidacion_ObtenerFolioLiquidacionPorFolio 1, 1
--======================================================
CREATE PROCEDURE [dbo].[Liquidacion_ObtenerFolioLiquidacionPorFolio]
@Folio	INT
, @OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
		SELECT L.ContratoID
			,  L.Folio
			,  L.Fecha
			,  C.ProductoID
			,  C.ProveedorID
			,  Prov.Descripcion		AS Proveedor
			,  Prod.Descripcion		AS Producto
			,  L.OrganizacionID
			,  L.LiquidacionID
			,  O.Descripcion		AS Organizacion
		FROM Liquidacion L
		INNER JOIN Contrato C
			ON (L.ContratoID = C.ContratoID)
		INNER JOIN Proveedor Prov
			ON (C.ProveedorID = Prov.ProveedorID)
		INNER JOIN Producto Prod
			ON (C.ProductoID = Prod.ProductoID)
		INNER JOIN Organizacion O
			ON (L.OrganizacionID = O.OrganizacionID)
		WHERE CAST(L.Fecha AS DATE) >= CAST(GETDATE() - 6 AS DATE)
			AND L.Folio = @Folio
			AND L.OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

GO
