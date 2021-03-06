USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Tratamiento_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Tratamiento_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Tratamiento_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 17/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Tratamiento_Crear
--======================================================
CREATE PROCEDURE [dbo].[Tratamiento_Crear]
@OrganizacionID int,
@CodigoTratamiento int,
@TipoTratamientoID int,
@Sexo char(1),
@RangoInicial int,
@RangoFinal int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Tratamiento (
		OrganizacionID,
		CodigoTratamiento,
		TipoTratamientoID,
		Sexo,
		RangoInicial,
		RangoFinal,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@OrganizacionID,
		@CodigoTratamiento,
		@TipoTratamientoID,
		@Sexo,
		@RangoInicial,
		@RangoFinal,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
