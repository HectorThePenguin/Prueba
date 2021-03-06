USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[MuertesEnTransito_EntradaGanado_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[MuertesEnTransito_EntradaGanado_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[MuertesEnTransito_EntradaGanado_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Alejandro Quiroz
-- Create date: 27/11/2014
-- Description: 
-- SpName     : MuertesEnTransito_EntradaGanado_ObtenerPorPagina 0,5,1,15
-- 001 Jorge Luis Velazquez Araujo 31/03/2016 **Se agrega para que regrese el EmbarqueID
--======================================================
CREATE PROCEDURE [dbo].[MuertesEnTransito_EntradaGanado_ObtenerPorPagina]
@FolioEntrada AS Varchar(15),
@OrganizacionID AS Int,
@Inicio AS Int,
@Limite AS Int
AS
BEGIN
SET NOCOUNT ON;
SELECT
	ROW_NUMBER() OVER (ORDER BY EG.FolioEntrada ASC) AS [RowNum],
	EG.EntradaGanadoID,
	EG.FolioEntrada,
	O.Descripcion AS Origen,
	T.Descripcion AS TipoOrigen,
	P.CodigoSAP,
	P.Descripcion AS Proveedor,
	CO.CorralID,
	CO.Codigo AS Corral,
	L.Lote,
	EG.CabezasRecibidas,
	ISNULL(COUNT(EGM.EntradaGanadoMuerteID), 0) AS MuertesRegistradas,
	SUM(EC.Cabezas) AS MuertesEnTransito,
	L.LoteID,
	T.TipoOrganizacionID,
	L.Cabezas ,
	EG.EmbarqueID--001
	INTO #MuertesEnTransito
FROM EntradaGanado(NOLOCK) EG
INNER JOIN EntradaCondicion(NOLOCK) EC
	ON EG.EntradaGanadoID = EC.EntradaGanadoID
	AND EC.Activo = 1
INNER JOIN Condicion(NOLOCK) C
	ON C.CondicionID = EC.CondicionID
	AND C.Activo = 1
INNER JOIN Organizacion(NOLOCK) O
	ON O.OrganizacionID = EG.OrganizacionOrigenID
	AND O.Activo = 1
INNER JOIN TipoOrganizacion(NOLOCK) T
	ON O.TipoOrganizacionID = T.TipoOrganizacionID
	AND T.Activo = 1
INNER JOIN EmbarqueDetalle(NOLOCK) ED
	ON EG.EmbarqueID = ED.EmbarqueID
	AND ED.OrganizacionDestinoID = EG.OrganizacionID
	AND ED.Activo = 1
INNER JOIN Proveedor(NOLOCK) P
	ON ED.ProveedorID = P.ProveedorID
	AND P.Activo = 1
INNER JOIN Lote(NOLOCK) L
	ON L.LoteID = EG.LoteID
INNER JOIN Corral(NOLOCK) CO
	ON L.CorralID = CO.CorralID
	AND CO.Activo = 1 AND L.Activo = 1
LEFT JOIN EntradaGanadoMuerte(NOLOCK) EGM
	ON EG.EntradaGanadoID = EGM.EntradaGanadoID
WHERE EG.Activo = 1
AND EG.OrganizacionID = @OrganizacionID
AND @FolioEntrada IN (EG.FolioEntrada, 0)
AND C.CondicionID NOT IN (1, 5)
AND L.Cabezas > 0 AND L.Activo = 1
GROUP BY	EG.EntradaGanadoID,
			EG.FolioEntrada,
			O.Descripcion,
			T.Descripcion,
			CO.Codigo,
			L.Lote,
			EG.CabezasRecibidas,
			P.CodigoSAP,
			P.Descripcion,
			CO.CorralID,
			L.LoteID,
			T.TipoOrganizacionID,
			L.Cabezas,
			EG.EmbarqueID--001
HAVING COUNT(EGM.EntradaGanadoMuerteID) = 0

SELECT
	MT.EntradaGanadoID,
	MT.FolioEntrada,
	MT.Origen,
	MT.TipoOrigen,
	MT.CodigoSAP,
	MT.Proveedor,
	MT.CorralID,
	MT.Corral,
	MT.Lote,
	MT.CabezasRecibidas,
	MT.MuertesRegistradas,
	MT.MuertesEnTransito,
	MT.LoteID,
	MT.TipoOrganizacionID,
	MT.Cabezas,
	MT.EmbarqueID--001
FROM #MuertesEnTransito mt
WHERE RowNum BETWEEN @Inicio AND @Limite

SELECT
	COUNT(EntradaGanadoID) AS [TotalReg]
FROM #MuertesEnTransito

DROP TABLE #MuertesEnTransito

SET NOCOUNT OFF;
END

GO