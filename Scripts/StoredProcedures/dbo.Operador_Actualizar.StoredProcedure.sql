USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Operador_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Operador_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Operador_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 04/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Operador_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[Operador_Actualizar]
@OperadorID int,
@Nombre varchar(50),
@ApellidoPaterno varchar(50),
@ApellidoMaterno varchar(50),
@CodigoSAP char(8),
@RolID int,
@UsuarioID int,
@OrganizacionID int,
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Operador SET
		Nombre = @Nombre,
		ApellidoPaterno = @ApellidoPaterno,
		ApellidoMaterno = @ApellidoMaterno,
		CodigoSAP = @CodigoSAP,
		RolID = @RolID,
		UsuarioID = @UsuarioID,
		OrganizacionID = @OrganizacionID,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE OperadorID = @OperadorID
	SET NOCOUNT OFF;
END

GO
