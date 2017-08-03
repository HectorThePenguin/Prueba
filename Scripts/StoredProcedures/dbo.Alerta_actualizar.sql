--======================================================
-- Author     : Franco Jesús Inzunza Martínez
-- Create date: 17/03/2016 11:10:00 a.m.
-- Description: SP para actualizar Alertas.
-- SpName     : Alerta_actualizar
-- --======================================================
CREATE PROCEDURE [dbo].[Alerta_actualizar]
@AlertaID int,
@ModuloID int,
@Descripcion varchar(255),
@HorasRespuesta int,
@TerminadoAutomatico BIT,
@Activo BIT,
@UsuarioModificacionID int--id del usuario que modifico este registro
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Alerta SET
		ModuloID=@ModuloID,
		Descripcion = @Descripcion,
		HorasRespuesta=@HorasRespuesta,
		TerminadoAutomatico=@TerminadoAutomatico,
		Activo=@Activo,
		FechaModificacion=GETDATE(),
		UsuarioModificacionID = @UsuarioModificacionID
	WHERE AlertaID = @AlertaID
	SET NOCOUNT OFF;
END
