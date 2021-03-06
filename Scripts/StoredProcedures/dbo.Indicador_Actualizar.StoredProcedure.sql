USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Indicador_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Indicador_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Indicador_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 02/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Indicador_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[Indicador_Actualizar]
@IndicadorID int,
@Descripcion varchar(50),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Indicador SET
		Descripcion = @Descripcion,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE IndicadorID = @IndicadorID
	SET NOCOUNT OFF;
END

GO
