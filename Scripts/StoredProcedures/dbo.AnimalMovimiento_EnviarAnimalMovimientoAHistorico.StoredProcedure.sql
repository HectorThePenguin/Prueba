USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_EnviarAnimalMovimientoAHistorico]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalMovimiento_EnviarAnimalMovimientoAHistorico]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_EnviarAnimalMovimientoAHistorico]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez Figueroa
-- Create date: 05/04/2014
-- Description:  Enviar AnimalMovimiento a AnimalMovimientoHistorico
-- Origen: APInterfaces
-- AnimalMovimiento_EnviarAnimalMovimientoAHistorico 1
-- =============================================
CREATE PROCEDURE [dbo].[AnimalMovimiento_EnviarAnimalMovimientoAHistorico]
	@AnimalID BIGINT
AS
  BEGIN
    SET NOCOUNT ON
		INSERT INTO AnimalMovimientoHistorico (AnimalMovimientoID, 
											   AnimalID, 
											   OrganizacionID, 
											   CorralID, 
											   LoteID, 
											   FechaMovimiento, 
											   Peso,
											   Temperatura, 
											   TipoMovimientoID, 
											   TrampaID, 
											   OperadorID, 
											   Observaciones, 
											   LoteIDOrigen,
											   AnimalMovimientoIDAnterior,
											   Activo, 
											   FechaCreacion, 
											   UsuarioCreacionID,
											   FechaModificacion, 
											   UsuarioModificacionID)
		SELECT AnimalMovimientoID, 
			   AnimalID, 
			   OrganizacionID, 
			   CorralID, 
			   LoteID, 
			   FechaMovimiento, 
			   Peso,
			   Temperatura, 
			   TipoMovimientoID, 
			   TrampaID, 
			   OperadorID, 
			   Observaciones, 
			   LoteIDOrigen,
			   AnimalMovimientoIDAnterior,
			   Activo, 
			   FechaCreacion, 
			   UsuarioCreacionID,
		       FechaModificacion, 
			   UsuarioModificacionID 
	     FROM AnimalMovimiento
		WHERE AnimalID = @AnimalID
	SET NOCOUNT OFF
  END

GO
