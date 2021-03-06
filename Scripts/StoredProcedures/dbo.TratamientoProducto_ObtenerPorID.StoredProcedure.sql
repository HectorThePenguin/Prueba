USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoProducto_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TratamientoProducto_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoProducto_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 14/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TratamientoProducto_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[TratamientoProducto_ObtenerPorID]
@TratamientoProductoID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TP.TratamientoProductoID,
		TP.TratamientoID,
		TP.ProductoID,
		TP.Dosis,
		TP.Activo
		, T.CodigoTratamiento AS Tratamiento
		, P.Descripcion AS Producto
	FROM TratamientoProducto TP
	INNER JOIN Tratamiento T
		ON (TP.TratamientoID = T.TratamientoID)
	INNER JOIN Producto P
		ON (TP.ProductoID = P.ProductoID)
	WHERE TP.TratamientoProductoID = @TratamientoProductoID
	SET NOCOUNT OFF;
END

GO
