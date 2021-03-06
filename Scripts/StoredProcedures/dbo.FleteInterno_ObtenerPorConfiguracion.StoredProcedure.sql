USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FleteInterno_ObtenerPorConfiguracion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FleteInterno_ObtenerPorConfiguracion]
GO
/****** Object:  StoredProcedure [dbo].[FleteInterno_ObtenerPorConfiguracion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 22/07/2014
-- Description: 
-- SpName     : FleteInterno_ObtenerPorConfiguracion 1, 22, 5, 13, 32, 1
--======================================================
CREATE PROCEDURE [dbo].[FleteInterno_ObtenerPorConfiguracion]
@OrganizacionID INT,
@TipoMovimientoID INT,
@AlmacenIDOrigen INT,
@AlmacenIDDestino INT,
@ProductoID INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		 FT.FleteInternoID,
		 FT.OrganizacionID AS OrganizacionIDFleteInterno,
		 O.Descripcion AS OrganizacionDescripcionOrigen,
		 O.TipoOrganizacionID,
		 FT.TipoMovimientoID,
		 TM.Descripcion,
		 FT.AlmacenIDOrigen,
		 AO.CodigoAlmacen AS CodigoAlmacenOrigen,
		 AO.Descripcion AS DescripcionAlmacenOrigen,
		 O2.OrganizacionID AS OrganizacionDestinoID,
		 O2.Descripcion AS OrganizacionDescripcionDestino,
		 FT.AlmacenIDDestino,
		 AD.CodigoAlmacen AS CodigoAlmacenDestino,
		 AD.Descripcion AS DescripcionAlmacenDestino,
		 FT.ProductoID,
		 P.Descripcion AS DescripcionProducto,
		 P.SubFamiliaID,
		 FT.Activo,
		 FT.FechaCreacion,
		 FT.UsuarioCreacionID
	FROM FleteInterno FT
		INNER JOIN Organizacion O
		ON (O.OrganizacionID = FT.OrganizacionID)
		INNER JOIN TipoMovimiento TM
			ON (TM.TipoMovimientoID = FT.TipoMovimientoID)
		INNER JOIN Almacen AO
			ON (AO.AlmacenID = FT.AlmacenIDOrigen)
		INNER JOIN Almacen AD
			ON (AD.AlmacenID = FT.AlmacenIDDestino)
	    INNER JOIN Organizacion O2
			ON (O2.OrganizacionID = AD.OrganizacionID)		
		INNER JOIN Producto P
			ON (P.ProductoID = FT.ProductoID)
	WHERE FT.OrganizacionID = @OrganizacionID
		AND FT.TipoMovimientoID = @TipoMovimientoID
		AND FT.AlmacenIDOrigen = @AlmacenIDOrigen
		AND FT.AlmacenIDDestino = @AlmacenIDDestino
		AND FT.ProductoID = @ProductoID
		AND FT.Activo = @Activo
	SET NOCOUNT OFF;
END

GO
