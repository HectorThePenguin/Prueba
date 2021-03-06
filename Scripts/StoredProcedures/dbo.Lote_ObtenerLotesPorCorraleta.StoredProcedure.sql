USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerLotesPorCorraleta]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ObtenerLotesPorCorraleta]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerLotesPorCorraleta]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Autor:		Roque.Solis
-- Create date: 2014/03/03
-- Description: SP para obtener todos los lotes asociados a una corraleta
-- Origen     : APInterfaces
-- EXEC Lote_ObtenerLotesPorCorraleta 2,3,4
--=============================================
CREATE PROCEDURE [dbo].[Lote_ObtenerLotesPorCorraleta]
@CorraletaID INT,
@GrupoCorralEnfermeria INT,
@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON
	SELECT 
	   COUNT(1) Cabezas,
		 L.LoteID,
		 L.OrganizacionID,
		 L.CorralID,
		 L.Lote
	FROM AnimalSalida ANS (NOLOCK)
	INNER JOIN Lote L (NOLOCK) ON L.LoteID = ANS.LoteID
	INNER JOIN Corral C (NOLOCK) ON L.CorralID= C.CorralID
	INNER JOIN TipoCorral TC (NOLOCK) ON C.TipoCorralID= TC.TipoCorralID
	WHERE ANS.CorraletaID = @CorraletaID 
	AND TC.GrupoCorralID = @GrupoCorralEnfermeria
	AND L.OrganizacionID = @OrganizacionID
	AND ANS.Activo = 1
	AND L.Activo = 1
	GROUP BY L.LoteID,L.OrganizacionID,L.CorralID,L.Lote
	SET NOCOUNT OFF
END

GO
