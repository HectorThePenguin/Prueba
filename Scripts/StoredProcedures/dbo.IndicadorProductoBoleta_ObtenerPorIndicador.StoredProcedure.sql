USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoBoleta_ObtenerPorIndicador]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorProductoBoleta_ObtenerPorIndicador]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoBoleta_ObtenerPorIndicador]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Juli�n Carranza Castro
-- Create date: 24/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorProductoBoleta_ObtenerPorIndicador
--======================================================
CREATE PROCEDURE [dbo].[IndicadorProductoBoleta_ObtenerPorIndicador]
@IndicadorID	INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		IPB.IndicadorProductoBoletaID,
		IPB.IndicadorProductoID,
		IPB.OrganizacionID,
		IPB.RangoMinimo,
		IPB.RangoMaximo,
		IPB.Activo
		, I.Descripcion
		, I.IndicadorID
	FROM IndicadorProductoBoleta IPB
	INNER JOIN IndicadorProducto IP
		ON (IPB.IndicadorProductoID = IP.IndicadorProductoID)
	INNER JOIN Indicador I
		ON (IP.IndicadorID = I.IndicadorID
			AND I.IndicadorID = @IndicadorID)
	SET NOCOUNT OFF;
END

GO
