USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ParametroTrampa_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ParametroTrampa_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[ParametroTrampa_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ParametroTrampa_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[ParametroTrampa_Actualizar]
@ParametroTrampaID int,
@ParametroID int,
@TrampaID int,
@Valor varchar(100),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE ParametroTrampa SET
		ParametroID = @ParametroID,
		TrampaID = @TrampaID,
		Valor = @Valor,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE ParametroTrampaID = @ParametroTrampaID
	SET NOCOUNT OFF;
END

GO
