USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoCosto_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoCosto_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[TipoCosto_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoCosto_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[TipoCosto_Actualizar]
@TipoCostoID int,
@Descripcion varchar(50),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE TipoCosto SET
		Descripcion = @Descripcion,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE TipoCostoID = @TipoCostoID
	SET NOCOUNT OFF;
END

GO
