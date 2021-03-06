USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TraspasoGanado_ObtenerAretesRecepcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TraspasoGanado_ObtenerAretesRecepcion]
GO
/****** Object:  StoredProcedure [dbo].[TraspasoGanado_ObtenerAretesRecepcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor: Pedro.Delgado
-- Fecha: 08/09/2014
-- Origen: APInterfaces
-- Descripción:	Obtiene los aretes para el corral de recepcion especificado
-- EXEC TraspasoGanado_ObtenerAretesRecepcion '803', 1, 1,1
-- =============================================
CREATE PROCEDURE [dbo].[TraspasoGanado_ObtenerAretesRecepcion] @Codigo VARCHAR(10)
	,@OrganizacionID INT
	,@GrupoCorralID INT
	,@Activo BIT
AS
BEGIN
	SELECT DISTINCT C.CorralID
		,C.Codigo
		,L.LoteID
		,L.Lote
		,EG.FolioEntrada
		,ISNULL(ISA.Arete, CAST(L.Cabezas AS VARCHAR(100)) + ' Animales') AS Arete
		,ISNULL(ISA.AreteMetalico,'') AS AreteMetalico
		,ISNULL(ISA.PesoCompra, 0) AS PesoCompra
		,ISNULL(ISA.TipoGanadoID, 0) AS TipoGanadoID
		,ISNULL(ISA.PesoOrigen, 0) AS PesoOrigen
		,ISNULL(ISA.FechaRegistro, GETDATE()) AS FechaRegistro
		,ISNULL(ISA.OrganizacionID, 0) AS OrganizacionID
		,TC.GrupoCorralID
	FROM Corral C
	INNER JOIN Lote L ON (C.CorralID = L.CorralID)
	INNER JOIN EntradaGanado EG ON (
			EG.CorralID = C.CorralID
			AND EG.LoteID = L.LoteID
			)
	LEFT JOIN InterfaceSalida IST ON (
			IST.SalidaID = EG.FolioOrigen
			AND IST.OrganizacionID = EG.OrganizacionOrigenID
			)
	LEFT JOIN InterfaceSalidaAnimal ISA ON (
			IST.SalidaID = ISA.SalidaID
			AND ISA.OrganizacionID = EG.OrganizacionOrigenID
			)
	INNER JOIN TipoCorral TC ON (TC.TipoCorralID = C.TipoCorralID)
	WHERE C.Codigo = @Codigo
		AND C.OrganizacionID = @OrganizacionID
		AND L.OrganizacionID = @OrganizacionID
		AND EG.OrganizacionID = @OrganizacionID
		--AND COALESCE(IST.OrganizacionDestinoID, @OrganizacionID) = @OrganizacionID
		AND TC.GrupoCorralID = @GrupoCorralID
		AND L.Activo = @Activo
		AND C.Activo = @Activo
		AND EG.Activo = @Activo
END

GO
