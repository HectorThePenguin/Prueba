USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Moneda_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Moneda_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Moneda_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 17/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Moneda_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[Moneda_ObtenerPorDescripcion]
@Descripcion varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		MonedaID,
		Descripcion,
		Abreviatura,
		Activo
	FROM Moneda
	WHERE Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
