USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SupervisionGanado_ValidarAretesDetectados]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SupervisionGanado_ValidarAretesDetectados]
GO
/****** Object:  StoredProcedure [dbo].[SupervisionGanado_ValidarAretesDetectados]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:    Ramses Santos
-- Create date: 17/02/2014
-- Description:  Validar que el arete ingresado no este detectado en el mismo dia.
-- EXEC SupervisionGanado_ValidarAretesDetectados '48405000218484', '123'
-- ===============================================================
CREATE PROCEDURE [dbo].[SupervisionGanado_ValidarAretesDetectados] @Arete VARCHAR(15), @AreteTestigo VARCHAR(15)
AS
BEGIN
	DECLARE @retorno INT
	SELECT @retorno = 0
	IF @Arete = ''
	BEGIN
		IF EXISTS (SELECT AreteMetalico FROM SupervisionGanado WHERE AreteMetalico = @AreteTestigo AND CAST(FechaDeteccion AS DATE) = CAST(GETDATE() AS DATE))
		BEGIN
			SELECT @retorno = 1
		END
	END
	ELSE
	BEGIN
		IF EXISTS (SELECT Arete FROM SupervisionGanado WHERE Arete = @Arete AND CAST(FechaDeteccion AS DATE) = CAST(GETDATE() AS DATE))
		BEGIN
			SELECT @retorno = 1
		END
	END
	SELECT @retorno
END

GO
