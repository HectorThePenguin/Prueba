USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ParametroTrampa_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ParametroTrampa_Crear]
GO
/****** Object:  StoredProcedure [dbo].[ParametroTrampa_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ParametroTrampa_Crear
--======================================================
CREATE PROCEDURE [dbo].[ParametroTrampa_Crear]
@ParametroID int,
@TrampaID int,
@Valor varchar(100),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT ParametroTrampa (
		ParametroID,
		TrampaID,
		Valor,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@ParametroID,
		@TrampaID,
		@Valor,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
