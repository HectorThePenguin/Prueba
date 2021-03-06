USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[MuertesEnTransito_EntradaGanado_ObtenerPorFolioEntrada]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[MuertesEnTransito_EntradaGanado_ObtenerPorFolioEntrada]
GO
/****** Object:  StoredProcedure [dbo].[MuertesEnTransito_EntradaGanado_ObtenerPorFolioEntrada]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Alejandro Quiroz
-- Create date: 27/11/2014
-- Description: 
-- SpName     : MuertesEnTransito_EntradaGanado_ObtenerPorFolioEntrada 4,1
-- 001 Jorge Luis Velazquez Araujo 31/03/2016 **Se agrega para que regrese el EmbarqueID
--======================================================
CREATE PROCEDURE [dbo].[MuertesEnTransito_EntradaGanado_ObtenerPorFolioEntrada]
@FolioEntrada AS Int,
@OrganizacionID AS Int
AS
BEGIN
SET NOCOUNT ON;
SELECT
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
	COUNT(EGM.EntradaGanadoMuerteID) AS MuertesRegistradas,
	SUM(EC.Cabezas) AS MuertesEnTransito,
	L.LoteID,
	T.TipoOrganizacionID,
	L.Cabezas,
	EG.EmbarqueID --001
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
	AND CO.Activo = 1
LEFT JOIN EntradaGanadoMuerte(NOLOCK) EGM
	ON EG.EntradaGanadoID = EGM.EntradaGanadoID
WHERE EG.FolioEntrada = @FolioEntrada
AND EG.Activo = 1
AND EG.OrganizacionID = @OrganizacionID
AND C.CondicionID NOT IN (1, 5)
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
SET NOCOUNT OFF;
END

GO