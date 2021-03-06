USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CostoOrganizacion_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CostoOrganizacion_Crear]
GO
/****** Object:  StoredProcedure [dbo].[CostoOrganizacion_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
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
CREATE PROCEDURE [dbo].[CostoOrganizacion_Crear]
	@TipoOrganizacionID INT,
	@CostoID INT,
	@Automatico BIT,
	@Activo BIT,
	@UsuarioCreacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT CostoOrganizacion(
		TipoOrganizacionID,
		CostoID,
		Automatico,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
	)
	VALUES(
		@TipoOrganizacionID,
		@CostoID,
		@Automatico,
		@Activo,
		GETDATE(),
		@UsuarioCreacionID
	)
	SET NOCOUNT OFF;
END

GO
