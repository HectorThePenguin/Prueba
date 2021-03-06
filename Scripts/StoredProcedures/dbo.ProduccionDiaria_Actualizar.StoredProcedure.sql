USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionDiaria_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionDiaria_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionDiaria_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 24/06/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ProduccionDiaria_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[ProduccionDiaria_Actualizar]
@ProduccionDiariaID int,
@Turno int,
@LitrosInicial decimal(14,2),
@LitrosFinal decimal(14,2),
@HorometroInicial int,
@HorometroFinal int,
@FechaProduccion smalldatetime,
@UsuarioIDAutorizo int,
@Observaciones varchar(255),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE ProduccionDiaria SET
		Turno = @Turno,
		LitrosInicial = @LitrosInicial,
		LitrosFinal = @LitrosFinal,
		HorometroInicial = @HorometroInicial,
		HorometroFinal = @HorometroFinal,
		FechaProduccion = @FechaProduccion,
		UsuarioIDAutorizo = @UsuarioIDAutorizo,
		Observaciones = @Observaciones,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE ProduccionDiariaID = @ProduccionDiariaID
	SET NOCOUNT OFF;
END

GO
