USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CalidadMezcladoFormulasAlimento_ObtenerCamposAnalisMuestra]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CalidadMezcladoFormulasAlimento_ObtenerCamposAnalisMuestra]
GO
/****** Object:  StoredProcedure [dbo].[CalidadMezcladoFormulasAlimento_ObtenerCamposAnalisMuestra]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Eduardo Cota
-- Create date: 10/Noviembre/2014
-- Description:  Consulta de 
-- CalidadMezcladoFormulasAlimento_ObtenerCamposAnalisMuestra
-- =============================================
CREATE PROCEDURE [dbo].[CalidadMezcladoFormulasAlimento_ObtenerCamposAnalisMuestra]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  
	      Descripcion
	 FROM TipoMuestra P (NOLOCK) 
	SET NOCOUNT OFF;
END

GO
