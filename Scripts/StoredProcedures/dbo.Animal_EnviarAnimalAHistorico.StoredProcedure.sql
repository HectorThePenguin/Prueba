USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_EnviarAnimalAHistorico]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_EnviarAnimalAHistorico]
GO
/****** Object:  StoredProcedure [dbo].[Animal_EnviarAnimalAHistorico]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez Figueroa
-- Create date: 05/04/2014
-- Description:  Enviar Animal a AnimalHistorico
-- Origen: APInterfaces
-- Animal_EnviarAnimalAHistorico 1
-- =============================================
CREATE PROCEDURE [dbo].[Animal_EnviarAnimalAHistorico]
	@AnimalID INT
AS
  BEGIN
    SET NOCOUNT ON
		INSERT INTO AnimalHistorico (AnimalID, 
									 Arete, 
									 AreteMetalico, 
									 FechaCompra, 
									 TipoGanadoID, 
									 CalidadGanadoID, 
									 ClasificacionGanadoID, 
									 PesoCompra,
									 OrganizacionIDEntrada, 
									 FolioEntrada, 
									 PesoLlegada, 
									 Paletas, 
									 CausaRechadoID, 
									 Venta, 
									 Cronico, 
									 Activo, 
									 FechaCreacion, 
									 UsuarioCreacionID, 
									 FechaModificacion, 
									 UsuarioModificacionID)
		SELECT AnimalID, 
		       Arete, 
			   AreteMetalico, 
			   FechaCompra, 
			   TipoGanadoID, 
			   CalidadGanadoID, 
			   ClasificacionGanadoID, 
			   PesoCompra,
		       OrganizacionIDEntrada, 
			   FolioEntrada, 
			   PesoLlegada, 
			   Paletas, 
			   CausaRechadoID, 
			   Venta, 
			   Cronico, 
			   Activo, 
		       FechaCreacion, 
			   UsuarioCreacionID, 
			   FechaModificacion, 
			   UsuarioModificacionID 
	     FROM Animal 
		WHERE AnimalID = @AnimalID
	SET NOCOUNT OFF
  END

GO
