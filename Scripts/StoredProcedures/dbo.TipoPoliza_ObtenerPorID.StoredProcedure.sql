USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoPoliza_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoPoliza_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[TipoPoliza_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoPoliza_ObtenerPorID 1
--======================================================
CREATE PROCEDURE [dbo].[TipoPoliza_ObtenerPorID]
@TipoPolizaID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TipoPolizaID,
		Descripcion,
		ClavePoliza,
		Activo
	FROM TipoPoliza
	WHERE TipoPolizaID = @TipoPolizaID
	SET NOCOUNT OFF;
END

GO
