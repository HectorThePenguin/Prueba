USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TiempoMuerto_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TiempoMuerto_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[TiempoMuerto_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 24/06/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TiempoMuerto_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[TiempoMuerto_Actualizar]
@TiempoMuertoID int,
@ProduccionDiariaID int,
@RepartoAlimentoID int,
@HoraInicio varchar(5),
@HoraFin varchar(5),
@CausaTiempoMuertoID int,
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE TiempoMuerto SET
		ProduccionDiariaID = @ProduccionDiariaID,
		RepartoAlimentoID = @RepartoAlimentoID,
		HoraInicio = @HoraInicio,
		HoraFin = @HoraFin,
		CausaTiempoMuertoID = @CausaTiempoMuertoID,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE TiempoMuertoID = @TiempoMuertoID
	SET NOCOUNT OFF;
END

GO
