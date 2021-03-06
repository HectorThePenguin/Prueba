USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerRepartosAjustados]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerRepartosAjustados]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerRepartosAjustados]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Jorge Luis Velazquez Araujo
-- Create date: 29/10/2014
-- Description:	Obtiene los repartos ajustados
-- EXEC Reparto_ObtenerRepartosAjustados '20150910', 1
-- 001 Jorge Luis Velazquez Araujo 10/09/2015 **Se agrega la Organizacion de  los Repartos
--=============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerRepartosAjustados] @Fecha DATE
	,@Activo BIT
	,@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #RepartosAjustados (RepartoID BIGINT)
	INSERT INTO #RepartosAjustados
	SELECT RepartoID
	FROM Reparto re
	WHERE CAST(re.Fecha AS DATE) = @Fecha
		AND re.Activo = @Activo
		AND EXISTS (
			SELECT TOP 1 ''
			FROM RepartoDetalle rd
			WHERE re.RepartoID = rd.RepartoID
				AND rd.TipoServicioID = 2
				AND rd.Ajuste = 1
			)
	AND re.OrganizacionID = @OrganizacionID
	SELECT re.RepartoID
		,OrganizacionID
		,LoteID
		,Fecha
		,PesoInicio
		,PesoProyectado
		,DiasEngorda
		,PesoRepeso
		,Activo
		,CorralID
	FROM Reparto re
	INNER JOIN #RepartosAjustados ra ON re.RepartoID = ra.RepartoID
	WHERE re.Activo = @Activo
	AND re.OrganizacionID = @OrganizacionID
	SELECT RD.RepartoDetalleID
		,RD.RepartoID
		,RD.TipoServicioID
		,RD.FormulaIDProgramada
		,RD.FormulaIDServida
		,RD.CantidadProgramada
		,RD.CantidadServida
		,RD.HoraReparto
		,RD.CostoPromedio
		,RD.Importe
		,RD.Servido
		,RD.Cabezas
		,RD.EstadoComederoID
		,RD.CamionRepartoID
		,RD.Observaciones
		,RD.Ajuste
	FROM RepartoDetalle RD(NOLOCK)
	INNER JOIN #RepartosAjustados ra ON rd.RepartoID = ra.RepartoID
	WHERE RD.Activo = @Activo	
	SET NOCOUNT OFF;
END

GO
