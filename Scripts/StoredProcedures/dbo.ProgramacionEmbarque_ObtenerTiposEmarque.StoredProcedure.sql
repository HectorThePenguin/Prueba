USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerTiposEmbarque]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerTiposEmbarque]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerTiposEmbarque]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Luis Alfonso Sandoval Huerta
-- Create date: 05/06/2017 11:00:00 a.m.
-- Description: Obtiene los tipo de embarque activos
-- SpName     : ProgramacionEmbarque_ObtenerTiposEmbarque 1
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerTiposEmbarque] 
@Activo INT
AS
BEGIN
	SELECT
		TipoEmbarqueID,
		Descripcion
	FROM TipoEmbarque
	WHERE Activo = @Activo;
END	

GO