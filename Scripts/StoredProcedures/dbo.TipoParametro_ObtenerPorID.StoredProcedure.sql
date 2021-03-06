USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoParametro_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoParametro_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[TipoParametro_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: Obtiene un tipo de parametro por ID 
-- SpName     : TipoParametro_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[TipoParametro_ObtenerPorID]
@TipoParametroID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TipoParametroID,
		Descripcion,
		Activo
	FROM TipoParametro
	WHERE TipoParametroID = @TipoParametroID
	SET NOCOUNT OFF;
END

GO
