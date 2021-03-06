USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Parametro_ObtenerPorTrampaTipoParametro]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Parametro_ObtenerPorTrampaTipoParametro]
GO
/****** Object:  StoredProcedure [dbo].[Parametro_ObtenerPorTrampaTipoParametro]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: C�sar Valdez
-- Create date: 12/02/2013
-- Description: Obtiene la lista de Parametros por organizacion y tipo de parametro
-- Empresa: Apinterfaces
-- EXEC Parametro_ObtenerPorTrampaTipoParametro 1,1
-- =============================================
CREATE PROCEDURE [dbo].[Parametro_ObtenerPorTrampaTipoParametro]
	@TrampaID INT,
	@TipoParametroID INT
AS
BEGIN
	SELECT PTrampa.ParametroID, 
		   PTrampa.Valor,
		   P.Clave, 
		   P.Descripcion, 
		   PTrampa.TrampaID
	 FROM ParametroTrampa PTrampa
	INNER JOIN Parametro P ON P.ParametroID = PTrampa.ParametroID 
	INNER JOIN TipoParametro TP ON TP.TipoParametroID = P.TipoParametroID
	WHERE PTrampa.TrampaID = @TrampaID
	  AND P.TipoParametroID = @TipoParametroID
	  AND PTrampa.Activo = 1
END

GO
