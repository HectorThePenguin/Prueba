USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalCosto_EnviarAnimalCostoAHistorico]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalCosto_EnviarAnimalCostoAHistorico]
GO
/****** Object:  StoredProcedure [dbo].[AnimalCosto_EnviarAnimalCostoAHistorico]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    César Valdez Figueroa
-- Create date: 05/04/2014
-- Description:  Enviar Animal Costo a AnimalCostoHistorico
-- Origen: APInterfaces
-- AnimalCosto_EnviarAnimalCostoAHistorico 1
-- =============================================
CREATE PROCEDURE [dbo].[AnimalCosto_EnviarAnimalCostoAHistorico]
	@AnimalID BIGINT
AS
  BEGIN
    SET NOCOUNT ON

		INSERT INTO AnimalCostoHistorico (AnimalCostoID, 
										  AnimalID, 
										  FechaCosto, 
										  CostoID, 
            							  TipoReferencia, 
										  FolioReferencia, 
										  Importe, 
										  FechaCreacion, 
										  UsuarioCreacionID, 
										  FechaModificacion, 
										  UsuarioModificacionID)
		SELECT AnimalCostoID, 
			   AnimalID, 
			   FechaCosto, 
			   CostoID, 
	  		   TipoReferencia, 
			   FolioReferencia, 
			   Importe, 
			   FechaCreacion, 
			   UsuarioCreacionID, 
			   FechaModificacion, 
			   UsuarioModificacionID
		  FROM AnimalCosto
		 WHERE AnimalID = @AnimalID
			
	SET NOCOUNT OFF
  END


GO
