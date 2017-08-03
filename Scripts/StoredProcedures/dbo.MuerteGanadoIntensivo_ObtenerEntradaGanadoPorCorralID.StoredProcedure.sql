
USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[MuerteGanadoIntensivo_ObtenerEntradaGanadoPorCorralID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[MuerteGanadoIntensivo_ObtenerEntradaGanadoPorCorralID]
GO
/****** Object:  StoredProcedure [dbo].[MuerteGanadoIntensivo_ObtenerEntradaGanadoPorCorralID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Edgar Villarreal
-- Create date: 01-12-2015
-- Description:	Obtiene una entrada de ganado por Por corralID 
-- exec MuerteGanadoIntensivo_ObtenerEntradaGanadoPorCorralID 7448,1
-- =============================================
CREATE PROCEDURE [dbo].[MuerteGanadoIntensivo_ObtenerEntradaGanadoPorCorralID]
	@CorralID INT,
	@Activo BIT
AS
BEGIN	
		SELECT EG.FolioEntrada, 
			   EG.OrganizacionOrigenID, 
			   O.TipoOrganizacionID, 
			   EG.PesoBruto, 
			   EG.PesoTara, 
			   EG.CabezasRecibidas,
			   EG.FechaEntrada,
			   EG.EmbarqueID,--001 
			   EG.EsRuteo
		FROM EntradaGanado (NOLOCK) AS EG 
		INNER JOIN Organizacion (NOLOCK) AS O ON (O.OrganizacionID = EG.OrganizacionOrigenID AND O.Activo = @Activo)
		INNER JOIN Lote L ON L.LoteID = EG.LoteID AND L.CorralID = EG.CorralID AND L.Activo = @Activo
		WHERE  EG.CorralID = @CorralID 
	  AND EG.Activo = @Activo
END

GO




