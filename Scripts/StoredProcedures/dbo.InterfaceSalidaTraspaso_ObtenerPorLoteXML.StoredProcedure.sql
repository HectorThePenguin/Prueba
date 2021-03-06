USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspaso_ObtenerPorLoteXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalidaTraspaso_ObtenerPorLoteXML]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspaso_ObtenerPorLoteXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--=============================================
-- Author     : Gilberto Carranza
-- Create date: 09/03/2015
-- Description: Obtiene los datos del traspaso de ganado
-- --=============================================
CREATE PROCEDURE [dbo].[InterfaceSalidaTraspaso_ObtenerPorLoteXML] @LotesXML XML
	,@Fecha DATE
	,@XmlInterface XML
AS
BEGIN
	SET NOCOUNT ON;

	CREATE TABLE #tLotes (LoteID INT)

	CREATE TABLE #tInterface (InterfaceDetalleID BIGINT)

	CREATE TABLE #tCabecero (
		InterfaceSalidaTraspasoID INT
		,OrganizacionID INT
		,OrganizacionIDDestino INT
		,FolioTraspaso INT
		,FechaEnvio SMALLDATETIME
		,CabezasEnvio INT
		,SacrificioGanado BIT
		,TraspasoGanado BIT
		,PesoTara INT
		,PesoBruto INT
		)

	CREATE TABLE #tDetalle (
		InterfaceSalidaTraspasoDetalleID INT
		,InterfaceSalidaTraspasoID INT
		,LoteID INT
		,TipoGanadoID INT
		,PesoProyectado INT
		,GananciaDiaria DECIMAL(5, 3)
		,DiasEngorda INT
		,FormulaID INT
		,DiasFormula INT
		,Cabezas INT
		)

	INSERT INTO #tLotes
	SELECT LoteID
	FROM (
		SELECT t.item.value('./LoteID[1]', 'INT') AS LoteID
		FROM @LotesXML.nodes('ROOT/Lotes') AS T(item)
		) x

	INSERT INTO #tInterface
	SELECT InterfaceDetalleID
	FROM (
		SELECT t.item.value('./Id[1]', 'BIGINT') AS InterfaceDetalleID
		FROM @XmlInterface.nodes('ROOT/InterfaceDetalleID') AS T(item)
		) x

	CREATE TABLE #tLoteSacrificio
	(
		InterfaceSalidaTraspasoDetalleID	INT
		, Cabezas							INT
	)
	INSERT INTO #tLoteSacrificio
	(
		InterfaceSalidaTraspasoDetalleID
		, Cabezas
	)
	SELECT LSL.InterfaceSalidaTraspasoDetalleID
		,  COUNT(LSLD.AnimalID)
	FROM #tInterface I
	INNER JOIN LoteSacrificioLucero LSL
		ON (I.InterfaceDetalleID = LSL.InterfaceSalidaTraspasoDetalleID
			AND LSL.PolizaGenerada = 0)
	INNER JOIN LoteSacrificioLuceroDetalle LSLD
		ON (LSL.LoteSacrificioID = LSLD.LoteSacrificioID)
	GROUP BY LSL.InterfaceSalidaTraspasoDetalleID

	INSERT INTO #tDetalle
	SELECT ISD.InterfaceSalidaTraspasoDetalleID
		,ISD.InterfaceSalidaTraspasoID
		,ISD.LoteID
		,ISD.TipoGanadoID
		,ISD.PesoProyectado
		,ISD.GananciaDiaria
		,ISD.DiasEngorda
		,ISD.FormulaID
		,ISD.DiasFormula
		,ISD.Cabezas
	FROM #tLotes tC
	INNER JOIN InterfaceSalidaTraspasoDetalle ISD(NOLOCK) 
		ON (tC.LoteID = ISD.LoteID)
	INNER JOIN #tInterface x 
		ON (ISD.InterfaceSalidaTraspasoDetalleID = x.InterfaceDetalleID)

	INSERT INTO #tCabecero
	SELECT IST.InterfaceSalidaTraspasoID
		,IST.OrganizacionID
		,IST.OrganizacionIDDestino
		,IST.FolioTraspaso
		,IST.FechaEnvio
		,IST.CabezasEnvio
		,IST.SacrificioGanado
		,IST.TraspasoGanado
		,IST.PesoTara
		,IST.PesoBruto
	FROM #tDetalle tD 
	INNER JOIN InterfaceSalidaTraspaso IST(NOLOCK)
		ON (tD.InterfaceSalidaTraspasoID = IST.InterfaceSalidaTraspasoID)

	SELECT DISTINCT InterfaceSalidaTraspasoID
		,OrganizacionID
		,OrganizacionIDDestino
		,FolioTraspaso
		,FechaEnvio
		,CabezasEnvio
		,SacrificioGanado
		,TraspasoGanado
		,PesoTara
		,PesoBruto
	FROM #tCabecero

	SELECT DISTINCT tD.InterfaceSalidaTraspasoDetalleID
		,tD.InterfaceSalidaTraspasoID
		,tD.LoteID
		,tD.TipoGanadoID
		,tD.PesoProyectado
		,tD.GananciaDiaria
		,tD.DiasEngorda
		,tD.FormulaID
		,tD.DiasFormula
		,tD.Cabezas
		,0 AS CorralID
		,OrganizacionIDDestino
		,TraspasoGanado
		,SacrificioGanado
	FROM #tDetalle tD
	INNER JOIN #tCabecero tC 
		ON (tD.InterfaceSalidaTraspasoID = tC.InterfaceSalidaTraspasoID)

	DROP TABLE #tLotes
	DROP TABLE #tDetalle
	DROP TABLE #tCabecero
	DROP TABLE #tInterface

	SET NOCOUNT OFF;
END

GO
