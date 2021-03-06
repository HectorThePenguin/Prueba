USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SupervisionDetectores_ObtenerImpresion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SupervisionDetectores_ObtenerImpresion]
GO
/****** Object:  StoredProcedure [dbo].[SupervisionDetectores_ObtenerImpresion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 19/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : SupervisionDetectores_ObtenerImpresion 1,'20141126'
--======================================================
CREATE PROCEDURE [dbo].[SupervisionDetectores_ObtenerImpresion] @OrganizacionID INT
	,@FechaSupervision DATE
AS
BEGIN
	SET NOCOUNT ON;
	SELECT sd.SupervisionDetectoresID
		,o.Descripcion AS Organizacion
		,op.Nombre + ' ' + op.ApellidoPaterno + ' ' + op.ApellidoMaterno AS Detector
		,sd.FechaSupervision
		,cs.Descripcion AS CriterioSupervision
		,cs.ValorInicial
		,cs.ValorFinal
		,cs.CodigoColor
		,sd.Observaciones
		,sd.Activo
	FROM SupervisionDetectores sd
	INNER JOIN Organizacion o ON sd.OrganizacionID = o.OrganizacionID
	INNER JOIN Operador op ON sd.OperadorID = op.OperadorID
	INNER JOIN CriterioSupervision cs ON sd.CriterioSupervisionID = cs.CriterioSupervisionID
	WHERE sd.OrganizacionID = @OrganizacionID
		AND CAST(sd.FechaSupervision AS DATE) = @FechaSupervision
	SELECT sdd.SupervisionDetectoresID
		,pr.PreguntaID
		,pr.Descripcion Pregunta
		,sdd.Respuesta
		,sdd.Activo
	FROM SupervisionDetectores sd
	INNER JOIN SupervisionDetectoresDetalle sdd ON sd.SupervisionDetectoresID = sdd.SupervisionDetectoresID
	INNER JOIN Pregunta pr ON sdd.PreguntaID = pr.PreguntaID
	WHERE sd.OrganizacionID = @OrganizacionID
		AND CAST(FechaSupervision AS DATE) = @FechaSupervision
	SET NOCOUNT OFF;
END

GO
