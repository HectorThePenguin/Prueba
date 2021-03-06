USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Sociedad_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Sociedad_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Sociedad_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Rub�n Guzman
-- Create date: 19/08/2015
-- Description:  Obtiene las Sociedades
-- Sociedad_ObtenerTodos
-- =============================================
CREATE PROCEDURE [dbo].[Sociedad_ObtenerTodos]
	@SociedadID INT = NULL
AS
BEGIN
SET NOCOUNT ON;
	SELECT  
		SociedadID,
		Descripcion,
		PaisID,
		Activo  
	FROM Sociedad (NOLOCK)  
	WHERE Activo = 1 AND SociedadID = ISNULL(@SociedadID, SociedadID)  
 SET NOCOUNT OFF;
END

GO
