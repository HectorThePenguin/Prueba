USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RegistroVigilanciaHumedad_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RegistroVigilanciaHumedad_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[RegistroVigilanciaHumedad_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 09/04/2015 12:00:00 a.m.
-- Description: 
-- SpName     : RegistroVigilanciaHumedad_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[RegistroVigilanciaHumedad_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		RegistroVigilanciaHumedadID,
		RegistroVigilanciaID,
		Humedad,
		NumeroMuestra,
		FechaMuestra,
		Activo
	FROM RegistroVigilanciaHumedad
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
