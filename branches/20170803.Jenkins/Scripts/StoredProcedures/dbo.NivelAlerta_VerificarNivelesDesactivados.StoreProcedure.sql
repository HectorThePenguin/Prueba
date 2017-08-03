USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[NivelAlerta_VerificarNivelesDesactivados]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[NivelAlerta_VerificarNivelesDesactivados]
GO
/****** Object:  StoredProcedure [dbo].[NivelAlerta_VerificarNivelesDesactivados]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Manuel Enrique Torres Lugo
-- Create date: 29/03/2016
-- Description: SP para verificar si existe un nivel desactivado.
-- SpName     : dbo.NivelAlerta_VerificarNivelesDesactivados
-- --======================================================
CREATE PROCEDURE [dbo].[NivelAlerta_VerificarNivelesDesactivados]
@Activo INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT COUNT(*) FROM NivelAlerta WHERE Activo = @Activo

	SET NOCOUNT OFF;
END