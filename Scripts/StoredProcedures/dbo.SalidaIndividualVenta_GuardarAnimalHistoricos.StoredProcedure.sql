USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualVenta_GuardarAnimalHistoricos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaIndividualVenta_GuardarAnimalHistoricos]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualVenta_GuardarAnimalHistoricos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Ramses Santos
-- Create date: 2014/02/28
-- Description: 
-- SalidaIndividualVenta_GuardarAnimalHistoricos 14, 2, 3
--=============================================
CREATE PROCEDURE [dbo].[SalidaIndividualVenta_GuardarAnimalHistoricos]
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
	SELECT AnimalID
	FROM
	(
		SELECT AC.AnimalID
		FROM #tAnimales tA
		INNER JOIN AnimalCosto AC(NOLOCK)
			ON (tA.AnimalID = AC.AnimalID
				AND AC.Importe > 0 AND CostoID = 1)
		UNION
		SELECT AC.AnimalID
		FROM #tAnimales tA
		INNER JOIN AnimalCostoHistorico AC(NOLOCK)
			ON (tA.AnimalID = AC.AnimalID
				AND AC.Importe > 0 AND CostoID = 1)
	) A

	--Trasladamos la tabla Animal a la historica
	INSERT INTO AnimalHistorico (AnimalId, Arete, AreteMetalico, FechaCompra, TipoGanadoId, CalidadGanadoId, ClasificacionGanadoId, PesoCompra,
		OrganizacionIdEntrada, FolioEntrada, PesoLlegada, Paletas, CausaRechadoId, Venta, Cronico, Activo, 
		FechaCreacion, UsuarioCreacionId, FechaModificacion, UsuarioModificacionId)
	SELECT AM.AnimalId, Arete, AreteMetalico, FechaCompra, TipoGanadoId, CalidadGanadoId, ClasificacionGanadoId, PesoCompra,
		OrganizacionIdEntrada, FolioEntrada, PesoLlegada, Paletas, CausaRechadoId, Venta, Cronico, Activo, 
		FechaCreacion, UsuarioCreacionId, FechaModificacion, UsuarioModificacionId 
	FROM Animal AM(NOLOCK)
	INNER JOIN #tAnimalesCosto tAC
		ON (AM.AnimalID = tAC.AnimalID)

	DELETE A FROM Animal A(NOLOCK) 
	INNER JOIN #tAnimalesCosto AC
		ON (A.AnimalID = AC.AnimalID)

	DROP TABLE #tAnimales
	DROP TABLE #tAnimalesCosto

	SET NOCOUNT OFF;
END

GO
