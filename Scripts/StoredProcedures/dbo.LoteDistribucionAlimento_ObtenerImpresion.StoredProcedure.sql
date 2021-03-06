USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteDistribucionAlimento_ObtenerImpresion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteDistribucionAlimento_ObtenerImpresion]
GO
/****** Object:  StoredProcedure [dbo].[LoteDistribucionAlimento_ObtenerImpresion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jorge Luis Velazquez Araujo
-- Create date: 17/12/2014
-- Description:	Obtiene los datos para la impresion de la Distribucion de Alimento
-- LoteDistribucionAlimento_ObtenerImpresion 1,'20141201'
-- =============================================
CREATE PROCEDURE [dbo].[LoteDistribucionAlimento_ObtenerImpresion] @OrganizacionID INT
	,@FechaDistribucion DATE
AS
BEGIN
	SELECT 
	lda.Fecha
	,us.Nombre AS Lector
	,co.Codigo AS Corral
	,cr.NumeroEconomico AS Camion
	,es.Descripcion AS Estatus
	,es.DescripcionCorta As DescripcionCorta
	,ts.TipoServicioID
	,ts.Descripcion AS TipoServicio	
	FROM LoteDistribucionAlimento lda
	INNER JOIN Usuario us on lda.UsuarioCreacionID = us.UsuarioID
	INNER JOIN Lote lo on lda.LoteID = lo.LoteID
	INNER JOIN Corral co on lo.CorralID = co.CorralID
	INNER JOIN Reparto re on re.LoteID = lo.LoteID
	inner join RepartoDetalle rd on re.RepartoID = rd.RepartoID and lda.TipoServicioID = rd.TipoServicioID
	inner join CamionReparto cr on rd.CamionRepartoID = cr.CamionRepartoID
	inner join Estatus es on lda.EstatusDistribucionID = es.EstatusID
	inner join TipoServicio ts on lda.TipoServicioID = ts.TipoServicioID
	WHERE lo.OrganizacionID = @OrganizacionID
	and lo.Activo = 1
	and CAST(lda.Fecha AS DATE) = @FechaDistribucion
	and CAST(re.Fecha AS DATE) = @FechaDistribucion	
END

GO
