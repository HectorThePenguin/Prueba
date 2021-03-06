USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CostoPromedio_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CostoPromedio_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[CostoPromedio_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 2013/12/09
-- Description: 
-- 
--=============================================
CREATE PROCEDURE [dbo].[CostoPromedio_ObtenerTodos]
@Activo BIT = null
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		CostoPromedioID,
		OrganizacionID,
		CostoID,
		Importe,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID	
	FROM CostoPromedio
	SET NOCOUNT OFF;
END

GO
