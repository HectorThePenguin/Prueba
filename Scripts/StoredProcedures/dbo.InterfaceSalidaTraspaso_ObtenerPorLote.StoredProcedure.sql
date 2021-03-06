USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspaso_ObtenerPorLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalidaTraspaso_ObtenerPorLote]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspaso_ObtenerPorLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--=============================================
-- Author     : Gilberto Carranza
-- Create date: 09/03/2015
-- Description: Obtiene los datos del traspaso de ganado
-- --=============================================
CREATE PROCEDURE [dbo].[InterfaceSalidaTraspaso_ObtenerPorLote] 
	@LoteID	INT
	,@InterfaceDetalleID BIGINT
AS
BEGIN
	SET NOCOUNT ON;

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
	FROM LoteSacrificioLucero LSL
	INNER JOIN LoteSacrificioLuceroDetalle LSLD
		ON (LSL.LoteSacrificioID = LSLD.LoteSacrificioID)
	WHERE LSL.InterfaceSalidaTraspasoDetalleID = @InterfaceDetalleID
		AND LSL.PolizaGenerada = 0
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
	FROM InterfaceSalidaTraspasoDetalle ISD(NOLOCK) 
	WHERE ISD.LoteID = @LoteID
		AND ISD.InterfaceSalidaTraspasoDetalleID = @InterfaceDetalleID

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

	DROP TABLE #tDetalle
	DROP TABLE #tCabecero

	SET NOCOUNT OFF;
END

GO
