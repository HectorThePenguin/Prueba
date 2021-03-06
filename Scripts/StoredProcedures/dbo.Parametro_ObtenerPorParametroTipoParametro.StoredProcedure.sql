USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Parametro_ObtenerPorParametroTipoParametro]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Parametro_ObtenerPorParametroTipoParametro]
GO
/****** Object:  StoredProcedure [dbo].[Parametro_ObtenerPorParametroTipoParametro]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 06/03/2014 12:00:00 a.m.
-- Description: Obtiene un elemento parametro por tipo parametro
-- SpName     : Parametro_ObtenerPorParametroTipoParametro 1, 1, 1
--======================================================
CREATE PROCEDURE [dbo].[Parametro_ObtenerPorParametroTipoParametro]
@ParametroID INT,
@TipoParametroID INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ParametroID,
		P.TipoParametroID,
		TP.Descripcion TipoParametro,
		P.Descripcion,
		Clave,
		P.Activo
	FROM Parametro P 
	INNER JOIN TipoParametro TP 
		ON P.TipoParametroID = TP.TipoParametroID 
	WHERE P.TipoParametroID = @TipoParametroID
		AND P.ParametroID = @ParametroID
		AND P.Activo = @Activo
	SET NOCOUNT OFF;
END

GO
