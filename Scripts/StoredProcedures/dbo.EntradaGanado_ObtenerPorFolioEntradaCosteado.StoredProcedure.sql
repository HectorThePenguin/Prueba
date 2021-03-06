USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorFolioEntradaCosteado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerPorFolioEntradaCosteado]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorFolioEntradaCosteado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jorge Luis Velazquez Araujo
-- Create date: 05-12-2013
-- Description:	Obtiene una entrada de ganado por ID 
-- EntradaGanado_ObtenerPorFolioEntradaCosteado 2, 1
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerPorFolioEntradaCosteado] @FolioEntrada INT
	,@OrganizacionID INT
AS 
BEGIN
	SELECT EntradaGanadoID
		,FolioEntrada
		,eg.OrganizacionID
		,o.Descripcion [Organizacion]
		,too.TipoOrganizacionID
		,too.Descripcion [TipoOrganizacion]
		,eg.OrganizacionOrigenID
		,FechaEntrada
		,eg.EmbarqueID
		,FolioOrigen
		,eg.FechaSalida
		,eg.CamionID
		,eg.ChoferID
		,eg.JaulaID
		,CabezasOrigen
		,CabezasRecibidas
		,eg.OperadorID
		,PesoBruto
		,PesoTara
		,EsRuteo
		,Fleje
		,CheckList
		,eg.CorralID
		,co.Codigo [Corral]
		,eg.LoteID
		,lo.Lote
		,Observacion
		,ImpresionTicket
		,Costeado
		,Manejado
		,eg.Activo
		, Op.Nombre + ' ' + Op.ApellidoPaterno + ' ' + Op.ApellidoMaterno AS NombreOperador
		, P.Descripcion AS Proveedor
		, P.ProveedorID
		, EG.Guia
		, EG.Factura
		, EG.Poliza
		, EG.HojaEmbarque
		, EG.ManejoSinEstres
		, EG.CabezasMuertas
		, EG.CertificadoZoosanitario
		, EG.PruebaTB
		, EG.PruebaTR
		, EG.CondicionJaulaID
	FROM EntradaGanado eg
	INNER JOIN Organizacion o ON eg.OrganizacionOrigenID = o.OrganizacionID
	INNER JOIN TipoOrganizacion too ON o.TipoOrganizacionID = too.TipoOrganizacionID
	INNER JOIN Operador Op ON EG.OperadorID = Op.OperadorID
	INNER JOIN Corral co ON eg.CorralID = co.CorralID
	INNER JOIN Lote lo ON eg.LoteID = lo.LoteID
	INNER JOIN Embarque Em
		ON (EG.EmbarqueID = Em.EmbarqueID)
	INNER JOIN EmbarqueDetalle EmDet
		ON (Em.EmbarqueID = EmDet.EmbarqueID
			AND eg.OrganizacionID = EmDet.OrganizacionDestinoID)
	INNER JOIN Proveedor P
		ON (EmDet.ProveedorID = P.ProveedorID)
	WHERE FolioEntrada = @FolioEntrada
		AND eg.OrganizacionID = @OrganizacionID
		AND eg.ImpresionTicket = 1
		AND eg.Activo = 1
		AND EXISTS (
			SELECT ''
			FROM EntradaGanadoCosteo egc
			WHERE egc.EntradaGanadoID = EG.EntradaGanadoID
				AND egc.Activo = 1
			)
	SELECT EC.EntradaCondicionID
		,EC.EntradaGanadoID
		,EC.CondicionID
		,EC.Cabezas
		,EC.Activo
		,C.Descripcion
	FROM EntradaCondicion EC
	INNER JOIN EntradaGanado EG ON EC.EntradaGanadoID = EG.EntradaGanadoID
	INNER JOIN Condicion C ON (EC.CondicionID = C.CondicionID)
	WHERE EG.FolioEntrada = @FolioEntrada
		AND EG.OrganizacionID = @OrganizacionID
		AND EC.Activo = 1
END

GO
