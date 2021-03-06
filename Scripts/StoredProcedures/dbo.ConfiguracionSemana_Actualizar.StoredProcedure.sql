USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionSemana_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionSemana_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionSemana_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 17/02/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ConfiguracionSemana_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[ConfiguracionSemana_Actualizar]
@ConfiguracionSemanaID int,
@OrganizacionID int,
@InicioSemana varchar(50),
@FinSemana varchar(50),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE ConfiguracionSemana SET
		OrganizacionID = @OrganizacionID,
		InicioSemana = @InicioSemana,
		FinSemana = @FinSemana,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE ConfiguracionSemanaID = @ConfiguracionSemanaID
	SET NOCOUNT OFF;
END

GO
