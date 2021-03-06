USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ConsultarLeidosPorOperadorId]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ConsultarLeidosPorOperadorId]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ConsultarLeidosPorOperadorId]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Ramses Santos
-- Create date: 25/03/2014
-- Description:	Obtiene los repartos asignados al operador.
-- EXEC Reparto_ConsultarLeidosPorOperadorId 4, 4
--======================================================
CREATE PROCEDURE [dbo].[Reparto_ConsultarLeidosPorOperadorId]
	@OperadorID INT,
	@OrganizacionId INT
AS
BEGIN
	DECLARE @TipoServicio INT
	SELECT @TipoServicio = TipoServicioID FROM TipoServicio (NOLOCK)
	WHERE CAST(DATEPART(HOUR, GETDATE()) AS VARCHAR)+':'+CAST(DATEPART(MINUTE, GETDATE()) AS VARCHAR) 
	BETWEEN HoraInicio AND HoraFin
	SELECT COUNT(r.RepartoID) AS Total FROM Reparto (NOLOCK) AS r 
	INNER JOIN RepartoDetalle (NOLOCK) rd ON (rd.RepartoID = r.RepartoID AND rd.TipoServicioID = @TipoServicio)
	INNER JOIN Lote (NOLOCK) AS l ON (r.LoteID = l.LoteID AND l.Activo = 1 AND l.OrganizacionID = r.OrganizacionID ) 
	INNER JOIN Corral (NOLOCK) AS c ON (c.CorralID = l.CorralID AND c.Activo = 1 AND c.OrganizacionID = l.OrganizacionID)
	INNER JOIN CorralLector (NOLOCK) AS cl ON (cl.CorralID = c.CorralID)
	INNER JOIN LoteDistribucionAlimento (NOLOCK) AS lda ON (lda.LoteID = r.LoteID AND rd.TipoServicioID = lda.TipoServicioID)
	WHERE cl.OperadorID = @OperadorID AND r.OrganizacionID = @OrganizacionId AND CAST(r.Fecha AS DATE) = CAST(GETDATE() AS DATE)
END

GO
