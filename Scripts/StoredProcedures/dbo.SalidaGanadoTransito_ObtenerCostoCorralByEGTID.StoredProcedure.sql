DROP PROCEDURE [dbo].[SalidaGanadoTransito_ObtenerCostoCorralByEGTID]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanadoTransito_ObtenerCostoCorralByEGTID]    Script Date: 11/04/2016 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Torres Lugo Manuel
-- Create date: 11/04/2016 
-- Description: Filtro obtener el costoCorral por EntraGanadoTransitoID
-- SpName     : SalidaGanadoTransito_ObtenerCostoCorralByEGTID
--======================================================
CREATE PROCEDURE dbo.SalidaGanadoTransito_ObtenerCostoCorralByEGTID
@EntradaGanadoTransitoID INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		EGTD.CostoID, C.Descripcion, EGTD.Importe 
	FROM EntradaGanadoTransitoDetalle AS EGTD 
	INNER JOIN Costo AS C ON C.CostoID = EGTD.CostoID
	WHERE EGTD.EntradaGanadoTransitoID = @EntradaGanadoTransitoID
	AND EGTD.Activo = 1
	AND C.Activo = 1

	SET NOCOUNT OFF;
END