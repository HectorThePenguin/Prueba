USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InsterfaceSalida_ObtenerPorEmbarqueID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InsterfaceSalida_ObtenerPorEmbarqueID]
GO
/****** Object:  StoredProcedure [dbo].[InsterfaceSalida_ObtenerPorEmbarqueID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2013/12/26
-- Description: 
-- InsterfaceSalida_ObtenerPorEmbarqueID 172
--=============================================
CREATE PROCEDURE [dbo].[InsterfaceSalida_ObtenerPorEmbarqueID]
@EmbarqueID INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @TotalInterfaceSalida INT
	DECLARE @TotalEmbarqueDetalle INT
	CREATE TABLE #ENTRADAGANADO (
		OrganizacionDestinoID INT
		,EntradaGanadoID INT
		,OrganizacionOrigenID INT
		,FolioOrigen INT
		,EmbarqueID INT
		)
	CREATE TABLE #EMBARQUE (
		EmbarqueID INT
		,FolioEmbarque INT
		,TipoEmbarqueID INT
		)
	CREATE TABLE #EMBARQUEDETALLE (
		EmbarqueDetalleID INT
		,EmbarqueID INT
		,OrganizacionOrigenID INT
		,OrganizacionDestinoID INT
		)
	CREATE TABLE #INTERFACESALIDA (
		OrganizacionID INT
		,SalidaID INT
		,OrganizacionDestinoID INT
		,FechaSalida SMALLDATETIME
		,EsRuteo BIT
		,Cabezas INT
		,Activo BIT
		,FechaRegistro DATETIME
		,UsuarioRegistro VARCHAR(50)
		)
	CREATE TABLE #INTERFACESALIDADETALLE (
		OrganizacionID INT
		,SalidaID INT
		,TipoGanadoID INT
		,TipoGanado VARCHAR(50)
		,Cabezas INT
		,PrecioKg DECIMAL(19, 2)
		,Importe DECIMAL(19, 2)
		,FechaRegistro SMALLDATETIME
		,UsuarioRegistro VARCHAR(50)
		)
	CREATE TABLE #INTERFACESALIDANIMAL (
		OrganizacionID INT
		,SalidaID INT
		,Arete VARCHAR(15)
		,FechaCompra SMALLDATETIME
		,PesoCompra DECIMAL
		,TipoGanadoID INT
		,TipoGanado VARCHAR(50)
		,PesoOrigen DECIMAL
		,FechaRegistro SMALLDATETIME
		,UsuarioRegistro VARCHAR(50)
		)
	CREATE TABLE #INTERFACESALIDCOSTO (
		OrganizacionID INT
		,SalidaID INT
		,Arete VARCHAR(15)
		,FechaCompra SMALLDATETIME
		,CostoID INT
		,Costo VARCHAR(50)
		,ClaveContableCosto CHAR(3)
		,RetencionID INT
		,Retencion VARCHAR(50)
		,Importe MONEY
		,FechaRegistro SMALLDATETIME
		,UsuarioRegistro VARCHAR(50)
		)
	INSERT INTO #ENTRADAGANADO
	SELECT OrganizacionID
		,EntradaGanadoID
		,OrganizacionOrigenID
		,FolioOrigen
		,EmbarqueID
	FROM EntradaGanado
	WHERE EmbarqueID = @EmbarqueID	
	INSERT INTO #EMBARQUE
	SELECT EmbarqueID
		,FolioEmbarque
		,TipoEmbarqueID
	FROM Embarque
	WHERE EmbarqueID = @EmbarqueID
	INSERT INTO #EMBARQUEDETALLE
	SELECT EmbarqueDetalleID
		,EmbarqueID
		,OrganizacionOrigenID
		,OrganizacionDestinoID
	FROM EmbarqueDetalle
	WHERE EmbarqueID = @EmbarqueID
	and Activo = 1
	SET @TotalEmbarqueDetalle = (SELECT COUNT(EmbarqueDetalleID) FROM #EMBARQUEDETALLE)
	SET @TotalInterfaceSalida = (SELECT COUNT(SalidaID) 
								 FROM InterfaceSalida isa
								 INNER JOIN #ENTRADAGANADO EG 
									ON (ISA.OrganizacionID = EG.OrganizacionOrigenID AND ISA.SalidaID = EG.FolioOrigen)
								 --INNER JOIN #EMBARQUEDETALLE ED 
									--ON (ISA.OrganizacionDestinoID = EG.OrganizacionDestinoID
									--	AND ISA.OrganizacionID = ED.OrganizacionOrigenID
									--	AND EG.EmbarqueID = ED.EmbarqueID)
								)
	INSERT INTO #INTERFACESALIDA
	SELECT isa.OrganizacionID
		,isa.SalidaID
		,isa.OrganizacionDestinoID
		,isa.FechaSalida
		,isa.EsRuteo
		,isa.Cabezas
		,isa.Activo
		,isa.FechaRegistro
		,isa.UsuarioRegistro
	FROM InterfaceSalida isa
	INNER JOIN #ENTRADAGANADO EG ON (ISA.OrganizacionID = EG.OrganizacionOrigenID AND ISA.SalidaID = EG.FolioOrigen AND @TotalInterfaceSalida = @TotalEmbarqueDetalle)
	--INNER JOIN #EMBARQUEDETALLE ED ON (ISA.OrganizacionDestinoID = EG.OrganizacionDestinoID
	--		AND ISA.OrganizacionID = ED.OrganizacionOrigenID
	--		AND EG.EmbarqueID = ED.EmbarqueID
	--		)
	INSERT INTO #INTERFACESALIDADETALLE
	SELECT isd.OrganizacionID
		,isd.SalidaID
		,isd.TipoGanadoID
		,tg.Descripcion [TipoGanado]
		,isd.Cabezas
		,isd.PrecioKg
		,isd.Importe
		,isd.FechaRegistro
		,isd.UsuarioRegistro
	FROM InterfaceSalidaDetalle isd
	INNER JOIN #INTERFACESALIDA isal ON isd.OrganizacionID = isal.OrganizacionID
		AND isd.SalidaID = isal.SalidaID
	INNER JOIN TipoGanado tg ON tg.TipoGanadoID = isd.TipoGanadoID
	--INNER JOIN #ENTRADAGANADO EG ON (isal.SalidaID = EG.FolioOrigen)
	--INNER JOIN #EMBARQUEDETALLE ED ON (isal.OrganizacionDestinoID = EG.OrganizacionDestinoID
	--		AND isal.OrganizacionID = ED.OrganizacionOrigenID
	--		AND EG.EmbarqueID = ED.EmbarqueID
	--		AND @TotalInterfaceSalida = @TotalEmbarqueDetalle
	--		)
	INSERT INTO #INTERFACESALIDANIMAL
	SELECT isa.OrganizacionID
		,isa.SalidaID
		,isa.Arete
		,isa.FechaCompra
		,isa.PesoCompra
		,isa.TipoGanadoID
		,isd.TipoGanado
		,isa.PesoOrigen
		,isa.FechaRegistro
		,isa.UsuarioRegistro
	FROM InterfaceSalidaAnimal isa
	INNER JOIN #INTERFACESALIDADETALLE isd ON isa.OrganizacionID = isd.OrganizacionID
		AND isa.SalidaID = isd.SalidaID
		AND isa.TipoGanadoID = isd.TipoGanadoID
	INNER JOIN #ENTRADAGANADO EG ON (isa.SalidaID = EG.FolioOrigen
									 AND @TotalInterfaceSalida = @TotalEmbarqueDetalle)
	INSERT INTO #INTERFACESALIDCOSTO
	SELECT isc.OrganizacionID
		,isc.SalidaID
		,isc.Arete
		,isc.FechaCompra
		,co.CostoID
		,co.Descripcion [Costo]
		,co.ClaveContable
		,re.RetencionID
		,re.Descripcion [Retencion]
		,isc.Importe
		,isc.FechaRegistro
		,isc.UsuarioRegistro
	FROM InterfaceSalidaCosto isc
	INNER JOIN Costo co ON isc.CostoID = co.CostoID
	LEFT JOIN Retencion re ON co.RetencionID = re.RetencionID
	INNER JOIN #INTERFACESALIDANIMAL isa ON isa.OrganizacionID = isc.OrganizacionID
		AND isa.SalidaID = isc.SalidaID
		AND isa.Arete = isc.Arete
		AND isa.FechaCompra = isc.FechaCompra
	INNER JOIN #ENTRADAGANADO EG ON (isa.SalidaID = EG.FolioOrigen
									 AND @TotalInterfaceSalida = @TotalEmbarqueDetalle)
	SELECT OrganizacionID
		,SalidaID
		,OrganizacionDestinoID
		,FechaSalida
		,EsRuteo
		,Cabezas
		,Activo
		,FechaRegistro
		,UsuarioRegistro
	FROM #INTERFACESALIDA
	SELECT OrganizacionID
		,SalidaID
		,TipoGanadoID
		,TipoGanado
		,Cabezas
		,PrecioKg
		,Importe
		,FechaRegistro
		,UsuarioRegistro
	FROM #INTERFACESALIDADETALLE
	SELECT OrganizacionID
		,SalidaID
		,Arete
		,FechaCompra
		,PesoCompra
		,TipoGanadoID
		,TipoGanado
		,PesoOrigen
		,FechaRegistro
		,UsuarioRegistro
	FROM #INTERFACESALIDANIMAL
	SELECT OrganizacionID
		,SalidaID
		,Arete
		,FechaCompra
		,CostoID
		,Costo
		,ClaveContableCosto
		,RetencionID
		,Retencion
		,Importe
		,FechaRegistro
		,UsuarioRegistro
	FROM #INTERFACESALIDCOSTO
	SET NOCOUNT OFF;
END

GO
