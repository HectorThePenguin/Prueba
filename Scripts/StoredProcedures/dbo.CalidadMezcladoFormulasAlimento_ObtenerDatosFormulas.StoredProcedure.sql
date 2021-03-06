USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CalidadMezcladoFormulasAlimento_ObtenerDatosFormulas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CalidadMezcladoFormulasAlimento_ObtenerDatosFormulas]
GO
/****** Object:  StoredProcedure [dbo].[CalidadMezcladoFormulasAlimento_ObtenerDatosFormulas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Eduardo Cota
-- Create date: 12/Noviembre/2014
-- Description:  Consulta de 
-- CalidadMezcladoFormulasAlimento_ObtenerDatosFormulas
-- =============================================
CREATE PROCEDURE [dbo].[CalidadMezcladoFormulasAlimento_ObtenerDatosFormulas]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  
	      CMF.TipoMuestraID, CMF.PesoBaseHumeda, CMF.PesoBaseSeca, TM.Descripcion, CMF.Factor
	 FROM 
		CalidadMezcladoFactor CMF inner join TipoMuestra TM on CMF.TipoMuestraID = TM.TipoMuestraID
	SET NOCOUNT OFF;
END

GO
