USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaAnimal_ActualizarAnimalID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalidaAnimal_ActualizarAnimalID]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaAnimal_ActualizarAnimalID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Emir Lezama
-- Create date: 31/07/2015
-- Description: Actualiza 
-- SpName     : InterfaceSalidaAnimal_ActualizarAnimalID 
--======================================================
CREATE PROCEDURE [dbo].[InterfaceSalidaAnimal_ActualizarAnimalID]
	@SalidaID 			INT
	,@OrganizacionID 	INT
	,@Arete 			VARCHAR(15)
	,@AreteMetalico 	VARCHAR(15)
	,@AnimalID 			BIGINT
AS
BEGIN
	IF @Arete= ''
		BEGIN
			SET @Arete = NULL;
		END
	IF @AreteMetalico= ''
		BEGIN
			SET @AreteMetalico = NULL;
		END
	UPDATE InterfaceSalidaAnimal SET AnimalID = @AnimalID
	WHERE OrganizacionID = @OrganizacionID AND SalidaID = @SalidaID AND 
	( Arete = COALESCE(@Arete, 'x') OR AreteMetalico = COALESCE(@AreteMetalico, 'x'))
END

GO
