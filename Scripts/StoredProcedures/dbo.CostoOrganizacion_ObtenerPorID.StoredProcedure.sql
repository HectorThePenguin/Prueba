USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CostoOrganizacion_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CostoOrganizacion_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[CostoOrganizacion_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
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
CREATE PROCEDURE [dbo].[CostoOrganizacion_ObtenerPorID]
@CostoOrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		CostoOrganizacionID,
		TipoOrganizacionID,
		CostoID,
		Automatico,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID	
	FROM CostoOrganizacion
	WHERE CostoOrganizacionID = @CostoOrganizacionID
	SET NOCOUNT OFF;
END

GO
