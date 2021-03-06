USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspaso_ObtenerPorFolioTraspasoCosteo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalidaTraspaso_ObtenerPorFolioTraspasoCosteo]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspaso_ObtenerPorFolioTraspasoCosteo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2013/11/25
-- Description: Obtiene una Salida por ID y Organizacion
-- InterfaceSalidaTraspaso_ObtenerPorFolioTraspasoCosteo 22,1
--=============================================
CREATE PROCEDURE [dbo].[InterfaceSalidaTraspaso_ObtenerPorFolioTraspasoCosteo]
@FolioTraspaso INT
, @OrganizacionID INT
AS
BEGIN

	SET NOCOUNT ON

		CREATE TABLE #Cabecero
		(
			InterfaceSalidaTraspasoID	INT
			, OrganizacionID			INT
			, OrganizacionIDDestino		INT
			, Organizacion				VARCHAR(100)
			, FolioTraspaso				INT
			, FechaEnvio				SMALLDATETIME
			, CabezasEnvio				INT
			, SacrificioGanado			BIT
			, TraspasoGanado			BIT
			, PesoTara					INT
			, PesoBruto					INT
		)

		INSERT INTO #Cabecero
		SELECT InSal.InterfaceSalidaTraspasoID
			, InSal.OrganizacionID
			, InSal.OrganizacionIDDestino
			, O.Descripcion					AS Organizacion
			, InSal.FolioTraspaso
			, InSal.FechaEnvio
			, InSal.CabezasEnvio
			, InSal.SacrificioGanado
			, InSal.TraspasoGanado
			, InSal.PesoTara
			, InSal.PesoBruto
		FROM InterfaceSalidaTraspaso InSal
		INNER JOIN Organizacion O
			ON (InSal.OrganizacionIDDestino = O.OrganizacionID)
		WHERE InSal.FolioTraspaso = @FolioTraspaso
			AND InSal.OrganizacionIDDestino = @OrganizacionID
		
		SELECT InterfaceSalidaTraspasoID
				, OrganizacionID
				, OrganizacionIDDestino
				, Organizacion
				, FolioTraspaso
				, FechaEnvio
				, CabezasEnvio
				, SacrificioGanado
				, TraspasoGanado
				, PesoTara
				, PesoBruto
		FROM #Cabecero

		SELECT InSalDet.InterfaceSalidaTraspasoDetalleID
				, InSalDet.InterfaceSalidaTraspasoID
				, InSalDet.LoteID
				, InSalDet.TipoGanadoID
				, InSalDet.PesoProyectado
				, InSalDet.GananciaDiaria
				, InSalDet.DiasEngorda
				, InSalDet.FormulaID
				, InSalDet.DiasFormula
				, InSalDet.Cabezas
		FROM #Cabecero InSal
		INNER JOIN InterfaceSalidaTraspasoDetalle InSalDet
			ON (InSal.InterfaceSalidaTraspasoID = InSalDet.InterfaceSalidaTraspasoID)

		DROP TABLE #Cabecero

	SET NOCOUNT OFF

END

GO
