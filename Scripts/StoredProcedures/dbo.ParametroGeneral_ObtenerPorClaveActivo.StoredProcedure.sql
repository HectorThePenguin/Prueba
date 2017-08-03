USE [SIAP]
GO

DROP PROCEDURE [dbo].[ParametroGeneral_ObtenerPorClaveActivo]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Daniel Benitez
-- Create date: 19/01/2017
-- Description: Obtiene un Parametro General solo cuando toda la configuracion esta activa
-- ParametroGeneral_ObtenerPorClaveActivo 'ProductoConRestriccionDescSK'
-- =============================================
CREATE PROCEDURE [dbo].[ParametroGeneral_ObtenerPorClaveActivo]	
	@Clave VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON
	SELECT PG.ParametroGeneralID
		   , PG.ParametroID		   
		   , PG.Valor
		   , PG.Activo
		   --, P.Descripcion AS Parametro		   
	 FROM ParametroGeneral PG
	 INNER JOIN Parametro P
		ON (P.ParametroID = PG.ParametroID
			AND P.Clave = @Clave			
			AND PG.Activo = 1 AND P.Activo = 1)	 
	SET NOCOUNT OFF
END

GO
