USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerActivosPorCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ObtenerActivosPorCorral]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerActivosPorCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--************************************
--Autor	  : Gilberto Carranza
--Fecha	  : 11/11/2013
--Proposito : Ontener Lote Por Corral
--Lote_ObtenerActivosPorCorral 1, 1
--************************************
CREATE PROCEDURE [dbo].[Lote_ObtenerActivosPorCorral]
	@OrganizacionID INT,
	@CorralID INT
AS
BEGIN
	SET NOCOUNT ON
	SELECT 
		count(LoteID) 	    
	FROM Lote
	WHERE OrganizacionID = @OrganizacionID
		AND CorralID = @CorralID		
		AND Activo = 1
		AND FechaCierre IS NOT NULL
	SET NOCOUNT OFF
END

GO
