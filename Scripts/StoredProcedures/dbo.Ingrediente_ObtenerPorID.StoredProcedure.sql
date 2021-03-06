USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Ingrediente_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Ingrediente_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Ingrediente_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Pedro Delgado
-- Create date: 11/06/2014
-- Description:  Obtiene el ingrediente por id
--	
-- =============================================
CREATE PROCEDURE [dbo].[Ingrediente_ObtenerPorID]
@IngredienteID INT,
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
	WHERE IngredienteID = @IngredienteID AND (Activo = @Activo OR @Activo = 2)
END

GO
