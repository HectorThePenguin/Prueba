USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoMovimiento_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoMovimiento_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[TipoMovimiento_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoMovimiento_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[TipoMovimiento_Actualizar]
@TipoMovimientoID int,
@Descripcion varchar(50),
@EsGanado bit,
@EsProducto bit,
@EsEntrada bit,
@EsSalida bit,
@ClaveCodigo char(2),
@TipoPolizaID int,
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE TipoMovimiento SET
		Descripcion = @Descripcion,
		EsGanado = @EsGanado,
		EsProducto = @EsProducto,
		EsEntrada = @EsEntrada,
		EsSalida = @EsSalida,
		ClaveCodigo = @ClaveCodigo,
		TipoPolizaID = @TipoPolizaID,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE TipoMovimientoID = @TipoMovimientoID
	SET NOCOUNT OFF;
END

GO
