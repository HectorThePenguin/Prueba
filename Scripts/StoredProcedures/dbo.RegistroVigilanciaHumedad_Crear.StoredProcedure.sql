USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RegistroVigilanciaHumedad_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RegistroVigilanciaHumedad_Crear]
GO
/****** Object:  StoredProcedure [dbo].[RegistroVigilanciaHumedad_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 09/04/2015 12:00:00 a.m.
-- Description: 
-- SpName     : RegistroVigilanciaHumedad_Crear
--======================================================
CREATE PROCEDURE [dbo].[RegistroVigilanciaHumedad_Crear]
@RegistroVigilanciaID int,
@Humedad decimal(18,2),
@NumeroMuestra int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT RegistroVigilanciaHumedad (
		RegistroVigilanciaID,
		Humedad,
		NumeroMuestra,
		FechaMuestra,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@RegistroVigilanciaID,
		@Humedad,
		@NumeroMuestra,
		GETDATE(),
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
