USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[NivelAlerta_ActivarPrimerNivelDesactivado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[NivelAlerta_ActivarPrimerNivelDesactivado]
GO
/****** Object:  StoredProcedure [dbo].[NivelAlerta_ActivarPrimerNivelDesactivado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Manuel Enrique Torres Lugo
-- Create date: 29/03/2016
-- Description: SP que devuelve el primer campo inactivo de
-- la tabla NivelAlerta y verifica si es el mismo que se le
-- envio.
-- RETURN	  : Si regresa 0 no es el primero deshabilitado,
--				si regresa > 0 es el primero deshabilitado
-- SpName     : dbo.NivelAlerta_ActivarPrimerNivelDesactivado
-- --======================================================
CREATE PROCEDURE [dbo].[NivelAlerta_ActivarPrimerNivelDesactivado]
@ID INT,
@Activo INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @NivelAlertaID INT;

	SET @NivelAlertaID = 0;

	SELECT TOP 1 
		@NivelAlertaID = NA.NivelAlertaID
	FROM NivelAlerta AS NA
	WHERE NA.Activo = @Activo
	ORDER BY NA.NivelAlertaID ASC

	IF (@ID <> @NivelAlertaID)
	BEGIN
		SET @NivelAlertaID = 0;
	END

	SELECT @NivelAlertaID
	SET NOCOUNT OFF;
END