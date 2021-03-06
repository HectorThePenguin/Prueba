USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoBoleta_ObtenerPorProductoOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorProductoBoleta_ObtenerPorProductoOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoBoleta_ObtenerPorProductoOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 13/04/2015
-- Description: Obtiene los indicadores de producto para las boletas
-- SpName     : IndicadorProductoBoleta_ObtenerPorProductoOrganizacion 101, 1, 1
--======================================================
CREATE PROCEDURE [dbo].[IndicadorProductoBoleta_ObtenerPorProductoOrganizacion]
@ProductoID INT
, @OrganizacionID	 INT
,@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		IPB.IndicadorProductoBoletaID,
		IPB.IndicadorProductoID,
		IPB.OrganizacionID,
		IPB.RangoMinimo,
		IPB.RangoMaximo,
		IPB.Activo
		, O.Descripcion		AS Organizacion
		, IP.IndicadorProductoID
		, IP.IndicadorID
		, IP.ProductoID
		, P.Descripcion		AS Producto
		, I.Descripcion		AS Indicador
	FROM IndicadorProductoBoleta IPB
	INNER JOIN IndicadorProducto IP
		ON (IPB.IndicadorProductoID = IP.IndicadorProductoID)
	INNER JOIN Organizacion O
		ON (IPB.OrganizacionID = O.OrganizacionID)
	INNER JOIN Producto P
		ON (IP.ProductoID = P.ProductoID)
	INNER JOIN Indicador I
		ON (IP.IndicadorID = I.IndicadorID)
	WHERE IP.ProductoID = @ProductoID
		AND IPB.OrganizacionID = @OrganizacionID
		and ipb.Activo = @Activo

	SET NOCOUNT OFF;
END

GO
