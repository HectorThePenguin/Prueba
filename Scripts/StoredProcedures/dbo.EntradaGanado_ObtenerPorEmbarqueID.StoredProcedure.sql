USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorEmbarqueID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerPorEmbarqueID]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorEmbarqueID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Gilberto Carranza
-- Create date: 13/11/2013
-- Description:	Obtiene una Entrada de Ganado Por Programacion de Embarque.
-- EntradaGanado_ObtenerPorEmbarqueID 621,0
--=============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerPorEmbarqueID]
 @EmbarqueID INT,
 @OrganizacionOrigenID INT = 0
AS 
BEGIN
	SET NOCOUNT ON
	DECLARE @EntradaGanadoID INT
	CREATE TABLE #EntradaGanado
	(
		EntradaGanadoID INT,
		FolioEntrada INT ,
		OrganizacionID INT ,
		OrganizacionOrigenID INT ,
		FechaEntrada SMALLDATETIME ,
		EmbarqueID INT ,
		FolioOrigen INT ,
		FechaSalida SMALLDATETIME,
		CamionID INT,
		ChoferID INT ,
		JaulaID INT ,
		CabezasOrigen INT ,
		CabezasRecibidas INT ,
		OperadorID INT ,
		PesoBruto DECIMAL(18, 2) ,
		PesoTara DECIMAL(18, 2) ,
		EsRuteo BIT ,
		Fleje BIT ,
		CheckList CHAR(10) ,
		CorralID INT ,
		LoteID INT ,
		Observacion VARCHAR(225) ,
		ImpresionTicket BIT ,
		Costeado BIT ,
		Manejado BIT ,
		Activo BIT,
		Guia BIT,
		Factura BIT,
		Poliza BIT,
		HojaEmbarque BIT,
		ManejoSinEstres BIT
		, CertificadoZoosanitario VARCHAR(15)
		, PruebaTB	VARCHAR(15)
		, PruebaTR	VARCHAR(15)
		, CondicionJaulaID	INT
	)
		INSERT INTO #EntradaGanado
		SELECT 
			EntradaGanadoID,
			FolioEntrada,
			OrganizacionID,
			OrganizacionOrigenID,
			FechaEntrada,
			EmbarqueID,
			FolioOrigen,
			FechaSalida,
			CamionID,
			ChoferID,
			JaulaID,
			CabezasOrigen,
			CabezasRecibidas,
			OperadorID,
			PesoBruto,
			PesoTara,
			EsRuteo,
			Fleje,
			CheckList,
			CorralID,
			LoteID,
			Observacion,
			ImpresionTicket,
			Costeado,
			Manejado,
			Activo,
			Guia,
			Factura,
			Poliza,
			HojaEmbarque,
			ManejoSinEstres
			, CertificadoZoosanitario
			, PruebaTB
			, PruebaTR
			, CondicionJaulaID
		 FROM EntradaGanado
		 WHERE EmbarqueID = @EmbarqueID
			AND @OrganizacionOrigenID IN (OrganizacionOrigenID, 0)
		 SELECT 
		 	EG.EntradaGanadoID,
			EG.FolioEntrada,
			EG.OrganizacionID,
			EG.OrganizacionOrigenID,
			EG.FechaEntrada,
			EG.EmbarqueID,
			EG.FolioOrigen,
			EG.FechaSalida,
			EG.CamionID,
			EG.ChoferID,
			EG.JaulaID,
			EG.CabezasOrigen,
			EG.CabezasRecibidas,
			EG.OperadorID,
			EG.PesoBruto,
			EG.PesoTara,
			EG.EsRuteo,
			EG.Fleje,
			EG.CheckList,
			ISNULL(EG.CorralID, 0) AS CorralID,
			ISNULL(EG.LoteID, 0) AS LoteID,
			EG.Observacion,
			EG.ImpresionTicket,
			EG.Costeado,
			EG.Manejado,
			EG.Activo,
			EG.Guia,
			EG.Factura,
			EG.Poliza,
			EG.HojaEmbarque,
			EG.ManejoSinEstres
			, EG.CertificadoZoosanitario
			, EG.PruebaTB
			, EG.PruebaTR
			, O.TipoOrganizacionID
			, O.Descripcion	AS Organizacion
			, TOrg.Descripcion AS TipoOrganizacion
			, C.Codigo AS Corral
			, L.Lote
			, EG.CondicionJaulaID
		 FROM #EntradaGanado EG
		 INNER JOIN Organizacion O
			ON (EG.OrganizacionOrigenID = O.OrganizacionID)
		 INNER JOIN TipoOrganizacion TOrg
			ON (O.TipoOrganizacionID = TOrg.TipoOrganizacionID)
		 LEFT JOIN Corral C
			ON (EG.CorralID = C.CorralID)
		 LEFT JOIN Lote L
			ON (EG.LoteID = L.LoteID)
		 SELECT 
			EC.EntradaCondicionID,
			EC.EntradaGanadoID,
			EC.CondicionID,
			EC.Cabezas,
			EC.Activo,
			C.Descripcion
		FROM EntradaCondicion EC
		INNER JOIN #EntradaGanado tEG
			ON (EC.EntradaGanadoID = tEG.EntradaGanadoID)
		INNER JOIN Condicion C
			ON (EC.CondicionID = C.CondicionID)
	DROP TABLE #EntradaGanado
			SELECT CE.CostoID, CE.Importe, E.EmbarqueID 
		FROM Embarque E 
		INNER JOIN EmbarqueDetalle ED ON E.EmbarqueID = ED.EmbarqueID AND E.OrganizacionID = ED.OrganizacionDestinoID
		INNER JOIN CostoEmbarqueDetalle CE ON CE.EmbarqueDetalleID = ED.EmbarqueDetalleID
		WHERE E.EmbarqueID = @EmbarqueID AND E.OrganizacionID = @OrganizacionOrigenID
	SET NOCOUNT OFF
END

GO
