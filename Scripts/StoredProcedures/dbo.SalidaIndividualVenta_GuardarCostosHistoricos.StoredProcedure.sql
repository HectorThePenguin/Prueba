USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualVenta_GuardarCostosHistoricos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaIndividualVenta_GuardarCostosHistoricos]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualVenta_GuardarCostosHistoricos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Ramses Santos
-- Create date: 2014/02/28
-- Description: 
-- SalidaIndividualVenta_GuardarCostosHistoricos 16, 1, 3
--=============================================
CREATE PROCEDURE [dbo].[SalidaIndividualVenta_GuardarCostosHistoricos]
	@FolioTicket INT,
	@OrganizacionID INT,
	@UsuarioCreacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @VentaGanadoID INT
	SELECT @VentaGanadoID = VG.VentaGanadoID
	FROM VentaGanado VG(NOLOCK) 
	INNER JOIN Lote L(NOLOCK)
		ON (VG.LoteID = L.LoteID)
	WHERE VG.FolioTicket = @FolioTicket
		AND VG.OrganizacionID = @OrganizacionID

	CREATE TABLE #tAnimales
	(
		AnimalID BIGINT
	)
	INSERT INTO #tAnimales
	SELECT AnimalID
	FROM VentaGanadoDetalle(NOLOCK)
	WHERE VentaGanadoID = @VentaGanadoID
	CREATE TABLE #tAnimalesCosto
	(
		AnimalID BIGINT
	)
	INSERT INTO #tAnimalesCosto
	SELECT AC.AnimalID
	FROM #tAnimales tA
	INNER JOIN AnimalCosto AC(NOLOCK)
		ON (tA.AnimalID = AC.AnimalID
			AND AC.Importe > 0 AND CostoID = 1)
	--Trasaladamos la AnimalCosto a la historica
	INSERT INTO AnimalCostoHistorico (AnimalCostoId, AnimalId, FechaCosto, CostoId, TipoReferencia, FolioReferencia, Importe, FechaCreacion, UsuarioCreacionId,
		FechaModificacion, UsuarioModificacionId)
	SELECT AnimalCostoID, AM.AnimalId, FechaCosto, CostoId,AM.TipoReferencia, FolioReferencia, Importe, FechaCreacion, UsuarioCreacionId,
		FechaModificacion, UsuarioModificacionId
	From AnimalCosto AM(NOLOCK)
	INNER JOIN #tAnimalesCosto tAC
		ON (AM.AnimalID = tAC.AnimalID)
	DELETE AC FROM AnimalCosto AC (NOLOCK)
	INNER JOIN #tAnimalesCosto A
		ON (AC.AnimalID = A.AnimalID)
	DROP TABLE #tAnimales
	DROP TABLE #tAnimalesCosto
	SET NOCOUNT OFF;
END

GO
