USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_ObtenerDatosFolioSacrificioPorFecha]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificioLucero_ObtenerDatosFolioSacrificioPorFecha]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_ObtenerDatosFolioSacrificioPorFecha]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--======================================================
-- Author     : Ramses Santos
-- Create date: 16/08/2014
-- Description: Obtiene los datos de sacrificio del dia.
-- SpName     : LoteSacrificioLucero_ObtenerDatosFolioSacrificioPorFecha '2015-03-10', 5
--======================================================
CREATE PROCEDURE [dbo].[LoteSacrificioLucero_ObtenerDatosFolioSacrificioPorFecha] 
@Fecha				DATE
, @OrganizacionID	INT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @FolioOrdenSacrificio	INT
	DECLARE @Corrales				INT
	DECLARE @Cabezas				INT
	DECLARE @OrdenSacrificio		INT

	SELECT @Corrales = COUNT(*)
	FROM LoteSacrificioLucero(NOLOCK) AS LSL
	INNER JOIN Lote L
		ON (LSL.LoteID = L.LoteID
			AND L.OrganizacionID = @OrganizacionID)
	WHERE CAST(LSL.Fecha AS DATE) = CAST(@Fecha AS DATE)
		AND ISNULL(Serie, '') = ''
		AND ImporteCanal > 0

	SELECT @Cabezas = COUNT(*)
	FROM LoteSacrificioLucero(NOLOCK) AS LS
	INNER JOIN LoteSacrificioLuceroDetalle(NOLOCK) AS LSD 
		ON (LSD.LoteSacrificioID = LS.LoteSacrificioID)
	WHERE CAST(LS.Fecha AS DATE) = CAST(@Fecha AS DATE)
		AND ISNULL(Serie, '') = ''
		AND ImporteCanal > 0

	SELECT	ISNULL(@OrdenSacrificio, 1) AS OrdenSacrificio
		,	ISNULL(@FolioOrdenSacrificio, 1) AS FolioOrdenSacrificio
		,	ISNULL(@Corrales, 0) AS Corrales
		,	ISNULL(@Cabezas, 0) AS Cabezas

	SET NOCOUNT OFF
END

GO
