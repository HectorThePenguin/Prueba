USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Ingrediente_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Ingrediente_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Ingrediente_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 05/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Ingrediente_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[Ingrediente_Actualizar]
@IngredienteID int,
@OrganizacionID int,
@FormulaID int,
@ProductoID int,
@PorcentajeProgramado decimal(10,2),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Ingrediente SET
		OrganizacionID = @OrganizacionID,
		FormulaID = @FormulaID,
		ProductoID = @ProductoID,
		PorcentajeProgramado = @PorcentajeProgramado,
		Activo = @Activo,
		UsuarioCreacionID = @UsuarioCreacionID,
		FechaModificacion = GETDATE()
	WHERE IngredienteID = @IngredienteID
	SET NOCOUNT OFF;
END

GO
