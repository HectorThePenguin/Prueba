USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[NivelAlerta_VerificarAsignacionNivelAlerta]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[NivelAlerta_VerificarAsignacionNivelAlerta]
GO
/****** Object:  StoredProcedure [dbo].[NivelAlerta_VerificarAsignacionNivelAlerta]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Manuel Enrique Torres Lugo
-- Create date: 29/03/2016
-- Description: SP para verificar que el nivel a desactivar
-- 							sea el ultimo y no este asignado.
-- SpName     : dbo.NivelAlerta_VerificarAsignacionNivelAlerta
-- --======================================================
CREATE PROCEDURE [dbo].[NivelAlerta_VerificarAsignacionNivelAlerta]
@ID INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @NivelAlertaID INT, @result INT;

	SET @NivelAlertaID = 0;
	SET @result = 0;

	SELECT TOP 1 
		@NivelAlertaID = NA.NivelAlertaID
	FROM NivelAlerta AS NA
	WHERE NA.Activo = 1
	ORDER BY NA.NivelAlertaID DESC
	
	SELECT 
		@result = AC.NivelAlertaID 
	FROM AlertaConfiguracion AS AC
	WHERE AC.NivelAlertaID = @NivelAlertaID

	IF (@result = 0)
	BEGIN
		IF(@ID = @NivelAlertaID)
			BEGIN
				SET @result = 0;
			END
			ELSE
				SET @result = 1;
	END

	SELECT @result

	SET NOCOUNT OFF;
END