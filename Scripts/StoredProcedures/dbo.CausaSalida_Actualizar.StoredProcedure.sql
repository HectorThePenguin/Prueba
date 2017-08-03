USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CausaSalida_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CausaSalida_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[CausaSalida_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CausaSalida_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[CausaSalida_Actualizar]
@CausaSalidaID int,
@Descripcion varchar(50),
@TipoMovimientoID int,
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE CausaSalida SET
		Descripcion = @Descripcion,
		TipoMovimientoID = @TipoMovimientoID,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE CausaSalidaID = @CausaSalidaID
	SET NOCOUNT OFF;
END

GO
