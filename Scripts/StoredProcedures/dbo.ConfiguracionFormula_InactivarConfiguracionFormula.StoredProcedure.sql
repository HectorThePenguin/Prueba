USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionFormula_InactivarConfiguracionFormula]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionFormula_InactivarConfiguracionFormula]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionFormula_InactivarConfiguracionFormula]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2014/03/22
-- Description: SP para Inactivar la configuracion de las formulas para cada organizacion
-- Origen     : APInterfaces
-- EXEC ConfiguracionFormula_InactivarConfiguracionFormula 4,1,0
-- =============================================
CREATE PROCEDURE [dbo].[ConfiguracionFormula_InactivarConfiguracionFormula]
	@OrganizacionID INT,
    @UsuarioModificacionID INT, 
    @Activo BIT 
AS
BEGIN
		UPDATE ConfiguracionFormula 
		   SET UsuarioModificacionID = @UsuarioModificacionID,
			   FechaModificacion = GETDATE(),
			   Activo = @Activo 
		 WHERE OrganizacionID = @OrganizacionID
		   AND Activo = 1
END

GO
