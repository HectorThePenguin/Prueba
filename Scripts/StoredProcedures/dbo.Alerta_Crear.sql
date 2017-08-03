--======================================================
-- Author     : Franco Jesus Inzunza Martinez
-- Create date: 17/03/2016 11:00:00 a.m.
-- Description: SP para registrar una Alerta.
-- SpName     : Alerta_Crear
--======================================================
CREATE PROCEDURE [dbo].[Alerta_Crear]
@ModuloID int,
@Descripcion varchar(255),
@HorasRespuesta int,
@TerminadoAutomatico BIT,
@Activo BIT,
@UsuarioCreacionID int--usuario que dio de alta este registro
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Alerta (
		ModuloID,
		Descripcion,
		HorasRespuesta,
		TerminadoAutomatico,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
    UsuarioModificacionID
	)
	VALUES(
		@ModuloID,
		@Descripcion,
		@HorasRespuesta,
		@TerminadoAutomatico,
		@Activo,
		GETDATE(),
		@UsuarioCreacionID,
		NULL,
		NULL)
	SET NOCOUNT OFF;
END