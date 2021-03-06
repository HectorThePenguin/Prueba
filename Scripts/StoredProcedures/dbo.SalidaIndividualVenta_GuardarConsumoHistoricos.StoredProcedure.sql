USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualVenta_GuardarConsumoHistoricos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaIndividualVenta_GuardarConsumoHistoricos]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualVenta_GuardarConsumoHistoricos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Ramses Santos
-- Create date: 2014/02/28
-- Description: 
-- SalidaIndividualVenta_GuardarConsumoHistoricos 20, 1, 3
--=============================================
CREATE PROCEDURE [dbo].[SalidaIndividualVenta_GuardarConsumoHistoricos]
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
	FROM VentaGanadoDetalle
	WHERE VentaGanadoID = @VentaGanadoID

	CREATE TABLE #tAnimalesCosto
	(
		AnimalID BIGINT
	)
	INSERT INTO #tAnimalesCosto
	SELECT AC.AnimalID
	FROM #tAnimales tA
	INNER JOIN AnimalCostoHistorico AC
		ON (tA.AnimalID = AC.AnimalID
			AND AC.Importe > 0 AND CostoID = 1)
	
	-----ANIMAL CONSUMO
	INSERT INTO AnimalConsumoHistorico (AnimalConsumoID, AnimalID, RepartoID, FormulaIDServida, Cantidad, TipoServicioID
										, Fecha, Activo, FechaCreacion, UsuarioCreacionID, FechaModificacion, UsuarioModificacionID)
	SELECT AnimalConsumoID, AC.AnimalID, RepartoID, FormulaIDServida, Cantidad, TipoServicioID
		 , Fecha, Activo, FechaCreacion, UsuarioCreacionID, FechaModificacion, UsuarioModificacionID
	FROM AnimalConsumo AC WITH(INDEX(IDX_AnimalConsumo_AnimalID))
	INNER JOIN #tAnimalesCosto tAc
		ON (AC.AnimalID = tAc.AnimalID)	

	DELETE AC FROM AnimalConsumo AC(NOLOCK)
	INNER JOIN #tAnimalesCosto A
		ON (AC.AnimalID = A.AnimalID)

	DROP TABLE #tAnimales
	DROP TABLE #tAnimalesCosto

	SET NOCOUNT OFF;
END

GO
