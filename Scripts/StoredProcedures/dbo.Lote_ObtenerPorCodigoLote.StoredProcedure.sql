USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPorCodigoLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ObtenerPorCodigoLote]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPorCodigoLote]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Jorge Luis Velazquez Araujo
-- Fecha: 24/06/2015
-- Descripci�n:	Obtiene el lote del corral
-- EXEC Lote_ObtenerPorCodigoLote 1,1
-- =============================================
CREATE PROCEDURE [dbo].[Lote_ObtenerPorCodigoLote]
	@OrganizacionID INT, 
	@CorralID INT,
	@Lote varchar(20),
	@Activo BIT = null
AS
BEGIN
	SET NOCOUNT ON
	SELECT  L.LoteID,
			L.Lote,
			L.OrganizacionID,
			L.CorralID,
			L.TipoCorralID,
			L.TipoProcesoID,
			L.FechaInicio,
			L.CabezasInicio,
			L.FechaCierre,
			L.Cabezas,
			L.FechaDisponibilidad,
			L.DisponibilidadManual,
			L.Activo					
	 FROM Lote L
	INNER JOIN Corral C ON C.CorralID = L.CorralID
	WHERE C.OrganizacionID = @OrganizacionID
	  AND C.CorralID = @CorralID	  
	  AND L.Lote = @Lote
	  AND (L.Activo = @Activo OR @Activo is null)
	SET NOCOUNT OFF
END

GO
