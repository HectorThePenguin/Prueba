USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[BoletaRecepcionForraje_ObtenerRangos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[BoletaRecepcionForraje_ObtenerRangos]
GO
/****** Object:  StoredProcedure [dbo].[BoletaRecepcionForraje_ObtenerRangos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Eduardo Cota
-- Create date: 16/12/2014
-- Description: Obtiene los rangos de humedad permitidos para el producto cuyo id es productoID
-- SpName     : BoletaRecepcionForraje_ObtenerRangos 4, 101, 1,2
--======================================================
CREATE PROCEDURE [dbo].[BoletaRecepcionForraje_ObtenerRangos] 
@indicadorID int,
@productoID int,
@activo bit,
@OrganizacionID int = 0
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		--IP.IndicadorProductoID, IP.IndicadorID, IP.ProductoID, IP.Activo, IPB.RangoMinimo, IPB.RangoMaximo
		IPB.RangoMinimo, IPB.RangoMaximo
	FROM 
		IndicadorProducto IP
		inner join IndicadorProductoBoleta IPB on IP.IndicadorProductoID = IPB.IndicadorProductoID
	WHERE
		IP.IndicadorID = @indicadorID and 
		IP.ProductoID = @productoID and
		IPB.Activo = @activo
		and @OrganizacionID IN (ipb.OrganizacionID, 0)
	SET NOCOUNT OFF;
END

GO
