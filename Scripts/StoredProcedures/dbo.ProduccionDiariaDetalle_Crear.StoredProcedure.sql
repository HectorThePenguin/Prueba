USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionDiariaDetalle_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionDiariaDetalle_Crear]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionDiariaDetalle_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 24/06/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ProduccionDiariaDetalle_Crear
--======================================================
CREATE PROCEDURE [dbo].[ProduccionDiariaDetalle_Crear]
@ProduccionDiariaDetalleID int,
@ProduccionDiariaID int,
@ProductoID int,
@PesajeMateriaPrimaID int,
@EspecificacionForraje int,
@HoraInicial varchar(5),
@HoraFinal varchar(5),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT ProduccionDiariaDetalle (
		ProduccionDiariaDetalleID,
		ProduccionDiariaID,
		ProductoID,
		PesajeMateriaPrimaID,
		EspecificacionForraje,
		HoraInicial,
		HoraFinal,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@ProduccionDiariaDetalleID,
		@ProduccionDiariaID,
		@ProductoID,
		@PesajeMateriaPrimaID,
		@EspecificacionForraje,
		@HoraInicial,
		@HoraFinal,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
