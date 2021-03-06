USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspasoLucero_ObtenerPorLoteXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalidaTraspasoLucero_ObtenerPorLoteXML]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspasoLucero_ObtenerPorLoteXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--=============================================
-- Author     : Gilberto Carranza
-- Create date: 09/03/2015
-- Description: Obtiene los datos del traspaso de ganado
-- --=============================================
CREATE PROCEDURE [dbo].[InterfaceSalidaTraspasoLucero_ObtenerPorLoteXML]
@LotesXML	XML
AS
BEGIN

	SET NOCOUNT ON;

		CREATE TABLE #tLotes
		(
			LoteID INT
		)

		CREATE TABLE #tCabecero
		(
			InterfaceSalidaTraspasoID	INT
			, OrganizacionID			INT
			, OrganizacionIDDestino		INT
			, FolioTraspaso				INT
			, FechaEnvio				SMALLDATETIME
			, CabezasEnvio				INT
			, SacrificioGanado			BIT
			, TraspasoGanado			BIT
			, PesoTara					INT
			, PesoBruto					INT
		)

		CREATE TABLE #tDetalle
		(
			InterfaceSalidaTraspasoDetalleID	INT
			, InterfaceSalidaTraspasoID			INT
			, LoteID							INT
			, TipoGanadoID						INT
			, PesoProyectado					INT
			, GananciaDiaria					DECIMAL(5,3)
			, DiasEngorda						INT
			, FormulaID							INT
			, DiasFormula						INT
			, Cabezas							INT
		)

		INSERT INTO #tLotes
		SELECT LoteID
		FROM 
		(
			SELECT
				t.item.value('./LoteID[1]', 'INT') AS LoteID
			FROM @LotesXML.nodes('ROOT/Lotes') AS T (item)
		) x

		DECLARE @GanaderasEnvianSacrificio VARCHAR(100)
		SET @GanaderasEnvianSacrificio = (SELECT Valor
										  FROM ParametroGeneral PG 
										  INNER JOIN Parametro P 
											ON (PG.ParametroID = P.ParametroID 
												AND P.Clave = 'GANADERATRASPASAGANADO')
										  )
		CREATE TABLE #tGanaderas
		(
			OrganizacionID INT
		)

		INSERT INTO #tGanaderas
		SELECT Registros
		FROM dbo.FuncionSplit(@GanaderasEnvianSacrificio, '|')

		DECLARE @FolioTraspaso INT
		DECLARE @OrganizacionID  INT

		SELECT @FolioTraspaso	= FolioOrigen
			,  @OrganizacionID  = OrganizacionOrigenID
		FROM EntradaGanado EG
		INNER JOIN #tLotes tL
			ON (EG.LoteID = tl.LoteID)

		--IF (@FolioTraspaso > 0)
		--BEGIN

			INSERT INTO #tDetalle
			SELECT ISD.InterfaceSalidaTraspasoDetalleID
				,  ISD.InterfaceSalidaTraspasoID
				,  ISD.LoteID
				,  ISD.TipoGanadoID
				,  ISD.PesoProyectado
				,  ISD.GananciaDiaria
				,  ISD.DiasEngorda
				,  ISD.FormulaID
				,  ISD.DiasFormula
				,  ISD.Cabezas
			FROM #tLotes tC
			INNER JOIN InterfaceSalidaTraspasoDetalle ISD
				ON (tC.LoteID = ISD.LoteID)

			INSERT INTO #tCabecero
			SELECT 	IST.InterfaceSalidaTraspasoID
					, IST.OrganizacionID
					, IST.OrganizacionIDDestino
					, IST.FolioTraspaso
					, IST.FechaEnvio
					, IST.CabezasEnvio
					, IST.SacrificioGanado
					, IST.TraspasoGanado
					, IST.PesoTara
					, IST.PesoBruto
			FROM InterfaceSalidaTraspaso IST
			INNER JOIN #tDetalle tD
				ON (IST.InterfaceSalidaTraspasoID = tD.InterfaceSalidaTraspasoID)
			
		--END

		SELECT InterfaceSalidaTraspasoID
			, OrganizacionID
			, OrganizacionIDDestino
			, FolioTraspaso
			, FechaEnvio
			, CabezasEnvio
			, SacrificioGanado
			, TraspasoGanado
			, PesoTara
			, PesoBruto
		FROM #tCabecero

		SELECT InterfaceSalidaTraspasoDetalleID
			,  InterfaceSalidaTraspasoID
			,  LoteID
			,  TipoGanadoID
			,  PesoProyectado
			,  GananciaDiaria
			,  DiasEngorda
			,  FormulaID
			,  DiasFormula
			,  Cabezas
			,  0			AS CorralID
		FROM #tDetalle

		DROP TABLE #tLotes
		DROP TABLE #tDetalle
		DROP TABLE #tCabecero
		DROP TABLE #tGanaderas		

	SET NOCOUNT OFF;
END

GO
