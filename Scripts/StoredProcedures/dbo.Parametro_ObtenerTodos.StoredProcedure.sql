USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Parametro_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Parametro_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Parametro_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: Obtiene una lista de todos los parametros
-- SpName     : Parametro_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[Parametro_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ParametroID,
		P.TipoParametroID,
		P.Descripcion,
		TP.descripcion TipoParametro, 
		Clave,
		P.Activo
	FROM Parametro P 
	INNER JOIN TipoParametro TP 
		ON P.TipoParametroID = TP.TipoParametroID
	WHERE P.Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
