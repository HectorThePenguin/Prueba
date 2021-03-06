USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_ObtenerPendientesPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Embarque_ObtenerPendientesPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_ObtenerPendientesPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Raul Esquer
-- Create date: 04-11-2013
-- Description:	Obtiene un listado de las programaciones pendientes 
-- Embarque_ObtenerPendientesPorPagina 0, 0, 0, 1, 1, 10
-- =============================================
CREATE PROCEDURE [dbo].[Embarque_ObtenerPendientesPorPagina]
	@FolioEmbarque INT, 
	@TipoOrganizacion INT, 
	@OrganizacionOrigenID INT, 
	@OrganizacionID INT, 
	@Estatus INT, 
	@Inicio INT, 
	@Limite INT 
AS
BEGIN	
	SET NOCOUNT ON;
	SELECT 
		ROW_NUMBER() OVER ( ORDER BY PE.FolioEmbarque ASC) AS RowNum,	    
		PE.EmbarqueID,
		PE.FolioEmbarque, 		
		OT.Descripcion TipoOrganizacion,
		OO.Descripcion OrganizacionOrigen,
		PD.FechaSalida, 
		TE.Descripcion TipoEmbarque, 
		C.Nombre + ' ' + C.ApellidoPaterno + ' ' + ApellidoMaterno Chofer,
		CM.PlacaCamion
	INTO #EmbarqueOrigen 
	FROM Embarque PE			
	INNER JOIN EmbarqueDetalle PD
		ON PE.EmbarqueID = PD.EmbarqueID AND PD.Orden = 1
	INNER JOIN Organizacion OO		
		ON PD.OrganizacionOrigenID = OO.OrganizacionID	
	INNER JOIN TipoOrganizacion OT
		ON OT.TipoOrganizacionID = OO.TipoOrganizacionID
	INNER JOIN TipoEmbarque TE
		ON TE.TipoEmbarqueID = PE.TipoEmbarqueID
	INNER JOIN Chofer C
		ON C.ChoferID = PD.ChoferID
	INNER JOIN Camion CM
		ON CM.CamionID = PD.CamionID
	WHERE @FolioEmbarque IN (0,PE.FolioEmbarque)
	  AND @OrganizacionID  IN (PE.OrganizacionID, 0)
	  AND @TipoOrganizacion IN (OT.TipoOrganizacionID, 0) 
	  AND @OrganizacionOrigenID IN (PD.OrganizacionOrigenID, 0) 
	  AND @Estatus IN (PE.Estatus) 
	SELECT
		PE.EmbarqueID,						
		OD.Descripcion OrganizacionDestino, 
		PD.FechaLlegada		
	INTO #EmbarqueDestino 
	FROM Embarque PE			
	INNER JOIN EmbarqueDetalle PD
		ON PE.EmbarqueID = PD.EmbarqueID 	
	INNER JOIN Organizacion OD
		ON PD.OrganizacionDestinoID = OD.OrganizacionID 
		AND PD.Orden = (Select Max(Orden) 
						From EmbarqueDetalle SPD
						WHERE SPD.EmbarqueID = PE.EmbarqueID)
	WHERE @FolioEmbarque IN (0,PE.FolioEmbarque)	
	SELECT DISTINCT
		PO.EmbarqueID,
		PO.FolioEmbarque, 		
		PO.TipoOrganizacion,
		PO.OrganizacionOrigen,
		PD.OrganizacionDestino,
		PO.FechaSalida, 
		PD.FechaLlegada, 
		PO.TipoEmbarque, 
		PO.Chofer, 
		PO.PlacaCamion
	FROM #EmbarqueOrigen PO
	INNER JOIN #EmbarqueDestino PD
		ON PO.EmbarqueID = PD.EmbarqueID
	WHERE   RowNum BETWEEN @Inicio AND @Limite	
	ORDER BY PO.EmbarqueID
	Select 
		COUNT(DISTINCT EmbarqueID)AS TotalReg 
	From #EmbarqueOrigen
	DROP TABLE #EmbarqueOrigen 
	DROP TABLE #EmbarqueDestino 	
END

GO
