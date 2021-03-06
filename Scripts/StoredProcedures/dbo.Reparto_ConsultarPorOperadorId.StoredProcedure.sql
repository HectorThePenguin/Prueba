USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ConsultarPorOperadorId]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ConsultarPorOperadorId]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ConsultarPorOperadorId]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Ramses Santos
-- Create date: 25/03/2014
-- Description:	Obtiene los repartos asignados al operador.
-- Reparto_ConsultarPorOperadorId 6, 1, ''
--======================================================
CREATE PROCEDURE [dbo].[Reparto_ConsultarPorOperadorId]
	@OperadorID INT,
	@OrganizacionId INT,
	@CodigoCorral [char](10)
AS
BEGIN
	DECLARE @TipoServicio INT
	SELECT @TipoServicio = TipoServicioID 
	FROM TipoServicio (NOLOCK)
	WHERE CONVERT(CHAR(5), GETDATE(), 108)
		BETWEEN HoraInicio AND HoraFin
	SELECT TOP 1 r.RepartoID, r.OrganizacionID, r.LoteID, r.Fecha, r.PesoInicio, r.PesoProyectado,
				r.DiasEngorda, r.PesoRepeso 
	FROM Reparto (NOLOCK) AS r 
	INNER JOIN RepartoDetalle (NOLOCK) rd 
		ON (rd.RepartoID = r.RepartoID 
			AND rd.TipoServicioID = @TipoServicio 
			AND rd.Activo = 1)
	INNER JOIN Lote (NOLOCK) AS l 
		ON (r.LoteID = l.LoteID 
			AND l.Activo = 1 
			AND l.OrganizacionID = r.OrganizacionID ) 
	INNER JOIN Corral (NOLOCK) AS c 
		ON (c.CorralID = l.CorralID 
			AND c.Activo = 1 
			AND c.OrganizacionID = l.OrganizacionID)
	INNER JOIN CorralLector (NOLOCK) AS cl 
		ON (cl.CorralID = c.CorralID 
			AND cl.Activo = 1)
	LEFT OUTER JOIN LoteDistribucionAlimento (NOLOCK) AS lda 
		ON (lda.LoteID = r.LoteID 
			AND rd.TipoServicioID = lda.TipoServicioID)
	WHERE cl.OperadorID = @OperadorID 
		AND r.OrganizacionID = @OrganizacionId 
		AND lda.TipoServicioID IS NULL 
		AND r.Activo = 1
		AND (c.Codigo = @CodigoCorral OR @CodigoCorral = '') 
		AND CAST(r.Fecha AS DATE) = CAST(GETDATE() AS DATE)
	ORDER BY c.Orden
END

GO
