USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Pregunta_ObtenerIDFormulario]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Pregunta_ObtenerIDFormulario]
GO
/****** Object:  StoredProcedure [dbo].[Pregunta_ObtenerIDFormulario]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Leonel ayala flores
-- Origen:    APInterfaces
-- Create date: 21/11/2013
-- Description:  Consulta de Preguntas.
-- Empresa: Apinterfaces
-- EXEC Pregunta_ObtenerIDFormulario 1
-- =============================================
CREATE PROCEDURE [dbo].[Pregunta_ObtenerIDFormulario]		
@FormularioId INT	
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		PreguntaID,
		TipoPreguntaID,
		Descripcion,
        '' TipoDato,
		Valor 
	FROM Pregunta 
	WHERE  TipoPreguntaID=@FormularioId 
	AND Activo=1
	ORDER BY Orden ASC;
	SET NOCOUNT OFF;
END

GO
