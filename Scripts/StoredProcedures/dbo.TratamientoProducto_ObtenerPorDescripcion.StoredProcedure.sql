USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoProducto_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TratamientoProducto_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoProducto_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 14/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TratamientoProducto_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[TratamientoProducto_ObtenerPorDescripcion]
@TratamientoID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TratamientoProductoID,
		TratamientoID,
		ProductoID,
		Dosis,
		Activo
	FROM TratamientoProducto
	WHERE TratamientoID = @TratamientoID
	SET NOCOUNT OFF;
END

GO
