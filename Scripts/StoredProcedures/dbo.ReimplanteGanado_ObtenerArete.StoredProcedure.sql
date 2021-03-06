USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ObtenerArete]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReimplanteGanado_ObtenerArete]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ObtenerArete]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor: cesar.valdez
-- Fecha: 2013-12-26
-- Descripción:	Obtiene el Numero Individual o Arete
-- EXEC ReimplanteGanado_ObtenerArete "22222", 1, 5
-- =============================================
CREATE PROCEDURE [dbo].[ReimplanteGanado_ObtenerArete]
	@Arete VARCHAR(15),
	@OrganizacionID INT,
	@MovimientoCorte INT
	
AS
BEGIN

	SELECT TOP 1
		A.AnimalID,
		A.Arete,
		A.AreteMetalico,
		A.TipoGanadoID,
		A.CalidadGanadoID,
		A.PesoCompra,
		A.OrganizacionIDEntrada,
		A.FolioEntrada,
		A.PesoLlegada,
		A.Venta,
		A.FechaCompra,
		C.CorralID,
		C.Codigo,
		C.TipoCorralID,
		C.Capacidad,
		LR.NumeroReimplante,
		L.LoteID,
		L.Lote,
		L.TipoCorralID,
		L.CabezasInicio,
		L.Cabezas,
		L.FechaCierre,
		L.FechaDisponibilidad,
		PR.FolioProgramacionID,
		LR.LoteReimplanteID,
		TOO.TipoOrganizacionID TipoOrigen,
		PR.Fecha FechaReimplante,		
		COALESCE(	
		 (SELECT TOP 1 Peso
				FROM AnimalMovimiento amov 
			 WHERE amov.AnimalID = A.AnimalID 
				 AND amov.TipoMovimientoID IN (@MovimientoCorte,7)
		   ORDER BY amov.AnimalMovimientoID ASC)
		,0)PesoCorte
	FROM Animal A(NOLOCK)
	INNER JOIN AnimalMovimiento AM(NOLOCK) ON A.AnimalID = AM.AnimalID
	INNER JOIN Corral C(NOLOCK) ON C.CorralID = AM.CorralID
	INNER JOIN Lote L(NOLOCK) ON L.CorralID = C.CorralID
	LEFT JOIN ProgramacionReimplante PR(NOLOCK) ON PR.LoteID = AM.LoteID AND PR.Activo = 1
	LEFT JOIN LoteProyeccion LP(NOLOCK) ON LP.LoteID = AM.LoteID
	LEFT JOIN LoteReimplante LR(NOLOCK) ON LR.LoteProyeccionID = LP.LoteProyeccionID
	INNER JOIN Organizacion O(NOLOCK) ON A.OrganizacionIDEntrada = O.OrganizacionID
	INNER JOIN TipoOrganizacion TOO(NOLOCK) ON O.TipoOrganizacionID = TOO.TipoOrganizacionID
	WHERE A.Arete = @Arete
	AND A.OrganizacionIDEntrada = @OrganizacionID
	AND A.Activo = 1 AND AM.Activo = 1
	AND L.Activo = 1

END


GO
