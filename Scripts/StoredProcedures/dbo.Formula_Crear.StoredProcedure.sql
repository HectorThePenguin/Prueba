USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Formula_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Formula_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Formula_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Formula_Crear
--======================================================
CREATE PROCEDURE [dbo].[Formula_Crear]
@Descripcion varchar(50),
@TipoFormulaID int,
@ProductoID int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Formula (
		Descripcion,
		TipoFormulaID,
		ProductoID,
		Activo,		
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@Descripcion,
		@TipoFormulaID,
		@ProductoID,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
