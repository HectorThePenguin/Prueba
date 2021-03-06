USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoCambio_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoCambio_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[TipoCambio_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 16/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoCambio_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[TipoCambio_Actualizar]
@TipoCambioID int,
@MonedaID int,
@Descripcion varchar(50),
@Cambio decimal(10,4),
@Fecha datetime,
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE TipoCambio SET
		MonedaID = @MonedaID,
		Descripcion = @Descripcion,
		Cambio = @Cambio,
		Fecha = @Fecha,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE TipoCambioID = @TipoCambioID
	SET NOCOUNT OFF;
END

GO
