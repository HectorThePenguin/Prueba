USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Embarque_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : José Gilberto Quintero López
-- Create date: 2013/11/05
-- Description: 
-- Embarque_ObtenerPorID 1
--=============================================
CREATE PROCEDURE [dbo].[Embarque_ObtenerPorID]
(
	@EmbarqueID INT
)
AS
BEGIN
	SET NOCOUNT ON;
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
	WHERE EmbarqueID = @EmbarqueID
	SELECT 
		EmbarqueDetalleID,
		EmbarqueID,
		pd.ProveedorID,
		p.Descripcion [Proveedor],		
		pd.ChoferID,
		c.Nombre,
		c.ApellidoPaterno,
		c.ApellidoMaterno,
		pd.JaulaID,
		j.PlacaJaula,
		pd.CamionID,
		cm.PlacaCamion,
		OrganizacionOrigenID,
		oo.Descripcion as [Origen],
		oo.TipoOrganizacionID as [TipoOrganizacionOrigenID],
		OrganizacionDestinoID,
		od.Descripcion as [Destino],
		od.TipoOrganizacionID as [TipoOrganizacionDestinoID],
		FechaSalida,
		FechaLlegada,
		Orden,
		Horas,
		Recibido,
		pd.Activo,
		pd.Comentarios,
		p.CorreoElectronico
	FROM EmbarqueDetalle pd
	INNER JOIN Proveedor p on p.ProveedorID = pd.ProveedorID
	INNER JOIN Chofer c on c.ChoferID = pd.ChoferID
	INNER JOIN Jaula j on j.JaulaID = pd.JaulaID
	INNER JOIN Camion cm on cm.CamionID = pd.CamionID
	INNER JOIN Organizacion oo on oo.OrganizacionID = pd.OrganizacionOrigenID
	INNER JOIN Organizacion od on od.OrganizacionID = pd.OrganizacionDestinoID
	WHERE EmbarqueID = @EmbarqueID
	SELECT ed.EmbarqueDetalleID, ced.CostoID, ced.Importe FROM EmbarqueDetalle ed 
    INNER JOIN CostoEmbarqueDetalle ced ON (ed.EmbarqueDetalleID = ced.EmbarqueDetalleID) 
    WHERE ed.EmbarqueID = @EmbarqueID
	SET NOCOUNT OFF;
END

GO
