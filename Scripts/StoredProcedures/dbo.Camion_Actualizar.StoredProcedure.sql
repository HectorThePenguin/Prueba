USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Camion_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Camion_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Camion_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 31/10/2013
-- Description:  Guardar un Camion.
-- Camion_Actualizar 'Camion001', 1
-- =============================================
-- =============================================
-- Edito:    Guillermo Osuna
-- Create date: 23/may/2017
-- Description:  Se agregaron los camos NumEconomico,MarcaID, Modelo, Color, Boletinado y Descripcion
-- Camion_Actualizar 'Camion001', 1, 170-490, 'R480', 1, 2009, 'Rojo', 1, 'Observaciones', 1, '1'
-- =============================================
CREATE PROCEDURE [dbo].[Camion_Actualizar]		
	@CamionID INT,
	@ProveedorID           INT,
	@PlacaCamion           VARCHAR(10),
	@NumEconomico		   VARCHAR(10),
	@MarcaID			   INT,
	@Modelo				   INT,
	@Color				   VARCHAR(255),
	@Boletinado			   BIT,
	@Observaciones		   VARCHAR(255),
	@Activo                BIT,
	@UsuarioModificacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Camion 
		SET ProveedorID = @ProveedorID,
			PlacaCamion = @PlacaCamion,
			NumEconomico = @NumEconomico,
			MarcaID = @MarcaID,
			Modelo = @Modelo,
			Color = @Color,
			Boletinado = @Boletinado,
			Observaciones = @Observaciones,
			Activo = @Activo, 
			FechaModificacion = GETDATE(),
			UsuarioModificacionID = @UsuarioModificacionID
	WHERE CamionID = @CamionID
	SET NOCOUNT OFF;
END

GO