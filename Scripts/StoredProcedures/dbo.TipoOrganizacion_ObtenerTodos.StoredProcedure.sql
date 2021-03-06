USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoOrganizacion_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoOrganizacion_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[TipoOrganizacion_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jos� Gilberto Quintero L�pez
-- Create date: 15/10/2013
-- Description:	Obtener listado de Tipo de organizaci�n.
-- TipoOrganizacion_ObtenerTodos 0
-- =============================================
CREATE PROCEDURE [dbo].[TipoOrganizacion_ObtenerTodos]	
@Activo BIT	= NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		TipoOrganizacionID,
		TipoProcesoID,
		Descripcion,
		Activo
	FROM TipoOrganizacion
	WHERE (Activo = @Activo OR @Activo is null)
	ORDER BY Descripcion
END

GO
