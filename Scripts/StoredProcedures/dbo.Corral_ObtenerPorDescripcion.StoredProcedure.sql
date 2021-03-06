USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 16/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Corral_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[Corral_ObtenerPorDescripcion]
@Descripcion varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		CorralID,
		OrganizacionID,
		Codigo,
		TipoCorralID,
		Capacidad,
		MetrosLargo,
		MetrosAncho,
		Seccion,
		Orden,
		Activo
	FROM Corral
	WHERE Codigo = @Descripcion
	SET NOCOUNT OFF;
END

GO
