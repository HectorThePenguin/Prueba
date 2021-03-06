USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaPorMuerte_GuardarSalidaPorMuerteNecropsia]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaPorMuerte_GuardarSalidaPorMuerteNecropsia]
GO
/****** Object:  StoredProcedure [dbo].[SalidaPorMuerte_GuardarSalidaPorMuerteNecropsia]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: César Valdez
-- Create date: 14/02/2013
-- Description: Guarda la salida por muerte de un animal desde necropsia
-- Empresa: Apinterfaces
-- =============================================
CREATE PROCEDURE [dbo].[SalidaPorMuerte_GuardarSalidaPorMuerteNecropsia]
	@MuerteId INT,
	@ProblemaId INT,
	@Observaciones VARCHAR(255),
	@FotoNecropsia VARCHAR(250),
	@OperadorNecropsiaId INT,
	@UsuarioModificacionId INT,
	@EstatusID INT,
	@AnimalID	INT
AS
BEGIN
SET NOCOUNT ON;
	--actualizamos la muerte
	UPDATE muertes 
	   SET ProblemaId = @ProblemaId, 
	       Observaciones = @Observaciones,
		   FotoNecropsia = @FotoNecropsia, 
		   OperadorNecropsia = @OperadorNecropsiaId, 
		   FechaNecropsia = GETDATE(),
		   UsuarioModificacionID = @UsuarioModificacionId, 
		   FechaModificacion = GETDATE(), 
		   EstatusID = @EstatusID,
		   Activo = 0,
		   AnimalID = @AnimalID
	 WHERE MuerteId = @MuerteId;
SET NOCOUNT OFF;  
END

GO
