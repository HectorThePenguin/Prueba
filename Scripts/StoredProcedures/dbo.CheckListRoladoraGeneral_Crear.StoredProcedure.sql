USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraGeneral_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoladoraGeneral_Crear]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraGeneral_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 03/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CheckListRoladoraGeneral_Crear
--======================================================
CREATE PROCEDURE [dbo].[CheckListRoladoraGeneral_Crear]
@CheckListRoladoraGeneralID int,
@Turno int,
@FechaInicio datetime,
@UsuarioIDSupervisor int,
@Observaciones varchar(255),
@SurfactanteInicio decimal(14,2),
@SurfactanteFin decimal(14,2),
@ContadorAguaInicio decimal(14,2),
@ContadorAguaFin decimal(14,2),
@GranoEnteroFinal decimal(14,2),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT CheckListRoladoraGeneral (
		Turno,
		FechaInicio,
		UsuarioIDSupervisor,
		Observaciones,
		SurfactanteInicio,
		SurfactanteFin,
		ContadorAguaInicio,
		ContadorAguaFin,
		GranoEnteroFinal,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@Turno,
		GETDATE(),--@FechaInicio,
		@UsuarioIDSupervisor,
		@Observaciones,
		@SurfactanteInicio,
		@SurfactanteFin,
		@ContadorAguaInicio,
		@ContadorAguaFin,
		@GranoEnteroFinal,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
