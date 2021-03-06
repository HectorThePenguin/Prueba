USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Ingrediente_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Ingrediente_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Ingrediente_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 05/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Ingrediente_Crear
--======================================================
CREATE PROCEDURE [dbo].[Ingrediente_Crear]
@OrganizacionID int,
@FormulaID int,
@ProductoID int,
@PorcentajeProgramado decimal(10,2),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Ingrediente (
		OrganizacionID,
		FormulaID,
		ProductoID,
		PorcentajeProgramado,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@OrganizacionID,
		@FormulaID,
		@ProductoID,
		@PorcentajeProgramado,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
