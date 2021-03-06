USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DescripcionGanado_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DescripcionGanado_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[DescripcionGanado_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 03/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : DescripcionGanado_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[DescripcionGanado_ObtenerPorDescripcion]
@Descripcion varchar(255)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		DescripcionGanadoID,
		Descripcion,
		Activo
	FROM DescripcionGanado
	WHERE Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
