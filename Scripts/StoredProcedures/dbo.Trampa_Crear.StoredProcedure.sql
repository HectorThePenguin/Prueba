USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Trampa_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Trampa_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Trampa_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Trampa_Crear
--======================================================
CREATE PROCEDURE [dbo].[Trampa_Crear]
@Descripcion varchar(50),
@OrganizacionID int,
@TipoTrampa char,
@HostName varchar(50),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Trampa (
		Descripcion,
		OrganizacionID,
		TipoTrampa,
		HostName,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@Descripcion,
		@OrganizacionID,
		@TipoTrampa,
		@HostName,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
