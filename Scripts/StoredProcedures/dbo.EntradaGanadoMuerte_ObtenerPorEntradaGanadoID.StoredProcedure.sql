USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoMuerte_ObtenerPorEntradaGanadoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanadoMuerte_ObtenerPorEntradaGanadoID]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoMuerte_ObtenerPorEntradaGanadoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:    César Valdez Figueroa
-- Create date: 11/12/2014
-- Description:  Obtener Entradas de ganado muerto en transito
-- Origen: APInterfaces
-- EntradaGanadoMuerte_ObtenerPorEntradaGanadoID 2
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanadoMuerte_ObtenerPorEntradaGanadoID] @EntradaGanadoID INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT EntradaGanadoMuerteID
		,EntradaGanadoID
		,Arete
		,FolioMuerte
		,Fecha
		,Activo
		,UsuarioCreacionID
		,Peso
	INTO #tCabecero
	FROM EntradaGanadoMuerte
	WHERE EntradaGanadoID = @EntradaGanadoID

	SELECT EntradaGanadoMuerteID
		,EntradaGanadoID
		,Arete
		,FolioMuerte
		,Fecha
		,Activo
		,UsuarioCreacionID
		,Peso
	FROM #tCabecero

	SELECT EntradaGanadoMuerteDetalleID
		,EGMD.EntradaGanadoMuerteID
		,CostoID
		,Importe
	FROM EntradaGanadoMuerteDetalle EGMD
	INNER JOIN #tCabecero tC ON (EGMD.EntradaGanadoMuerteID = tC.EntradaGanadoMuerteID)

	DROP TABLE #tCabecero

	SET NOCOUNT OFF
END

GO
