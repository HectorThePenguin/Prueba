USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Grado_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Grado_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Grado_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 29/04/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Grado_Crear
--======================================================
CREATE PROCEDURE [dbo].[Grado_Crear]
@Descripcion varchar(50),
@NivelGravedad char(1),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Grado (
		Descripcion,
		NivelGravedad,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@Descripcion,
		@NivelGravedad,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
