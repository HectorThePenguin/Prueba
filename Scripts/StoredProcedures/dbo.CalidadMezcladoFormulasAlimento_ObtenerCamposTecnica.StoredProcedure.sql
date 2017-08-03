USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CalidadMezcladoFormulasAlimento_ObtenerCamposTecnica]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CalidadMezcladoFormulasAlimento_ObtenerCamposTecnica]
GO
/****** Object:  StoredProcedure [dbo].[CalidadMezcladoFormulasAlimento_ObtenerCamposTecnica]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Eduardo Cota
-- Create date: 10/Noviembre/2014
-- Description:  Consulta de 
-- CalidadMezcladoFormulasAlimento_ObtenerCamposTecnica
-- =============================================
CREATE PROCEDURE [dbo].[CalidadMezcladoFormulasAlimento_ObtenerCamposTecnica]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  
	      Descripcion,
	      TipoTecnicaID
	 FROM TipoTecnica P (NOLOCK) 
	SET NOCOUNT OFF;
END

GO
