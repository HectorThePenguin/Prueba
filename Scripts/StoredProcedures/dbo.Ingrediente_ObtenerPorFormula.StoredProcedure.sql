USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Ingrediente_ObtenerPorFormula]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Ingrediente_ObtenerPorFormula]
GO
/****** Object:  StoredProcedure [dbo].[Ingrediente_ObtenerPorFormula]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Pedro Delgado
-- Create date: 11/06/2014
-- Description:  Obtiene los ingredientes de una formula
--	
-- =============================================
CREATE PROCEDURE [dbo].[Ingrediente_ObtenerPorFormula]
@FormulaID INT,
@OrganizacionID INT,
@Activo INT = 1
AS
BEGIN
	SELECT 
		IngredienteID,
		OrganizacionID,
		FormulaID,
		ProductoID,
		PorcentajeProgramado,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID
	FROM Ingrediente (NOLOCK) I
	WHERE FormulaID = @FormulaID AND OrganizacionID = @OrganizacionID AND (Activo = @Activo OR @Activo = 2)
END

GO
