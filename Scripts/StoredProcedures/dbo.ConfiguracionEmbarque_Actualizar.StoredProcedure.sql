USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionEmbarque_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionEmbarque_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionEmbarque_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 13/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ConfiguracionEmbarque_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[ConfiguracionEmbarque_Actualizar]
@ConfiguracionEmbarqueID int,
@OrganizacionOrigenID int,
@OrganizacionDestinoID int,
@Kilometros decimal(10,2),
@Horas decimal(10,2),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE ConfiguracionEmbarque SET
		OrganizacionOrigenID = @OrganizacionOrigenID,
		OrganizacionDestinoID = @OrganizacionDestinoID,
		Kilometros = @Kilometros,
		Horas = @Horas,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE ConfiguracionEmbarqueID = @ConfiguracionEmbarqueID
	SET NOCOUNT OFF;
END

GO
