USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Trampa_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Trampa_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Trampa_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Trampa_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[Trampa_Actualizar]
@TrampaID int,
@Descripcion varchar(50),
@OrganizacionID int,
@TipoTrampa char,
@HostName varchar(50),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Trampa SET
		Descripcion = @Descripcion,
		OrganizacionID = @OrganizacionID,
		TipoTrampa = @TipoTrampa,
		HostName = @HostName,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE TrampaID = @TrampaID
	SET NOCOUNT OFF;
END

GO
