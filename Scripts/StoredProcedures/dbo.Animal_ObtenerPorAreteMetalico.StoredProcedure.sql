USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerPorAreteMetalico]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ObtenerPorAreteMetalico]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerPorAreteMetalico]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Jorge Luis Velazquez Araujo
-- Create date: 06/11/2014
-- Description:	Obtiene un animal por su arete metalico
-- [[Animal_ObtenerPorAreteMetalico]] ''
--======================================================
CREATE PROCEDURE [dbo].[Animal_ObtenerPorAreteMetalico]
@AreteMetalico VARCHAR(15),
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
	WHERE AreteMetalico = @AreteMetalico AND Activo = 1 AND OrganizacionIDEntrada = @OrganizacionID
END
GO
