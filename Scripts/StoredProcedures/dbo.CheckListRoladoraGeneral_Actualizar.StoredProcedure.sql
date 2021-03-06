USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraGeneral_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoladoraGeneral_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraGeneral_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 03/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CheckListRoladoraGeneral_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[CheckListRoladoraGeneral_Actualizar]
@CheckListRoladoraGeneralID int,
@Turno int,
@UsuarioIDSupervisor int,
@Observaciones varchar(255),
@SurfactanteInicio decimal(14,2),
@SurfactanteFin decimal(14,2),
@ContadorAguaInicio decimal(14,2),
@ContadorAguaFin decimal(14,2),
@GranoEnteroFinal decimal(14,2),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE CheckListRoladoraGeneral SET
		Turno = @Turno,
		UsuarioIDSupervisor = @UsuarioIDSupervisor,
		Observaciones = @Observaciones,
		SurfactanteInicio = @SurfactanteInicio,
		SurfactanteFin = @SurfactanteFin,
		ContadorAguaInicio = @ContadorAguaInicio,
		ContadorAguaFin = @ContadorAguaFin,
		GranoEnteroFinal = @GranoEnteroFinal,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE CheckListRoladoraGeneralID = @CheckListRoladoraGeneralID
	SET NOCOUNT OFF;
END

GO
