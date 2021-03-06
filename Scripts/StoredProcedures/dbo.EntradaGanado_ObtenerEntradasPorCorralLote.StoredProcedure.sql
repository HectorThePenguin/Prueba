USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerEntradasPorCorralLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerEntradasPorCorralLote]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerEntradasPorCorralLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez Figueroa
-- Create date: 10/09/2014
-- Description:  Obtener Entradas costeadas sin prorratear
-- Origen: APInterfaces
-- EntradaGanado_ObtenerEntradasPorCorralLote 2,2
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerEntradasPorCorralLote]
	@CorralID INT,
    @LoteID INT
AS
  BEGIN
    SET NOCOUNT ON
	SELECT EG.EntradaGanadoID, 
	       EG.LoteID, 
		   L.CabezasInicio, 
		   EG.FolioEntrada, 
		   dbo.ObtenerPartidasCorralLote(EG.CorralID,EG.LoteID,'|') "FolioEntradaAgrupado",
		   EG.FolioOrigen, 
		   EG.OrganizacionOrigenID,
		   EG.OrganizacionID
	  FROM EntradaGanado EG
	 INNER JOIN EntradaGanadoCosteo EGC ON ( EGC.EntradaGanadoID = EG.EntradaGanadoID AND EGC.Activo = 1)
	 INNER JOIN Lote L ON ( L.LoteID = EG.LoteID)
	 INNER JOIN Organizacion O ON (EG.OrganizacionID = O.OrganizacionID AND O.Activo = 1)
	 WHERE EGC.Prorrateado = 1
	   AND EG.Activo = 1
	   AND EG.CorralID = @CorralID
	   AND EG.LoteID = @LoteID
	SET NOCOUNT OFF
  END

GO
