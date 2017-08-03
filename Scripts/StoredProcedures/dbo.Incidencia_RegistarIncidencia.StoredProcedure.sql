USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Incidencia_RegistarIncidencia]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Incidencia_RegistarIncidencia]
GO
/****** Object:  StoredProcedure [dbo].[Incidencia_RegistarIncidencia]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Edgar Villarreal
-- Create date: 28/03/2016
-- Description: Vencidas incidencias
-- Incidencia_RegistarIncidencia  1,59
--=============================================
CREATE PROCEDURE [dbo].[Incidencia_RegistarIncidencia] 
@IncidenciaID INT,
@EstatusID INT,
@HorasRespuesta INT,
@EstatusAnteriorID INT,
@AccionAnteriorID INT,
@NivelAlertaID INT,
@Fecha datetime,
@FechaVencimientoAnterior datetime,
@UsuarioModificacionID INT,
@UsuarioResponsableAnteriorID INT,
@Comentarios VARCHAR(255)

AS
BEGIN
	SET NOCOUNT ON;

		DECLARE @SiguienteNivel INT
		DECLARE @FechaVencimiento DATETIME
		--Este query trae el siguiente nivel de alerta
		SET @SiguienteNivel = (SELECT TOP(1) NivelAlertaID from NivelAlerta WHERE NivelAlertaID > @NivelAlertaID AND Activo = 1 ORDER BY NivelAlertaID ASC)
		
		SET @FechaVencimiento = dateadd(HOUR, @HorasRespuesta , @FechaVencimientoAnterior)
		
		IF DATEPART(dw,@FechaVencimiento) = 1
		BEGIN
			SET @FechaVencimiento = DATEADD(DAY, 1, @FechaVencimiento);
		END

		UPDATE Incidencia SET
					EstatusID = @EstatusID,
					FechaVencimiento = @FechaVencimiento, --dateadd(HOUR, @HorasRespuesta , @FechaVencimientoAnterior),
					Comentarios = @Comentarios,
					UsuarioModificacionID = @UsuarioModificacionID,
					FechaModificacion = GETDATE(),
					NivelAlertaID = ISNULL(@SiguienteNivel, @NivelAlertaID)
			WHERE IncidenciaID = @IncidenciaID

			INSERT INTO IncidenciaSeguimiento (IncidenciaID,
												Fecha, 
												FechaVencimiento, 
												FechaVencimientoAnterior, 
												AccionID,
												AccionAnteriorID, 
												EstatusID, 
												EstatusAnteriorID,
												UsuarioResponsableID,
												UsuarioResponsableAnteriorID,
												NivelAlertaID,
												NivelAlertaAnteriorID,
												Comentarios,
												FechaModificacion,
												UsuarioModificacionID)
						VALUES(	@IncidenciaID,
										@Fecha,
										@FechaVencimiento,-- dateadd(HOUR, @HorasRespuesta , @FechaVencimientoAnterior),
										@FechaVencimientoAnterior,
										CASE @AccionAnteriorID WHEN 0 THEN NULL ELSE @AccionAnteriorID END,
										CASE @AccionAnteriorID WHEN 0 THEN NULL ELSE @AccionAnteriorID END,
										@EstatusID,
										@EstatusAnteriorID,
										CASE @UsuarioResponsableAnteriorID WHEN 0 THEN NULL ELSE @UsuarioResponsableAnteriorID END,
										CASE @UsuarioResponsableAnteriorID WHEN 0 THEN NULL ELSE @UsuarioResponsableAnteriorID END,
										ISNULL(@SiguienteNivel, @NivelAlertaID),
										@NivelAlertaID,
										@Comentarios,
										GETDATE(),
										@UsuarioModificacionID)
			
	SET NOCOUNT OFF;
END
