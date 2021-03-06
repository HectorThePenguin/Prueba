USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CalidadGanado_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CalidadGanado_Crear]
GO
/****** Object:  StoredProcedure [dbo].[CalidadGanado_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 16/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CalidadGanado_Crear
--======================================================
CREATE PROCEDURE [dbo].[CalidadGanado_Crear]
@Descripcion varchar(50),
@Sexo char(1),
@Calidad varchar(10),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT CalidadGanado (
		Descripcion,
		Calidad,
		Sexo,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@Descripcion,
		@Calidad,
		@Sexo,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SET NOCOUNT OFF;
END

GO
