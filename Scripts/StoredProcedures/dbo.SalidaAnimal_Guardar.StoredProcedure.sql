USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaAnimal_Guardar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaAnimal_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[SalidaAnimal_Guardar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez Figueroa
-- Create date: 18/02/2014
-- Description:  Guardar el Salida Animal
-- Origen: APInterfaces
-- SalidaAnimal_Guardar 1,1,1,1,1
-- =============================================
CREATE PROCEDURE [dbo].[SalidaAnimal_Guardar]
	@SalidaGanadoID INT,
	@AnimalID INT,
	@LoteID INT,
	@Activo BIT,
	@UsuarioCreacionID INT
AS
  BEGIN
      SET NOCOUNT ON
	INSERT INTO SalidaAnimal (SalidaGanadoID, 
							  AnimalID, 
							  LoteID, 
							  Activo, 
							  FechaCreacion, 
							  UsuarioCreacionID) 
	VALUES (@SalidaGanadoID, 
			@AnimalID, 
			@LoteID , 
			@Activo, 
			GETDATE(), 
			@UsuarioCreacionID)
	SET NOCOUNT OFF
  END

GO
