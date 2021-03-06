USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerAnimalesPorCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ObtenerAnimalesPorCorral]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerAnimalesPorCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Ramses Santos
-- Fecha: 12/02/2014
-- Origen: APInterfaces
-- Descripción:	Obtiene los aretes para el corral de enfermeria especificado
-- EXEC Animal_ObtenerAnimalesPorCorral 'ce1', 4 --
-- =============================================
CREATE PROCEDURE [dbo].[Animal_ObtenerAnimalesPorCorral]
@Codigo CHAR(10),
@OrganizacionID INT
AS
BEGIN
	SELECT 
		A.[AnimalID],
		A.[Arete],
		A.[AreteMetalico],
		A.[FechaCompra],
		A.[TipoGanadoID],
		A.[CalidadGanadoID],
		A.[ClasificacionGanadoID],
		A.[PesoCompra],
		A.[OrganizacionIDEntrada],
		A.[FolioEntrada],
		A.[PesoLlegada],
		A.[Paletas],
		A.[CausaRechadoID],
		A.[Venta],
		A.[Cronico],
		A.[Activo],
		A.[UsuarioCreacionID],
		A.[UsuarioModificacionID],
		TG.Sexo
	FROM Corral C
	INNER JOIN AnimalMovimiento AM (NOLOCK)
		ON AM.CorralID = C.CorralID
	INNER JOIN Animal A (NOLOCK)
		ON A.AnimalID = AM.AnimalID
	INNER JOIN TipoGanado TG 
		ON TG.TipoGanadoID = A.TipoGanadoID
	WHERE C.Codigo = @Codigo
		AND AM.Activo = 1
		AND A.Activo = 1
		AND C.OrganizacionID = @OrganizacionID
END
GO
