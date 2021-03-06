USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerAnimalesPorLoteID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ObtenerAnimalesPorLoteID]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerAnimalesPorLoteID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Jesus Alvarez
-- Fecha: 11/03/2014
-- Origen: APInterfaces
-- Descripción:	Obtiene los aretes para el lote especificado
-- EXEC Animal_ObtenerAnimalesPorLote 'ce1', 4 --
-- =============================================
CREATE PROCEDURE [dbo].[Animal_ObtenerAnimalesPorLoteID]
@LoteID INT,
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
		A.[UsuarioModificacionID]
	FROM Lote L WITH (INDEX(IX_Lote_LoteIDOrganizacionID))
		INNER JOIN AnimalMovimiento (NOLOCK) AM ON AM.LoteID = L.LoteID
		INNER JOIN Animal (NOLOCK) A ON A.AnimalID = AM.AnimalID
	WHERE L.OrganizacionID = @OrganizacionID	 
	AND AM.Activo = 1
	AND A.Activo = 1
	--AND L.Activo = 1
	AND L.LoteID = @LoteID
END
GO
