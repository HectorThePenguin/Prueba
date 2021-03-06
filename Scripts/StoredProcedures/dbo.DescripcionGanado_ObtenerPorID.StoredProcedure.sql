USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DescripcionGanado_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DescripcionGanado_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[DescripcionGanado_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 03/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : DescripcionGanado_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[DescripcionGanado_ObtenerPorID]
@DescripcionGanadoID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		DescripcionGanadoID,
		Descripcion,
		Activo
	FROM DescripcionGanado
	WHERE DescripcionGanadoID = @DescripcionGanadoID
	SET NOCOUNT OFF;
END

GO
