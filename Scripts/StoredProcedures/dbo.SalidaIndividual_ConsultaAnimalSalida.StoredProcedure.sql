USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividual_ConsultaAnimalSalida]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaIndividual_ConsultaAnimalSalida]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividual_ConsultaAnimalSalida]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Pedro Delgado
-- Create date: 03/03/2014
-- Description: Obtiene si el animal se encuentra en la tabla AnimalSalida
-- Origen     : APInterfaces
-- =============================================
CREATE PROCEDURE [dbo].[SalidaIndividual_ConsultaAnimalSalida]
@AnimalID INT
AS 
BEGIN
	SELECT TOP 1 AnimalID
	FROM AnimalSalida (NOLOCK)
	WHERE AnimalID = @AnimalID AND Activo = 1
END

GO
