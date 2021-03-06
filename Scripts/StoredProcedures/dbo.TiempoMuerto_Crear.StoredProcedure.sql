USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TiempoMuerto_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TiempoMuerto_Crear]
GO
/****** Object:  StoredProcedure [dbo].[TiempoMuerto_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 24/06/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TiempoMuerto_Crear
--======================================================
CREATE PROCEDURE [dbo].[TiempoMuerto_Crear]
@TiempoMuertoID int,
@ProduccionDiariaID int,
@RepartoAlimentoID int,
@HoraInicio varchar(5),
@HoraFin varchar(5),
@CausaTiempoMuertoID int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT TiempoMuerto (
		TiempoMuertoID,
		ProduccionDiariaID,
		RepartoAlimentoID,
		HoraInicio,
		HoraFin,
		CausaTiempoMuertoID,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@TiempoMuertoID,
		@ProduccionDiariaID,
		@RepartoAlimentoID,
		@HoraInicio,
		@HoraFin,
		@CausaTiempoMuertoID,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
