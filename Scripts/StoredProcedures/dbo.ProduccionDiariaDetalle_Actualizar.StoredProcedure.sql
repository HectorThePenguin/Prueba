USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionDiariaDetalle_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionDiariaDetalle_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionDiariaDetalle_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 24/06/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ProduccionDiariaDetalle_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[ProduccionDiariaDetalle_Actualizar]
@ProduccionDiariaDetalleID int,
@ProduccionDiariaID int,
@ProductoID int,
@PesajeMateriaPrimaID int,
@EspecificacionForraje int,
@HoraInicial varchar(5),
@HoraFinal varchar(5),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE ProduccionDiariaDetalle SET
		ProduccionDiariaID = @ProduccionDiariaID,
		ProductoID = @ProductoID,
		PesajeMateriaPrimaID = @PesajeMateriaPrimaID,
		EspecificacionForraje = @EspecificacionForraje,
		HoraInicial = @HoraInicial,
		HoraFinal = @HoraFinal,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE ProduccionDiariaDetalleID = @ProduccionDiariaDetalleID
	SET NOCOUNT OFF;
END

GO
