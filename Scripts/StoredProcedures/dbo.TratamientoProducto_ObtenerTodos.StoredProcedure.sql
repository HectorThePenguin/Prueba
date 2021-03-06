USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoProducto_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TratamientoProducto_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoProducto_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 14/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TratamientoProducto_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[TratamientoProducto_ObtenerTodos]
@Activo BIT = NULL
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
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
