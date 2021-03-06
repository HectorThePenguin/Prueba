USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RegistroVigilanciaHumedad_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RegistroVigilanciaHumedad_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[RegistroVigilanciaHumedad_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 09/04/2015 12:00:00 a.m.
-- Description: 
-- SpName     : RegistroVigilanciaHumedad_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[RegistroVigilanciaHumedad_Actualizar]
@RegistroVigilanciaHumedadID int,
@RegistroVigilanciaID int,
@Humedad decimal(18,2),
@NumeroMuestra int,
@FechaMuestra smalldatetime,
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE RegistroVigilanciaHumedad SET
		RegistroVigilanciaID = @RegistroVigilanciaID,
		Humedad = @Humedad,
		NumeroMuestra = @NumeroMuestra,
		FechaMuestra = @FechaMuestra,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE RegistroVigilanciaHumedadID = @RegistroVigilanciaHumedadID
	SET NOCOUNT OFF;
END

GO
