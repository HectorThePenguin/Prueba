USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoPoliza_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoPoliza_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[TipoPoliza_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoPoliza_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[TipoPoliza_Actualizar]
@TipoPolizaID int,
@Descripcion varchar(50),
@ClavePoliza char(2),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
		UPDATE TipoPoliza SET
			Descripcion = @Descripcion,
			ClavePoliza = @ClavePoliza,
			Activo = @Activo,
			UsuarioModificacionID = @UsuarioModificacionID,
			FechaModificacion = GETDATE()
		WHERE TipoPolizaID = @TipoPolizaID
	SET NOCOUNT OFF;
END

GO
