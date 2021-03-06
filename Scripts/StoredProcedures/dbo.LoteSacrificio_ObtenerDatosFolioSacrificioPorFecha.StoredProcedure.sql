USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificio_ObtenerDatosFolioSacrificioPorFecha]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificio_ObtenerDatosFolioSacrificioPorFecha]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificio_ObtenerDatosFolioSacrificioPorFecha]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 16/08/2014
-- Description: Obtiene los datos de sacrificio del dia.
-- SpName     : LoteSacrificio_ObtenerDatosFolioSacrificioPorFecha '2015-02-12', 2
--======================================================
CREATE PROCEDURE [dbo].[LoteSacrificio_ObtenerDatosFolioSacrificioPorFecha] @Fecha DATE
	,@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @OrdenSacrificio INT
	DECLARE @FolioOrdenSacrificio INT
	DECLARE @Corrales INT
	DECLARE @Cabezas INT
	SELECT @FolioOrdenSacrificio = OS.FolioOrdenSacrificio
		,@OrdenSacrificio = OS.OrdenSacrificioID
	FROM OrdenSacrificio(NOLOCK) AS OS
	INNER JOIN LoteSacrificio(NOLOCK) AS LS ON (OS.OrdenSacrificioID = LS.OrdenSacrificioID)
	WHERE CAST(LS.Fecha AS DATE) = CAST(@Fecha AS DATE)
		AND OS.OrganizacionID = @OrganizacionID
		AND ISNULL(LS.Folio, 0) = 0
		AND ISNULL(Serie, '') = ''
	SELECT @Corrales = COUNT(*)
	FROM LoteSacrificio(NOLOCK) AS LS
	WHERE CAST(LS.Fecha AS DATE) = CAST(@Fecha AS DATE)
		AND OrdenSacrificioID = @OrdenSacrificio
	SELECT @Cabezas = COUNT(*)
	FROM LoteSacrificio(NOLOCK) AS LS
	INNER JOIN LoteSacrificioDetalle(NOLOCK) AS LSD ON (LSD.LoteSacrificioID = LS.LoteSacrificioID)
	WHERE CAST(LS.Fecha AS DATE) = CAST(@Fecha AS DATE)
		AND LS.OrdenSacrificioID = @OrdenSacrificio
	SELECT ISNULL(@OrdenSacrificio, 0) AS OrdenSacrificio
		,ISNULL(@FolioOrdenSacrificio, 0) AS FolioOrdenSacrificio
		,ISNULL(@Corrales, 0) AS Corrales
		,ISNULL(@Cabezas, 0) AS Cabezas
	SET NOCOUNT OFF
END

GO
