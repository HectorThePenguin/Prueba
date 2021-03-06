USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ColorObjetivo_ObtenerSemaforo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ColorObjetivo_ObtenerSemaforo]
GO
/****** Object:  StoredProcedure [dbo].[ColorObjetivo_ObtenerSemaforo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/06/16
-- Description: Procedimiento almacenado para obtener el semaforo de indicador objetivo
-- ColorObjetivo_ObtenerSemaforo
--=============================================
CREATE PROCEDURE [dbo].[ColorObjetivo_ObtenerSemaforo]
AS
BEGIN
	SET NOCOUNT ON;
		SELECT TOC.TipoObjetivoCalidadID
			,  TOC.Descripcion			
			,  CO.Tendencia
			,  CO.CodigoColor
		FROM ColorObjetivo CO
		INNER JOIN TipoObjetivoCalidad TOC
			ON (CO.TipoObjetivoCalidadID = TOC.TipoObjetivoCalidadID
				AND CO.Activo = 1)
	SET NOCOUNT OFF;
END

GO
