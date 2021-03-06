USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoCalidad_ObtenerProductosPorIndicador]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorProductoCalidad_ObtenerProductosPorIndicador]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoCalidad_ObtenerProductosPorIndicador]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Juli�n Carranza Castro
-- Create date: 02/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorProductoCalidad_ObtenerProductosPorIndicador 1
--======================================================
CREATE PROCEDURE [dbo].[IndicadorProductoCalidad_ObtenerProductosPorIndicador] @IndicadorID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT DISTINCT IP.ProductoID
		,P.Descripcion AS Producto
	FROM IndicadorProductoCalidad IP
	INNER JOIN Producto P ON (IP.ProductoID = P.ProductoID)
	WHERE ip.IndicadorID = @IndicadorID
	SET NOCOUNT OFF;
END

GO
