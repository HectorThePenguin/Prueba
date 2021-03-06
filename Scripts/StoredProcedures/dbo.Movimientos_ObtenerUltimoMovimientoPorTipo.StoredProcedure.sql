USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Movimientos_ObtenerUltimoMovimientoPorTipo]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Movimientos_ObtenerUltimoMovimientoPorTipo]
GO
/****** Object:  StoredProcedure [dbo].[Movimientos_ObtenerUltimoMovimientoPorTipo]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Roque.Solis
-- Fecha: 2014-01-13
-- Origen: APInterfaces
-- Descripci�n:	Obtiene La info del ultimo movimiento por tipo
-- EXEC Movimientos_ObtenerUltimoMovimientoPorTipo '48400406522752',9,4
-- =============================================
CREATE PROCEDURE [dbo].[Movimientos_ObtenerUltimoMovimientoPorTipo]
	@Arete VARCHAR(15), 
	@TipoMovimiento INT,
	@OrganizacionIDEntrada INT
AS
BEGIN
	SELECT TOP 1
		AM.AnimalID,
		AM.AnimalMovimientoID,
		AM.OrganizacionID,
		AM.CorralID,
		AM.LoteID,
		AM.FechaMovimiento,
		AM.Peso,
		AM.Temperatura,
		AM.TipoMovimientoID,
		AM.TrampaID,
		AM.OperadorID,
		AM.Observaciones,
		AM.Activo,
		AM.FechaCreacion,
		AM.UsuarioCreacionID
	FROM Animal AS A (NOLOCK) 
	INNER JOIN AnimalMovimiento (NOLOCK) AM ON A.AnimalID = AM.AnimalID 
    INNER JOIN Corral C (NOLOCK) ON AM.CorralID = C.CorralID AND C.Activo = 1
	WHERE A.Arete = @Arete
	AND AM.TipoMovimientoID = @TipoMovimiento
	AND A.OrganizacionIDEntrada = @OrganizacionIDEntrada
 ORDER BY AM.AnimalMovimientoID DESC
END

GO
