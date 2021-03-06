USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ConsultaPorAnimalID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ConsultaPorAnimalID]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ConsultaPorAnimalID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Pedro Delgado
-- Create date: 14/04/2013
-- Description: Consulta un animal por animalid
-- Empresa: Apinterfaces
-- =============================================
CREATE PROCEDURE [dbo].[Animal_ConsultaPorAnimalID]
@AnimalID INT
AS
BEGIN
	SELECT 
		A.AnimalID,
		A.Arete,
		A.AreteMetalico,
		A.FechaCompra,
		A.TipoGanadoID,
		A.CalidadGanadoID,
		A.ClasificacionGanadoID,
		A.PesoCompra,
		A.OrganizacionIDEntrada,
		A.FolioEntrada,
		A.PesoLlegada,
		A.Paletas,
		A.CausaRechadoID,
		A.Venta,
		A.Cronico,
		A.Activo,
		A.FechaCreacion,
		A.UsuarioCreacionID,
		A.FechaModificacion,
		A.UsuarioModificacionID
	FROM Animal (NOLOCK) A
	WHERE A.AnimalID = @AnimalID
END
GO
