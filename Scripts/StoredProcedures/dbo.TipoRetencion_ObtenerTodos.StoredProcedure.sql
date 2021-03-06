USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoRetencion_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoRetencion_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[TipoRetencion_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:    Ruben Guzman meza
-- Create date: 15/10/2013  
-- Description:  Obtener listado de Proveedores  
-- TipoRetencion_ObtenerTodos  
-- ============================================= 
CREATE PROCEDURE [dbo].[TipoRetencion_ObtenerTodos]
@Activo BIT = NULL 
AS
BEGIN
	SET NOCOUNT ON;  
	SELECT 
		TipoRetencionID
		,Descripcion
		,Activo 
	FROM TipoRetencion
	WHERE Activo = @Activo OR @Activo IS NULL  
	SET NOCOUNT OFF; 
END

GO
