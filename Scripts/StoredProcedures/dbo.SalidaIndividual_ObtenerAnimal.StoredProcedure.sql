USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividual_ObtenerAnimal]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaIndividual_ObtenerAnimal]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividual_ObtenerAnimal]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 27/02/2014
-- Description:	Obtiene un animal para verificar su existencia
-- [SalidaIndividual_ObtenerAnimal] ''
--======================================================
CREATE PROCEDURE [dbo].[SalidaIndividual_ObtenerAnimal]
@Arete VARCHAR(15),
@OrganizacionID INT
AS
BEGIN
	SELECT 
		AnimalID,
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
		UsuarioCreacionID
	FROM Animal (NOLOCK)
	WHERE Arete = @Arete AND Activo = 1 AND OrganizacionIDEntrada = @OrganizacionID
END

GO
