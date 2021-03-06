USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_ObtenerPorFolioEmbarque]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Embarque_ObtenerPorFolioEmbarque]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_ObtenerPorFolioEmbarque]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 2013/11/05
-- Description: 
-- Embarque_ObtenerPorFolioEmbarque 1
--=============================================
CREATE PROCEDURE [dbo].[Embarque_ObtenerPorFolioEmbarque]
(
	@FolioEmbarque INT
)
AS
BEGIN
	SET NOCOUNT ON;
	declare @EmbarqueID INT
	declare @Embarque as table(
		EmbarqueID INT,
		FolioEmbarque INT,
		OrganizacionID INT,
		Organizacion VARCHAR(50),
		TipoOrganizacionID INT,
		TipoOrganizacion VARCHAR(50),
		TipoEmbarqueID INT,
		TipoEmbarque VARCHAR(50),
		Estatus  INT)
	insert @Embarque(EmbarqueID,
		FolioEmbarque,
		OrganizacionID,
		Organizacion,
		TipoOrganizacionID,
		TipoOrganizacion,
		TipoEmbarqueID,
		TipoEmbarque,
		Estatus)
	SELECT 
		EmbarqueID,
		FolioEmbarque,
		e.OrganizacionID,
		o.Descripcion as [Organizacion],
		o.TipoOrganizacionID,
		ot.Descripcion as [TipoOrganizacion],
		e.TipoEmbarqueID,
		te.Descripcion as [TipoEmbarque],
		Estatus
	FROM Embarque e
	INNER JOIN TipoEmbarque  te on te.TipoEmbarqueID = e.TipoEmbarqueID
	INNER JOIN Organizacion o on o.OrganizacionID = e.OrganizacionID
	INNER JOIN TipoOrganizacion ot on ot.TipoOrganizacionID = o.TipoOrganizacionID
	WHERE FolioEmbarque = @FolioEmbarque
	SELECT EmbarqueID,
		FolioEmbarque,
		OrganizacionID,
		Organizacion,
		TipoOrganizacionID,
		TipoOrganizacion,
		TipoEmbarqueID,
		TipoEmbarque,
		Estatus
	FROM @Embarque
	SELECT 
		EmbarqueDetalleID,
		pd.EmbarqueID,
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
		pd.Comentarios		
	FROM EmbarqueDetalle pd
	inner join @Embarque e on e.EmbarqueID = pd.EmbarqueDetalleID
	INNER JOIN Proveedor p on p.ProveedorID = pd.ProveedorID
	INNER JOIN Chofer c on c.ChoferID = pd.ChoferID
	INNER JOIN Jaula j on j.JaulaID = pd.JaulaID
	INNER JOIN Camion cm on cm.CamionID = pd.CamionID
	INNER JOIN Organizacion oo on oo.OrganizacionID = pd.OrganizacionOrigenID
	INNER JOIN Organizacion od on od.OrganizacionID = pd.OrganizacionDestinoID
	SET NOCOUNT OFF;
END

GO
