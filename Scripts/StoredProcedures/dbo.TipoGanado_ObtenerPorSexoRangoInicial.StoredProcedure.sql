USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoGanado_ObtenerPorSexoRangoInicial]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoGanado_ObtenerPorSexoRangoInicial]
GO
/****** Object:  StoredProcedure [dbo].[TipoGanado_ObtenerPorSexoRangoInicial]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================      
-- Author:    Marco Zamora      
-- Create date: 21/11/2013      
-- Description:  Guardar las preguntas realizadas en la Evaluacion de Riesgos de Corral      
-- Origen:	  APInterfaces      
-- =============================================      
CREATE PROCEDURE [dbo].[TipoGanado_ObtenerPorSexoRangoInicial]
	@Sexo CHAR(1),
	@RangoInicial DECIMAL
AS
BEGIN	
SET NOCOUNT ON;	
	SELECT TG.TipoGanadoID,TG.Descripcion, TC.RangoFinal AS PesoMaximo
	FROM TipoGanado TG
	INNER JOIN TipoGanadoCorrales TC ON TG.TipoGanadoID = TC.TipoGanadoID
	WHERE TG.sexo = @Sexo 
	AND TC.RangoInicial = @RangoInicial
SET NOCOUNT OFF;	
END

GO
