USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CatPrecioGanadoOrganizacion_ObtenerListaPrecio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CatPrecioGanadoOrganizacion_ObtenerListaPrecio]
GO
/****** Object:  StoredProcedure [dbo].[CatPrecioGanadoOrganizacion_ObtenerListaPrecio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Ruben Guzman
-- Create date: 19/08/2015
-- Description:  Obtiene los precios del ganado de las distintas organizaciones
-- CatPrecioGanadoOrganizacion_ObtenerListaPrecio
CREATE PROCEDURE [dbo].[CatPrecioGanadoOrganizacion_ObtenerListaPrecio]
	@SociedadID INT = NULL,
	@ZonaID INT = NULL
AS
BEGIN 
SET NOCOUNT ON;
	SELECT 
		OrganizacionId = O.OrganizacionId,
		Organizacion = O.Descripcion,
		MachoPesadoId = 1,
		MachoPesado = dbo.RegresaPrecioPromedio( O.OrganizacionId, 1),
		PesoPromedioMachoPesado = dbo.RegresaPesoPromedio( O.OrganizacionId, 1),
		ToreteId = 2,
		Torete = dbo.RegresaPrecioPromedio( O.OrganizacionId, 2),
		PesoPromedioTorete = dbo.RegresaPesoPromedio( O.OrganizacionId, 2),
		BecerroLigeroId = 3,
		BecerroLigero = dbo.RegresaPrecioPromedio( O.OrganizacionId, 3),
		PesoPromedioBecerroLigero = dbo.RegresaPesoPromedio( O.OrganizacionId, 3),
		BecerroId = 4,
		Becerro = dbo.RegresaPrecioPromedio( O.OrganizacionId, 4),
		PesoPromedioBecerro = dbo.RegresaPesoPromedio( O.OrganizacionId, 4),
		VaquillaTipo2Id = 5,
		VaquillaTipo2 = dbo.RegresaPrecioPromedio( O.OrganizacionId, 5),
		PesoPromedioVaquillaTipo2 = dbo.RegresaPesoPromedio( O.OrganizacionId, 5),
		HembraPesadaId = 6,
		HembraPesada = dbo.RegresaPrecioPromedio( O.OrganizacionId, 6),
		PesoPromedioHembraPesada = dbo.RegresaPesoPromedio( O.OrganizacionId, 6),
		VaquillaId = 7,
		Vaquilla = dbo.RegresaPrecioPromedio( O.OrganizacionId, 7),
		PesoPromedioVaquilla = dbo.RegresaPesoPromedio( O.OrganizacionId, 7),
		BecerraId = 8,
		Becerra = dbo.RegresaPrecioPromedio( O.OrganizacionId, 8),
		PesoPromedioBecerra = dbo.RegresaPesoPromedio( O.OrganizacionId, 8),
		BecerraLigeraId = 9,
		BecerraLigera = dbo.RegresaPrecioPromedio( O.OrganizacionId, 9),
		PesoPromedioBecerraLigera = dbo.RegresaPesoPromedio( O.OrganizacionId, 9),
		ToretePesadoId = 10,
		ToretePesado = dbo.RegresaPrecioPromedio( O.OrganizacionId, 10),
		PesoPromedioToretePesado = dbo.RegresaPesoPromedio( O.OrganizacionId, 10),
		SociedadID = S.SociedadID,
		ZonaID = Z.ZonaID
	FROM Sociedad S (NOLOCK)
	INNER JOIN Zona Z
		ON Z.PaisID = S.PaisID
	INNER JOIN Organizacion O
		ON O.ZonaID = Z.ZonaID	
	WHERE ISNULL(@SociedadID,0) IN (0,S.SociedadID) AND ISNULL(@ZonaID,0) IN (0,Z.ZonaID)
	GROUP BY O.OrganizacionId, O.Descripcion, S.SociedadID, Z.ZonaID
	ORDER BY O.OrganizacionId
SET NOCOUNT OFF;
END
GO