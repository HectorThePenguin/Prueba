USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Incidencia_CerrarIncidencia]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Incidencia_CerrarIncidencia]
GO
/****** Object:  StoredProcedure [dbo].[Incidencia_CerrarIncidencia]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Edgar Villarreal
-- Create date: 28/03/2016
-- Description: Cerrar incidencias
-- Incidencia_CerrarIncidencia  1,59
--=============================================
CREATE PROCEDURE [dbo].[Incidencia_CerrarIncidencia] 
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
		UPDATE Incidencia SET
					EstatusID = @EstatusID,
					FechaVencimiento = NULL,
					Comentarios = @Comentarios,
					UsuarioModificacionID = @UsuarioModificacionID,
					FechaModificacion = GETDATE()
			WHERE IncidenciaID = @IncidenciaID

			INSERT INTO IncidenciaSeguimiento (IncidenciaID,
												Fecha, 
												FechaVencimiento, 
												FechaVencimientoAnterior, 
												AccionAnteriorID, 
												EstatusID, 
												EstatusAnteriorID,
												UsuarioResponsableAnteriorID,
												NivelAlertaID,
												Comentarios,
											  FechaModificacion,
												UsuarioModificacionID)
						VALUES(	@IncidenciaID,
										@Fecha,
										NULL,
										@FechaVencimientoAnterior,
										 CASE @AccionAnteriorID WHEN 0 THEN NULL ELSE @AccionAnteriorID END,
										@EstatusID,
										@EstatusAnteriorID,
										CASE @UsuarioResponsableAnteriorID WHEN 0 THEN NULL ELSE @UsuarioResponsableAnteriorID END,
										@NivelAlertaID,
										@Comentarios,
										GETDATE(),
										@UsuarioModificacionID)
			
	SET NOCOUNT OFF;
END