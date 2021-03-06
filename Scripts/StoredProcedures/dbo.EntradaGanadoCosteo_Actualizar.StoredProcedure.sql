USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoCosteo_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanadoCosteo_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoCosteo_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
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
CREATE PROCEDURE [dbo].[EntradaGanadoCosteo_Actualizar]
	@EntradaGanadoCosteoID INT,
	@OrganizacionID INT,
	@EntradaGanadoID INT,
	@Activo BIT,		
	@UsuarioModificacion INT,
	@Observacion VARCHAR(255)	
AS
BEGIN
	SET NOCOUNT ON;
		UPDATE EntradaGanadoCosteo SET 
			OrganizacionID = @OrganizacionID,
			EntradaGanadoID = @EntradaGanadoID,
			Activo = @Activo,			
			FechaModificacion = GETDATE(),
			UsuarioModificacion = @UsuarioModificacion,
			Observacion = @Observacion	
		WHERE EntradaGanadoCosteoID = @EntradaGanadoCosteoID
	SET NOCOUNT OFF;
END

GO
