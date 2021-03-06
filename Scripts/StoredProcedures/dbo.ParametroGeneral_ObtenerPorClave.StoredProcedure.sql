USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ParametroGeneral_ObtenerPorClave]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ParametroGeneral_ObtenerPorClave]
GO
/****** Object:  StoredProcedure [dbo].[ParametroGeneral_ObtenerPorClave]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jorge Luis Velazquez Araujo
-- Create date: 26/07/2014
-- Description: Obtiene un Parametro General
-- ParametroGeneral_ObtenerPorClave 'MesesAtrasReporteInventario'
-- =============================================
CREATE PROCEDURE [dbo].[ParametroGeneral_ObtenerPorClave]	
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
			AND PG.Activo = 1)	 
	SET NOCOUNT OFF
END

GO
