USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TraspasoGanado_ObtenerAretesCorral]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TraspasoGanado_ObtenerAretesCorral]
GO
/****** Object:  StoredProcedure [dbo].[TraspasoGanado_ObtenerAretesCorral]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Jesus.Alvarez
-- Fecha: 12/02/2014
-- Origen: APInterfaces
-- Descripción:	Obtiene los aretes para el corral de enfermeria especificado
-- EXEC TraspasoGanado_ObtenerAretesCorral '011', 4, 3,1,8
-- EXEC TraspasoGanado_ObtenerAretesCorral '010', 4, 3,1,8
-- =============================================
CREATE PROCEDURE [dbo].[TraspasoGanado_ObtenerAretesCorral] @Codigo CHAR(10)
	,@OrganizacionID INT
	,@GrupoCorralID INT
	,@Activo INT /*,
@TipoCorral INT*/
AS
BEGIN
	SELECT A.AnimalID
		,A.Arete
		,A.AreteMetalico
	FROM Corral C
	INNER JOIN TipoCorral TC ON TC.TipoCorralID = C.TipoCorralID
	INNER JOIN Lote L ON L.CorralID = C.CorralID
	INNER JOIN AnimalMovimiento AM(NOLOCK) ON AM.LoteID = L.LoteID
		AND AM.CorralID = C.CorralID
	INNER JOIN Animal A(NOLOCK) ON A.AnimalID = AM.AnimalID
	WHERE C.Codigo = @Codigo
		AND TC.GrupoCorralID = @GrupoCorralID
		AND L.Activo = @Activo
		AND AM.Activo = @Activo
		AND A.Activo = @Activo
		AND C.OrganizacionID = @OrganizacionID
		AND L.OrganizacionID = @OrganizacionID
		AND A.OrganizacionIDEntrada = @OrganizacionID
		AND AM.OrganizacionID = @OrganizacionID
	ORDER BY A.Arete
		--AND TC.TipoCorralID != @TipoCorral
END

GO
