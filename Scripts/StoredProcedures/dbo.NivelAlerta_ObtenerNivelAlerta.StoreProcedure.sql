USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[NivelAlerta_ObtenerNivelAlerta]    Script Date: 16/10/2015 10:19:15 a.m. ******/
DROP PROCEDURE [dbo].[NivelAlerta_ObtenerNivelAlerta]
GO
/****** Object:  StoredProcedure [dbo].[NivelAlerta_ObtenerNivelAlerta]    Script Date: 16/10/2015 10:19:15 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Manuel Enrique Torres Lugo
-- Create date: 16/03/2016
-- Description: SP par obtener los niveles de alerta.
-- SpName     : dbo.NivelAlerta_ObtenerNivelAlerta
-- --======================================================
CREATE PROCEDURE [dbo].[NivelAlerta_ObtenerNivelAlerta]

AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		NivelAlertaID,
		Descripcion
	FROM NivelAlerta
	WHERE Activo = 1
	SET NOCOUNT OFF;
END