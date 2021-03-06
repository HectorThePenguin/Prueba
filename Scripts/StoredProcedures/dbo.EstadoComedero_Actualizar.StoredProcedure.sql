USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EstadoComedero_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EstadoComedero_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[EstadoComedero_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : EstadoComedero_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[EstadoComedero_Actualizar]
@EstadoComederoID int,
@Descripcion varchar(100),
@DescripcionCorta varchar(50),
@NoServir bit,
@AjusteBase decimal(10,2),
@Tendencia char(1),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE EstadoComedero SET
		Descripcion = @Descripcion,
		DescripcionCorta = @DescripcionCorta,
		NoServir = @NoServir,
		AjusteBase = @AjusteBase,
		Tendencia = @Tendencia,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE EstadoComederoID = @EstadoComederoID
	SET NOCOUNT OFF;
END

GO
