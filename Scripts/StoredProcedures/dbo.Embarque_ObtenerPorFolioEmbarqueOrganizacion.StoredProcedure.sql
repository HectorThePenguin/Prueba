USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_ObtenerPorFolioEmbarqueOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Embarque_ObtenerPorFolioEmbarqueOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_ObtenerPorFolioEmbarqueOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Embarque_ObtenerPorFolioEmbarqueOrganizacion]
@FolioEmbarque INT
, @OrganizacionID INT	
AS
/*
=============================================
-- Author     : Gilberto Carranza
-- Create date: 2013/11/13
-- Description: Obtiene Embarques por Folio y Organizacion
-- Embarque_ObtenerPorFolioEmbarqueOrganizacion 1,1
--=============================================
*/
BEGIN
	SET NOCOUNT ON;
	DECLARE @EmbarqueID INT
	CREATE TABLE #Embarque
	(
		EmbarqueID INT
		, OrganizacionID INT
		, Organizacion VARCHAR(1000)
		, TipoOrganizacionID INT
		, TipoOrganizacion VARCHAR(1000)
		, FolioEmbarque INT
		, TipoEmbarqueID INT
		, TipoEmbarque VARCHAR(1000)
		, Estatus INT
	)
	INSERT INTO #Embarque
	SELECT 
		EmbarqueID,
		pe.OrganizacionID,
		o.Descripcion as [Organizacion],
		o.TipoOrganizacionID,
		ot.Descripcion as [TipoOrganizacion],
		FolioEmbarque,
		pe.TipoEmbarqueID,
		te.Descripcion as [TipoEmbarque],
		Estatus
	FROM Embarque pe
	INNER JOIN TipoEmbarque  te on te.TipoEmbarqueID = pe.TipoEmbarqueID
	INNER JOIN Organizacion o on o.OrganizacionID = pe.OrganizacionID
	INNER JOIN TipoOrganizacion ot on ot.TipoOrganizacionID = o.TipoOrganizacionID
	WHERE pe.FolioEmbarque = @FolioEmbarque
		AND pe.OrganizacionID = @OrganizacionID
	SET @EmbarqueID = (SELECT TOP 1 EmbarqueID FROM #Embarque)
	SELECT EmbarqueID
		, OrganizacionID
		, Organizacion
		, TipoOrganizacionID
		, TipoOrganizacion
		, FolioEmbarque
		, TipoEmbarqueID
		, TipoEmbarque
		, Estatus 
	FROM #Embarque
	SELECT 
		EmbarqueDetalleID
		, EmbarqueID
		, pd.ProveedorID
		, p.Descripcion [Proveedor]
		, p.CodigoSAP
		, pd.ChoferID
		, c.Nombre
		, c.ApellidoPaterno
		, c.ApellidoMaterno
		, pd.JaulaID
		, j.PlacaJaula
		, pd.CamionID
		, cm.PlacaCamion
		, OrganizacionOrigenID
		, oo.Descripcion as [Origen]
		, oo.TipoOrganizacionID as [TipoOrganizacionOrigenID]
		, too.Descripcion as [TipoOrganizacionOrigen]
		, OrganizacionDestinoID
		, od.Descripcion as [Destino]
		, od.TipoOrganizacionID as [TipoOrganizacionDestinoID]
		, tod.Descripcion as [TipoOrganizacionDestino]
		, FechaSalida
		, FechaLlegada
		, Orden
		, Horas
		, Recibido
		, pd.Activo
		, pd.Comentarios
		, pd.Kilometros
		, p.CorreoElectronico
	FROM EmbarqueDetalle pd
	INNER JOIN Proveedor p on p.ProveedorID = pd.ProveedorID
	INNER JOIN Chofer c on c.ChoferID = pd.ChoferID
	INNER JOIN Jaula j on j.JaulaID = pd.JaulaID
	INNER JOIN Camion cm on cm.CamionID = pd.CamionID
	INNER JOIN Organizacion oo on oo.OrganizacionID = pd.OrganizacionOrigenID
	INNER JOIN TipoOrganizacion too ON oo.TipoOrganizacionID = too.TipoOrganizacionID
	INNER JOIN Organizacion od on od.OrganizacionID = pd.OrganizacionDestinoID
	INNER JOIN TipoOrganizacion tod ON od.TipoOrganizacionID = tod.TipoOrganizacionID
	WHERE EmbarqueID = @EmbarqueID
	SELECT ed.EmbarqueDetalleID, ced.CostoID, ced.Importe, ed.OrganizacionOrigenID, ed.OrganizacionDestinoID  FROM EmbarqueDetalle ed 
    INNER JOIN CostoEmbarqueDetalle ced ON (ed.EmbarqueDetalleID = ced.EmbarqueDetalleID) 
    WHERE ed.EmbarqueID = @EmbarqueID
	DROP TABLE #Embarque
END

GO
