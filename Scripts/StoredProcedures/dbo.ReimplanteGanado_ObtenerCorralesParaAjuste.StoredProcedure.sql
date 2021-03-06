USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ObtenerCorralesParaAjuste]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReimplanteGanado_ObtenerCorralesParaAjuste]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ObtenerCorralesParaAjuste]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2014/07/21
-- Description: SP para Obtener corrales para ajustes de animales desñues del reimplante de ganado
-- Origen     : APInterfaces
-- EXEC ReimplanteGanado_ObtenerCorralesParaAjuste 1
-- =============================================
CREATE PROCEDURE [dbo].[ReimplanteGanado_ObtenerCorralesParaAjuste] @OrganizacionID INT
AS
BEGIN
	SELECT PR.FolioProgramacionID
		,PR.LoteID
	FROM ProgramacionReimplante PR
	WHERE CONVERT(CHAR(8), PR.Fecha, 112) = CONVERT(CHAR(8), GETDATE(), 112)
		AND PR.Activo = 1
		AND PR.OrganizacionID = @OrganizacionID
		AND EXISTS (
			SELECT AM.LoteID
			FROM AnimalMovimiento AM(NOLOCK)
			WHERE AM.Activo = 1
				AND AM.OrganizacionID = PR.OrganizacionID
				AND AM.LoteID = PR.LoteID
				AND am.TipoMovimientoID = 6 --Movimiento Reimplante
				AND CONVERT(CHAR(8), AM.FechaMovimiento, 112) = CONVERT(CHAR(8), GETDATE(), 112)
			)
END

GO
