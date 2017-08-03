USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanadoTransito_CorralesSalidaByOrganizacionID]    Script Date: 11/04/2016 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[SalidaGanadoTransito_CorralesSalidaByOrganizacionID]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanadoTransito_CorralesSalidaByOrganizacionID]    Script Date: 11/04/2016 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Torres Lugo Manuel
-- Create date: 11/04/2016 
-- Description: Filtro para traer los campos de TRCA y TRCD
-- SpName     : SalidaGanadoTransito_CorralesSalidaByOrganizacionID
--======================================================
CREATE PROCEDURE dbo.SalidaGanadoTransito_CorralesSalidaByOrganizacionID
@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		P.ParametroID,
		PO.OrganizacionID,
		EGT.EntradaGanadoTransitoID,
		P.Descripcion,
		PO.Valor,
		C.CorralID,
		L.LoteID,
		L.Cabezas,
		CASE WHEN EGT.Cabezas <= 0 THEN 0 ELSE (EGT.Peso / EGT.Cabezas) END AS Peso_Promedio,
		L.Activo
	FROM Parametro AS P
	INNER JOIN ParametroOrganizacion AS PO ON PO.ParametroID = P.ParametroID 
	INNER JOIN Corral AS C ON C.Codigo = PO.Valor 
	INNER JOIN Lote AS L ON L.CorralID = C.CorralID
	INNER JOIN EntradaGanadoTransito AS EGT ON EGT.LoteID = L.LoteID
	WHERE P.Clave  IN ('CORRALFALTPROPIO','CORRALFALTDIRECTA')
	AND PO.Activo = 1
	AND P.Activo = 1
	AND PO.OrganizacionID = @OrganizacionID
	AND C.OrganizacionID = @OrganizacionID
	AND L.OrganizacionID = @OrganizacionID
  AND EGT.Cabezas>0
	SET NOCOUNT OFF;
END