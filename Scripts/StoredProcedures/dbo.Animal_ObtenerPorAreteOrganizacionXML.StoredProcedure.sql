USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerPorAreteOrganizacionXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ObtenerPorAreteOrganizacionXML]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerPorAreteOrganizacionXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 16/01/2015
-- Description:  Obtiene animales por su disponibilidad
-- =============================================
CREATE PROCEDURE [dbo].[Animal_ObtenerPorAreteOrganizacionXML]
@XmlAnimales XML
AS
BEGIN
	SET NOCOUNT ON;

	CREATE TABLE #tAnimales
	(
		Arete				VARCHAR(100)
		, OrganizacionID	INT
	)

	INSERT INTO #tAnimales
	SELECT 
		  t.item.value('./Arete[1]', 'VARCHAR(100)') AS Arete
		, t.item.value('./OrganizacionIDEntrada[1]', 'INT') AS OrganizacionID
	FROM @XmlAnimales.nodes('ROOT/Animal') AS T(item)
	
	SELECT	A.AnimalID
		,	A.Arete
		,	A.AreteMetalico
		,	A.FechaCompra
		,	A.TipoGanadoID
		,	A.CalidadGanadoID
		,	A.ClasificacionGanadoID
		,	A.PesoCompra
		,	A.OrganizacionIDEntrada
		,	A.FolioEntrada
		,	A.PesoLlegada
		,	A.Paletas
		,	A.CausaRechadoID
		,	A.Venta
		,	A.Cronico
		,	A.CambioSexo
		,	A.Activo
	FROM Animal A(NOLOCK)
	INNER JOIN #tAnimales tA(NOLOCK) 
		ON (A.Arete = tA.Arete
			AND A.OrganizacionIDEntrada = tA.OrganizacionID)

	DROP TABLE #tAnimales

	SET NOCOUNT OFF;
END

GO
