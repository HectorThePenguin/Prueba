USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Parametro_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Parametro_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Parametro_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: Obtiene un parametro por descripcion 
-- SpName     : Parametro_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[Parametro_ObtenerPorDescripcion]
@Descripcion varchar(50)
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
	WHERE P.Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
