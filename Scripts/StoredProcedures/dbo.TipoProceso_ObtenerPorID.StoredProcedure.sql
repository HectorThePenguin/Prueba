USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoProceso_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoProceso_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[TipoProceso_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 27/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoProceso_ObtenerPorID 1
--======================================================
CREATE PROCEDURE [dbo].[TipoProceso_ObtenerPorID]
@TipoProcesoID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TipoProcesoID,
		Descripcion,
		Activo
	FROM TipoProceso
	WHERE TipoProcesoID = @TipoProcesoID
	SET NOCOUNT OFF;
END

GO
