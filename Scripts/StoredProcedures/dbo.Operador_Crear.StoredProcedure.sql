USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Operador_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Operador_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Operador_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 04/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Operador_Crear
--======================================================
CREATE PROCEDURE [dbo].[Operador_Crear]
@Nombre varchar(50),
@ApellidoPaterno varchar(50),
@ApellidoMaterno varchar(50),
@CodigoSAP char(8),
@RolID int,
@UsuarioID int,
@OrganizacionID int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Operador (
		Nombre,
		ApellidoPaterno,
		ApellidoMaterno,
		CodigoSAP,
		RolID,
		UsuarioID,
		OrganizacionID,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@Nombre,
		@ApellidoPaterno,
		@ApellidoMaterno,
		@CodigoSAP,
		@RolID,
		@UsuarioID,
		@OrganizacionID,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SET NOCOUNT OFF;
END

GO
