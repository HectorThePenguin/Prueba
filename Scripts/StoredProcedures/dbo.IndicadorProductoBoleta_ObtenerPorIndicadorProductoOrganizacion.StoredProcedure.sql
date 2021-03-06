USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoBoleta_ObtenerPorIndicadorProductoOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorProductoBoleta_ObtenerPorIndicadorProductoOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoBoleta_ObtenerPorIndicadorProductoOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Juli�n Carranza Castro
-- Create date: 24/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorProductoBoleta_ObtenerPorIndicadorProductoOrganizacion 1, 1
--======================================================
CREATE PROCEDURE [dbo].[IndicadorProductoBoleta_ObtenerPorIndicadorProductoOrganizacion]
@IndicadorProductoID INT
, @OrganizacionID	 INT
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
		ON (IP.UsuarioCreacionID = I.IndicadorID)
	WHERE IPB.IndicadorProductoID = @IndicadorProductoID
		AND IPB.OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

GO
