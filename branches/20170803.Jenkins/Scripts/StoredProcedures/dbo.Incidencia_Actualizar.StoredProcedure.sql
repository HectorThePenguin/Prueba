USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Incidencia_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Incidencia_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Incidencia_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Eric García
-- Create date: 15/03/2016 3:22:00 p.m.
-- Description: Obtiene todas las incidencias activas por la organización del usuario
-- SpName     : EXEC Incidencia_Actualizar 
--======================================================
CREATE PROCEDURE [dbo].[Incidencia_Actualizar]  
@IncidenciaID INT,  
@Fecha DATETIME,  
@Comentarios varchar(max),
@NivelAlertaID INT,
@AccionID INT,
@UsuarioID INT

AS  
BEGIN  
	SET NOCOUNT ON; 
	DECLARE @AccionAnteriorID INT
	DECLARE @FechaVencimientoAnterior DATETIME
	DECLARE @EstatusAnteriorID INT
	DECLARE @UsuarioRespontableAnteriorID INT
	DECLARE @NivelAlertaAnteriorID INT
	DECLARE @FechaRegistro DATETIME
	DECLARE @ComentariosAnteriores VARCHAR(255)
	
	--Este query trae el siguiente nivel de alerta
	
	SELECT @AccionAnteriorID = ISNULL(AccionID, 0), 
				 @FechaVencimientoAnterior = FechaVencimiento,
				 @EstatusAnteriorID = EstatusID,
				 @ComentariosAnteriores = Comentarios,
				 @UsuarioRespontableAnteriorID = UsuarioResponsableID,
				 @NivelAlertaAnteriorID = NivelAlertaID,
				 @FechaRegistro = Fecha
  FROM Incidencia WHERE IncidenciaID = @IncidenciaID
	
	UPDATE Incidencia
	   SET
			 FechaVencimiento = CASE @AccionAnteriorID WHEN 0 THEN @Fecha ELSE FechaVencimiento END,  
		   Comentarios = @Comentarios,
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
		@Fecha,
		@FechaVencimientoAnterior,
		@AccionID,
		CASE @AccionAnteriorID WHEN 0 THEN NULL ELSE @AccionAnteriorID END,
		@Comentarios,
		@EstatusAnteriorID,
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

