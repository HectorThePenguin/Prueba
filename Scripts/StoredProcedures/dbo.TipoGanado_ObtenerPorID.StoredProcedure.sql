USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoGanado_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoGanado_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[TipoGanado_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 20/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoGanado_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[TipoGanado_ObtenerPorID]
@TipoGanadoID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TipoGanadoID,
		Descripcion,
		Sexo,
		PesoMinimo,
		PesoMaximo,
		Activo
	FROM TipoGanado
	WHERE TipoGanadoID = @TipoGanadoID
	SET NOCOUNT OFF;
END

GO
