USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerConsumoTotalDelDia]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerConsumoTotalDelDia]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerConsumoTotalDelDia]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Roque.Solis
-- Create date: 2014/03/25
-- Description: SP para el consumo total del dia de un lote
-- Origen     : APInterfaces
-- EXEC Reparto_ObtenerConsumoTotalDelDia 4, 37
-- =============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerConsumoTotalDelDia]
@OrganizacionID INT,
@CorralID INT
AS
BEGIN
	SELECT SUM(RD.CantidadServida) ConsumoTotal
	FROM RepartoDetalle RD (NOLOCK)
	INNER JOIN Reparto R (NOLOCK) ON RD.RepartoID=R.RepartoID
	-- INNER JOIN Lote L (NOLOCK) ON R.LoteID=L.LoteID
	INNER JOIN Corral C (NOLOCK) ON R.CorralID=C.CorralID
	WHERE CAST(R.Fecha AS DATE) = CAST(GETDATE() AS DATE)
	AND RD.TipoServicioID IN (1,2)
	AND R.OrganizacionID = @OrganizacionID
	AND C.CorralID = @CorralID
	AND R.Activo=1
	AND RD.Activo=1
	-- AND L.Activo=1
	AND C.Activo=1
SET NOCOUNT OFF;
END

GO
