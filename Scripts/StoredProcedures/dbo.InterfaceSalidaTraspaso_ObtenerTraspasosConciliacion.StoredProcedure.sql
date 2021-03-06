USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspaso_ObtenerTraspasosConciliacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalidaTraspaso_ObtenerTraspasosConciliacion]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspaso_ObtenerTraspasosConciliacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 30/03/2015
-- Description: Obtiene los datos del traspaso de ganado
-- [InterfaceSalidaTraspaso_ObtenerTraspasosConciliacion] 5, '20150301', '20150330'
-- --=============================================
CREATE PROCEDURE [dbo].[InterfaceSalidaTraspaso_ObtenerTraspasosConciliacion]
@OrganizacionID	INT
, @FechaInicial	DATE
, @FechaFinal	DATE
AS
BEGIN

	SET NOCOUNT ON;

		DECLARE @FechaInicio DATE, @FechaFin DATE

		SELECT @FechaInicio = @FechaInicial, @FechaFin = @FechaFinal

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
		WHERE IST.PesoBruto > 0
			AND IST.PesoTara > 0
			AND IST.OrganizacionID = @OrganizacionID
			AND CAST(FechaEnvio AS DATE) BETWEEN @FechaInicio AND @FechaFin


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
			FROM #tCabecero tC
			INNER JOIN InterfaceSalidaTraspasoDetalle ISD
				ON (tC.InterfaceSalidaTraspasoID = ISD.InterfaceSalidaTraspasoID)

		SELECT DISTINCT InterfaceSalidaTraspasoID
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

		SELECT DISTINCT tD.InterfaceSalidaTraspasoDetalleID
			,  tD.InterfaceSalidaTraspasoID
			,  tD.LoteID
			,  tD.TipoGanadoID
			,  tD.PesoProyectado
			,  tD.GananciaDiaria
			,  tD.DiasEngorda
			,  tD.FormulaID
			,  tD.DiasFormula
			,  tD.Cabezas
			,  0			AS CorralID
			,  OrganizacionIDDestino
			,  TraspasoGanado
			,  SacrificioGanado
		FROM #tDetalle tD
		INNER JOIN #tCabecero tC
			ON (tD.InterfaceSalidaTraspasoID = tC.InterfaceSalidaTraspasoID)

		DROP TABLE #tDetalle
		DROP TABLE #tCabecero

	SET NOCOUNT OFF;
END

GO
