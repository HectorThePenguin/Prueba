USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorDescripcionOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerPorDescripcionOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorDescripcionOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 22/04/2014
-- Description: 
-- SpName     : Corral_ObtenerPorDescripcionOrganizacion '001', 100
--======================================================
CREATE PROCEDURE [dbo].[Corral_ObtenerPorDescripcionOrganizacion]
@Descripcion varchar(50)
, @OrganizacionID INT
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
		AND OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

GO
