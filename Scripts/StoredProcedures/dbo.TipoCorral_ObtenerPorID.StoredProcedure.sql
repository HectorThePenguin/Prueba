USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoCorral_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoCorral_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[TipoCorral_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 16/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoCorral_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[TipoCorral_ObtenerPorID]
@TipoCorralID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TipoCorralID,
		Descripcion,
		Activo
	FROM TipoCorral
	WHERE TipoCorralID = @TipoCorralID
	SET NOCOUNT OFF;
END

GO
