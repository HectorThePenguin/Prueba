USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Incidencia_Rechazar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Incidencia_Rechazar]
GO
/****** Object:  StoredProcedure [dbo].[Incidencia_Rechazar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Eric García
-- Create date: 15/03/2016 3:22:00 p.m.
-- Description: Obtiene todas las incidencias activas por la organización del usuario
-- SpName     : EXEC Incidencia_Rechazar 503619, '', 45, 5 , 57
--=============================================  
CREATE PROCEDURE [dbo].[Incidencia_Rechazar]  
@IncidenciaID INT,  
@Comentarios varchar(max),
@NivelAlertaID INT,
@UsuarioID INT,
@EstatusID INT
AS  
BEGIN  
	SET NOCOUNT ON; 
	DECLARE @SiguienteNivel INT
	DECLARE @AccionAnteriorID INT
	DECLARE @FechaVencimientoAnterior DATETIME
	DECLARE @EstatusAnteriorID INT
	DECLARE @UsuarioRespontableAnteriorID INT
	DECLARE @NivelAlertaAnteriorID INT
	DECLARE @FechaRegistro DATETIME
	DECLARE @ComentariosAnteriores VARCHAR(255)
	DECLARE @FechaVencimiento DATETIME
	DECLARE @HorasRespuesta INT
	
	--Este query trae el siguiente nivel de alerta
	SET @SiguienteNivel = (SELECT TOP(1) NivelAlertaID from NivelAlerta WHERE NivelAlertaID < @NivelAlertaID AND Activo = 1 ORDER BY NivelAlertaID DESC)

	SET @HorasRespuesta = (SELECT HorasRespuesta 
							FROM Alerta 
							INNER JOIN Incidencia Inc ON Inc.AlertaID = Alerta.AlertaID
							AND Inc.IncidenciaID = @IncidenciaID)

	SET @FechaVencimiento = DATEADD(HOUR, @HorasRespuesta, GETDATE());
	IF DATEPART(dw,@FechaVencimiento) = 1
	BEGIN
		SET @FechaVencimiento = DATEADD(DAY, 1, @FechaVencimiento);
	END
	
	SELECT @AccionAnteriorID = ISNULL(AccionID, 0), 
				 @FechaVencimientoAnterior = FechaVencimiento,
				 @EstatusAnteriorID = EstatusID,
				 @ComentariosAnteriores = Comentarios,
				 @UsuarioRespontableAnteriorID = UsuarioResponsableID,
				 @NivelAlertaAnteriorID = NivelAlertaID,
				 @FechaRegistro = Fecha
  FROM Incidencia WHERE IncidenciaID = @IncidenciaID
	
	UPDATE Incidencia
	   SET FechaVencimiento = @FechaVencimiento,  
		   Comentarios = @Comentarios,
		   NivelAlertaID = @SiguienteNivel,
			 EstatusID = @EstatusID,
			 AccionID = NULL,
			 UsuarioResponsableID = @UsuarioRespontableAnteriorID,
		   UsuarioModificacionID = @UsuarioID,
		   FechaModificacion = GetDate()
	 WHERE IncidenciaID = @IncidenciaID;
	 
	 INSERT INTO IncidenciaSeguimiento 
	 (IncidenciaID,
		Fecha,
		FechaVencimiento,
		FechaVencimientoAnterior,
		AccionID,
		AccionAnteriorID,
		Comentarios,
		EstatusID,
		EstatusAnteriorID,
		UsuarioResponsableID,
		UsuarioResponsableAnteriorID,
		NivelAlertaID,
		NivelAlertaAnteriorID,
		UsuarioModificacionID,
		FechaModificacion
		)
	 VALUES
	 (@IncidenciaID,
		@FechaRegistro,
		@FechaVencimiento,
		@FechaVencimientoAnterior,
		CASE @AccionAnteriorID WHEN 0 THEN NULL ELSE @AccionAnteriorID END,
		CASE @AccionAnteriorID WHEN 0 THEN NULL ELSE @AccionAnteriorID END,
		@Comentarios,
		@EstatusID,
		@EstatusAnteriorID,
		@UsuarioID,
		@UsuarioRespontableAnteriorID,
		@SiguienteNivel,
		@NivelAlertaID,
		@UsuarioID,
		GETDATE()
		)

	SET NOCOUNT OFF;  
END  
