USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CostoOrganizacion_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CostoOrganizacion_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[CostoOrganizacion_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
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
CREATE PROCEDURE [dbo].[CostoOrganizacion_Actualizar]
	@CostoOrganizacionID INT,
	@TipoOrganizacionID INT,
	@CostoID INT,
	@Automatico BIT,
	@Activo BIT,
	@UsuarioModificacionID INT	
AS
BEGIN
	SET NOCOUNT ON;
		UPDATE CostoOrganizacion SET 
			TipoOrganizacionID = @TipoOrganizacionID,
			CostoID = @CostoID,
			Automatico = @Automatico,
			Activo = @Activo,
			FechaModificacion = GETDATE(),
			UsuarioModificacionID = @UsuarioModificacionID	
		WHERE CostoOrganizacionID = @CostoOrganizacionID
	SET NOCOUNT OFF;
END

GO
