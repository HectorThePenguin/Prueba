USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoBoleta_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorProductoBoleta_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoBoleta_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 24/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorProductoBoleta_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[IndicadorProductoBoleta_ObtenerPorID]
@IndicadorProductoBoletaID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		IndicadorProductoBoletaID,
		IndicadorProductoID,
		OrganizacionID,
		RangoMinimo,
		RangoMaximo,
		Activo
	FROM IndicadorProductoBoleta
	WHERE IndicadorProductoBoletaID = @IndicadorProductoBoletaID
	SET NOCOUNT OFF;
END

GO
