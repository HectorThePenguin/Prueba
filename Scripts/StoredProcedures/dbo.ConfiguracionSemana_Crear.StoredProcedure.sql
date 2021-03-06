USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionSemana_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionSemana_Crear]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionSemana_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 17/02/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ConfiguracionSemana_Crear
--======================================================
CREATE PROCEDURE [dbo].[ConfiguracionSemana_Crear]
@OrganizacionID int,
@InicioSemana varchar(50),
@FinSemana varchar(50),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT ConfiguracionSemana (
		OrganizacionID,
		InicioSemana,
		FinSemana,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@OrganizacionID,
		@InicioSemana,
		@FinSemana,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
