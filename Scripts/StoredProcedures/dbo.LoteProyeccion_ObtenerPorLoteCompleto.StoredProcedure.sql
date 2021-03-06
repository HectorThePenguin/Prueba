USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteProyeccion_ObtenerPorLoteCompleto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteProyeccion_ObtenerPorLoteCompleto]
GO
/****** Object:  StoredProcedure [dbo].[LoteProyeccion_ObtenerPorLoteCompleto]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Jorge Luis Velazquez Araujo
-- Create date: 03/06/2015
-- Description:	Obtener la proyeccion con sus reimplantes.
-- LoteProyeccion_ObtenerPorLoteCompleto 1, 4;
-- =============================================
CREATE PROCEDURE [dbo].[LoteProyeccion_ObtenerPorLoteCompleto]
@LoteID INT,
@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		LoteProyeccionID,
		LoteID,
		OrganizacionID,
		Frame,
		GananciaDiaria,
		ConsumoBaseHumeda,
		Conversion,
		PesoMaduro,
		PesoSacrificio,
		DiasEngorda,
		FechaEntradaZilmax
	FROM LoteProyeccion
	WHERE LoteID = @LoteID
	AND OrganizacionID = @OrganizacionID

	SELECT 
	 lr.LoteReimplanteID
	,lr.LoteProyeccionID
	,lr.NumeroReimplante
	,lr.FechaProyectada
	,lr.PesoProyectado
	,lr.FechaReal
	,lr.PesoReal
	from LoteReimplante lr
	inner join LoteProyeccion lp on lr.LoteProyeccionID = lp.LoteProyeccionID
	WHERE lp.LoteID = @LoteID
	AND OrganizacionID = @OrganizacionID
	

	SET NOCOUNT OFF;
END

GO
