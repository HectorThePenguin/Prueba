USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RepartoAlimento_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RepartoAlimento_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[RepartoAlimento_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 14/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : RepartoAlimento_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[RepartoAlimento_Actualizar]
@RepartoAlimentoID int,
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
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE RepartoAlimento SET
		TipoServicioID = @TipoServicioID,
		CamionRepartoID = @CamionRepartoID,
		UsuarioIDReparto = @UsuarioIDReparto,
		HorometroInicial = @HorometroInicial,
		HorometroFinal = @HorometroFinal,
		OdometroInicial = @OdometroInicial,
		OdometroFinal = @OdometroFinal,
		LitrosDiesel = @LitrosDiesel,
		FechaReparto = @FechaReparto,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE RepartoAlimentoID = @RepartoAlimentoID
	SET NOCOUNT OFF;
END

GO
