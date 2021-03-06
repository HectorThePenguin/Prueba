USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Moneda_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Moneda_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Moneda_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 17/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Moneda_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[Moneda_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		MonedaID,
		Descripcion,
		Abreviatura,
		Activo
	FROM Moneda
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
