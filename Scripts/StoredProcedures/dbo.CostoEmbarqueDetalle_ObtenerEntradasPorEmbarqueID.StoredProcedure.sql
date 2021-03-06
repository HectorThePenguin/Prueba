USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CostoEmbarqueDetalle_ObtenerEntradasPorEmbarqueID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CostoEmbarqueDetalle_ObtenerEntradasPorEmbarqueID]
GO
/****** Object:  StoredProcedure [dbo].[CostoEmbarqueDetalle_ObtenerEntradasPorEmbarqueID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	Cesar Valdez
-- Create date: 27/06/2014
-- Origen: APInterfaces
-- Description:	Obtiene las entradas que pertenecen al EmbarqueID
-- execute CostoEmbarqueDetalle_ObtenerEntradasPorEmbarqueID 172
-- =============================================
CREATE PROCEDURE [dbo].[CostoEmbarqueDetalle_ObtenerEntradasPorEmbarqueID]
	@EmbarqueID INT
AS
BEGIN	
	SET NOCOUNT ON;	
	
	SELECT EG.EntradaGanadoID,
	       EG.FolioEntrada,
  		   EG.Costeado,
		   EG.PesoBruto,
		   EG.PesoTara,
		   ED.OrganizacionOrigenID,
		   TOR.TipoOrganizacionID
	  FROM EntradaGanado EG
	 INNER JOIN EmbarqueDetalle ED ON EG.EmbarqueID = ED.EmbarqueID AND EG.OrganizacionOrigenID = ED.OrganizacionOrigenID AND ED.Activo = 1
	 INNER JOIN Organizacion O ON O.OrganizacionID = EG.OrganizacionOrigenID
	 INNER JOIN TipoOrganizacion TOR ON TOR.TipoOrganizacionID = O.TipoOrganizacionID
	 WHERE ED.EmbarqueID = @EmbarqueID

	SET NOCOUNT OFF;	

END
GO
