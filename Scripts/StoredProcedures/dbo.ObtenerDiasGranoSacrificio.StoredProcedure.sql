USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ObtenerDiasGranoSacrificio]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ObtenerDiasGranoSacrificio]
GO
/****** Object:  StoredProcedure [dbo].[ObtenerDiasGranoSacrificio]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 18/09/2014 12:00:00 a.m.
-- Description: Obtiene los dias de aplicacion de los tipos de formula de PRODUCCION,FINALIZACION,RETIRO
-- SpName     : ObtenerDiasGranoSacrificio 1,0
--======================================================
CREATE PROCEDURE [dbo].[ObtenerDiasGranoSacrificio] @OrganizacionID INT
	,@LoteID INT
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #DIASGRANOS (
		LoteID INT
		,DiasGrano INT
		)
	INSERT INTO #DIASGRANOS
	SELECT r.LoteID
		,COUNT(r.RepartoID) Total
	FROM Reparto r
	WHERE r.OrganizacionID = @OrganizacionID 
	AND @LoteID IN (r.LoteID,0)
		AND r.Activo = 1
		AND (
			SELECT COUNT(rd.RepartoDetalleID)
			FROM RepartoDetalle rd
			INNER JOIN Formula fo ON rd.FormulaIDServida = fo.FormulaID
			WHERE r.RepartoID = rd.RepartoID
				AND fo.TipoFormulaID IN (3, 4, 8) --Tipo Formula Produccion, Finalizacion y Retiro
				AND rd.Activo = 1
			) = 2
	GROUP BY r.LoteID
	select 
	LoteID,
	DiasGrano
	from
	#DIASGRANOS	
	where LoteID is not null
	order by LoteID
	SET NOCOUNT OFF;
END

GO
