USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProducto_ObtenerPorIndicadorProducto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorProducto_ObtenerPorIndicadorProducto]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProducto_ObtenerPorIndicadorProducto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Juli�n Carranza Castro
-- Create date: 02/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorProducto_ObtenerPorIndicadorProducto
--======================================================
CREATE PROCEDURE [dbo].[IndicadorProducto_ObtenerPorIndicadorProducto]
@IndicadorID INT,
@ProductoID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		IP.IndicadorProductoID,
		IP.IndicadorID,
		IP.ProductoID,
		IP.Activo
		, P.Descripcion			AS Producto
		, I.Descripcion			AS Indicador
	FROM IndicadorProducto IP
	INNER JOIN Producto P
		ON (IP.ProductoID = P.ProductoID
			AND P.ProductoID = @ProductoID)
	INNER JOIN Indicador I
		ON (IP.IndicadorID = I.IndicadorID
			AND I.IndicadorID = @IndicadorID)
	SET NOCOUNT OFF;
END

GO
