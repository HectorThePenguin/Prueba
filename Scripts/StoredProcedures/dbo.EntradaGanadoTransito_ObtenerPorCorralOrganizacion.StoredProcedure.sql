USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoTransito_ObtenerPorCorralOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanadoTransito_ObtenerPorCorralOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoTransito_ObtenerPorCorralOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Juli�n Carranza Castro
-- Create date: 25/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : EntradaGanadoTransito_ObtenerPorCorralOrganizacion 'TRCA', 1
--======================================================
CREATE PROCEDURE [dbo].[EntradaGanadoTransito_ObtenerPorCorralOrganizacion]
@Corral				VARCHAR(10)
, @OrganizacionID	INT
AS 
BEGIN
	SET NOCOUNT ON;
	SELECT EGT.EntradaGanadoTransitoID
		,EGT.LoteID
		,EGT.Cabezas
		,EGT.Peso
		,EGT.Activo
		,L.Lote
		,C.Codigo
		,C.CorralID
	INTO #tEntradaGanadoTransito
	FROM EntradaGanadoTransito EGT
	INNER JOIN Lote L ON (EGT.LoteID = L.LoteID
							AND L.OrganizacionID = @OrganizacionID)
	INNER JOIN Corral C ON (L.CorralID = C.CorralID
							AND LTRIM(RTRIM(C.Codigo)) = LTRIM(RTRIM(@Corral)))
	SELECT EntradaGanadoTransitoID
		,LoteID
		,Cabezas
		,Peso
		,Activo
		,Lote
		,Codigo
		,CorralID
	FROM #tEntradaGanadoTransito
	SELECT EGTD.EntradaGanadoTransitoDetalleID
		,  EGTD.EntradaGanadoTransitoID
		,  EGTD.CostoID
		,  EGTD.Importe
	FROM #tEntradaGanadoTransito EGT
	INNER JOIN EntradaGanadoTransitoDetalle EGTD
		ON (EGT.EntradaGanadoTransitoID = EGTD.EntradaGanadoTransitoID)
		AND EGTD.CostoID = 1
	DROP TABLE #tEntradaGanadoTransito
	SET NOCOUNT OFF;
END

GO
