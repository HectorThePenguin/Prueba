USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RepartoAlimento_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RepartoAlimento_Crear]
GO
/****** Object:  StoredProcedure [dbo].[RepartoAlimento_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 14/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : RepartoAlimento_Crear
--======================================================
CREATE PROCEDURE [dbo].[RepartoAlimento_Crear]
@TipoServicioID int,
@CamionRepartoID int,
@UsuarioIDReparto int,
@HorometroInicial int,
@HorometroFinal int,
@OdometroInicial int,
@OdometroFinal int,
@LitrosDiesel int,
@FechaReparto smalldatetime,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT RepartoAlimento (
		TipoServicioID,
		CamionRepartoID,
		UsuarioIDReparto,
		HorometroInicial,
		HorometroFinal,
		OdometroInicial,
		OdometroFinal,
		LitrosDiesel,
		FechaReparto,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@TipoServicioID,
		@CamionRepartoID,
		@UsuarioIDReparto,
		@HorometroInicial,
		@HorometroFinal,
		@OdometroInicial,
		@OdometroFinal,
		@LitrosDiesel,
		@FechaReparto,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
