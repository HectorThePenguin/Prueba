USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[NivelAlerta_ObtenerPoDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[NivelAlerta_ObtenerPoDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[NivelAlerta_ObtenerPoDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramón Abel Atondo Echavarria
-- Create date: 16/03/2016
-- Description: SP par obtener los registros de nivel alerta solo descripcion.
-- SpName     : dbo.NivelAlerta_ObtenerPoDescripcion
-- --======================================================
CREATE PROCEDURE [dbo].[NivelAlerta_ObtenerPoDescripcion]
@Descripcion varchar(255)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		NivelAlertaID,
		Descripcion,
		Activo
	FROM NivelAlerta
	WHERE Descripcion = @Descripcion
	SET NOCOUNT OFF;
END
