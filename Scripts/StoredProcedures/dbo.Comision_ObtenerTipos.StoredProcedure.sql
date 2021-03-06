USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Comision_ObtenerTipos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Comision_ObtenerTipos]
GO
/****** Object:  StoredProcedure [dbo].[Comision_ObtenerTipos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Daniel Benitez
-- Fecha: 01-08-2015
-- Descripción:	Obtener un listado de los tipos de comision activos
-- Comision_ObtenerTipos	
-- =============================================
CREATE PROCEDURE [dbo].[Comision_ObtenerTipos] 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TipoComisionID, Descripcion FROM TipoComision (NOLOCK) WHERE Activo = 1
	SET NOCOUNT OFF;  
END

GO
