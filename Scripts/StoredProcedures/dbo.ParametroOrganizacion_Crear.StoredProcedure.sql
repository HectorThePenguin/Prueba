USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ParametroOrganizacion_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ParametroOrganizacion_Crear]
GO
/****** Object:  StoredProcedure [dbo].[ParametroOrganizacion_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ParametroOrganizacion_Crear
--======================================================
CREATE PROCEDURE [dbo].[ParametroOrganizacion_Crear]
@ParametroID int,
@OrganizacionID int,
@Valor varchar(100),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT ParametroOrganizacion (
		ParametroID,
		OrganizacionID,
		Valor,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@ParametroID,
		@OrganizacionID,
		@Valor,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
