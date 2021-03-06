USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RegistroVigilanciaHumedad_ObtenerPorRegistroVigilanciaID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RegistroVigilanciaHumedad_ObtenerPorRegistroVigilanciaID]
GO
/****** Object:  StoredProcedure [dbo].[RegistroVigilanciaHumedad_ObtenerPorRegistroVigilanciaID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 14/04/2015 12:00:00 a.m.
-- Description: 
-- SpName     : RegistroVigilanciaHumedad_ObtenerPorRegistroVigilanciaID
--======================================================
CREATE PROCEDURE [dbo].[RegistroVigilanciaHumedad_ObtenerPorRegistroVigilanciaID]
@RegistroVigilanciaID int
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
	WHERE RegistroVigilanciaID = @RegistroVigilanciaID
	and Activo = 1
	SET NOCOUNT OFF;
END

GO
