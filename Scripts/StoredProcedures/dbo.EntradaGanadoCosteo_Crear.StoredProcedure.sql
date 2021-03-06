USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoCosteo_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanadoCosteo_Crear]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoCosteo_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 2013/11/25
-- Description: 
-- 
--=============================================
CREATE PROCEDURE [dbo].[EntradaGanadoCosteo_Crear]
@OrganizacionID INT,
	@EntradaGanadoID INT,
	@Activo BIT,	
	@UsuarioCreacion INT,		
	@Observacion VARCHAR(255)	
AS
BEGIN
	SET NOCOUNT ON;
	INSERT EntradaGanadoCosteo(
		OrganizacionID,
		EntradaGanadoID,
		Activo,
		FechaCreacion,
		UsuarioCreacion,		
		Observacion	
	)
	VALUES(
		@OrganizacionID,
		@EntradaGanadoID,
		@Activo,
		GETDATE(),
		@UsuarioCreacion,				
		@Observacion	
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
