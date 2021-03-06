USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraGeneral_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoladoraGeneral_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraGeneral_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 23/06/2014
-- Description:  Obtener CheckList
-- CheckListRoladoraGeneral_Guardar
-- =============================================
CREATE PROCEDURE [dbo].[CheckListRoladoraGeneral_Guardar]
@Turno INT
, @FechaInicio DATETIME
, @UsuarioIDSupervisor INT
, @Observaciones VARCHAR(50)
, @SurfactanteInicio DECIMAL(14,2)
, @SurfactanteFin DECIMAL(14,2)
, @ContadorAguaInicio DECIMAL(14,2)
, @ContadorAguaFin DECIMAL(14,2)
, @GranoEnteroFinal DECIMAL(14,2)
, @Activo BIT
, @UsuarioCreacion INT
AS
BEGIN
	INSERT INTO CheckListRoladoraGeneral
	(
		Turno
		, FechaInicio
		, UsuarioIDSupervisor
		, Observaciones
		, SurfactanteInicio
		, SurfactanteFin
		, ContadorAguaInicio
		, ContadorAguaFin
		, GranoEnteroFinal
		, Activo
		, FechaCreacion
		, UsuarioCreacionID
	)
	VALUES
	(
		@Turno
		, @FechaInicio
		, @UsuarioIDSupervisor
		, @Observaciones
		, @SurfactanteInicio
		, @SurfactanteFin
		, @ContadorAguaInicio
		, @ContadorAguaFin
		, @GranoEnteroFinal
		, @Activo
		, GETDATE()
		, @UsuarioCreacion
	)
END

GO
