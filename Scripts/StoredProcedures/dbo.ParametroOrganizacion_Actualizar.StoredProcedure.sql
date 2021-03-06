USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ParametroOrganizacion_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ParametroOrganizacion_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[ParametroOrganizacion_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ParametroOrganizacion_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[ParametroOrganizacion_Actualizar]
@ParametroOrganizacionID int,
@ParametroID int,
@OrganizacionID int,
@Valor varchar(100),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE ParametroOrganizacion SET
		ParametroID = @ParametroID,
		OrganizacionID = @OrganizacionID,
		Valor = @Valor,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE ParametroOrganizacionID = @ParametroOrganizacionID
	SET NOCOUNT OFF;
END

GO
