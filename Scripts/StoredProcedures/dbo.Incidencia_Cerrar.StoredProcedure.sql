USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Incidencia_Cerrar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Incidencia_Cerrar]
GO
/****** Object:  StoredProcedure [dbo].[Incidencia_Cerrar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Eric García
-- Create date: 15/03/2016 3:22:00 p.m.
-- Description: Obtiene todas las incidencias activas por la organización del usuario
-- SpName     : EXEC Incidencia_Cerrar 
--=============================================  
CREATE PROCEDURE [dbo].[Incidencia_Cerrar]  
@IncidenciaID INT,  
@Fecha DATETIME,  
@Comentarios varchar(max),
@NivelAlertaID INT,
@AccionID INT,
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

	SELECT @AccionAnteriorID = ISNULL(AccionID, 0), 
				 @FechaVencimientoAnterior = FechaVencimiento,
				 @EstatusAnteriorID = EstatusID,
				 @ComentariosAnteriores = Comentarios,
				 @UsuarioRespontableAnteriorID = UsuarioResponsableID,
				 @NivelAlertaAnteriorID = NivelAlertaID,
				 @FechaRegistro = Fecha
  FROM Incidencia WHERE IncidenciaID = @IncidenciaID
	
	UPDATE Incidencia
	   SET FechaVencimiento = NULL,  
		   Comentarios = @Comentarios,
		   NivelAlertaID = @NivelAlertaID,
			 EstatusID = @EstatusID,
			 AccionID = @AccionID,
			 UsuarioResponsableID = @UsuarioID,
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
		NULL,
		@FechaVencimientoAnterior,
		@AccionID,
		CASE @AccionAnteriorID WHEN 0 THEN NULL ELSE @AccionAnteriorID END,
		@Comentarios,
		@EstatusID,
		@EstatusAnteriorID,
		@UsuarioID,
		@UsuarioRespontableAnteriorID,
		@NivelAlertaID,
		@NivelAlertaAnteriorID,
		@UsuarioID,
		GETDATE()
		)

	SET NOCOUNT OFF;  
END
