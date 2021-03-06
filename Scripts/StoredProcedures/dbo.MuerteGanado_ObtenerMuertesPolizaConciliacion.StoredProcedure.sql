USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[MuerteGanado_ObtenerMuertesPolizaConciliacion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[MuerteGanado_ObtenerMuertesPolizaConciliacion]
GO
/****** Object:  StoredProcedure [dbo].[MuerteGanado_ObtenerMuertesPolizaConciliacion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gilberto Carranza
-- Create date: 09-07-2014
-- Description:	Obtiene los datos para generar la poliza de salida
-- MuerteGanado_ObtenerMuertesPolizaConciliacion 1, '20151021', '20151021'
-- =============================================
CREATE PROCEDURE [dbo].[MuerteGanado_ObtenerMuertesPolizaConciliacion]
@OrganizacionID INT
, @FechaInicial DATE
, @FechaFinal DATE
AS
BEGIN

	SET NOCOUNT ON

		DECLARE @TipoMovimientoMuerte INT
		SET @TipoMovimientoMuerte = 8

			SELECT MAX(AnimalMovimientoID)	AS AnimalMovimientoID
				,  ah.AnimalID
				,  ah.Arete
				,  OrganizacionID
				,  MAX(CAST(FechaMovimiento AS DATE)) AS FechaMovimiento
			FROM AnimalMovimientoHistorico amh(NOLOCK)
			INNER JOIN AnimalHistorico ah on amh.AnimalID = ah.AnimalID
			WHERE TipoMovimientoID = @TipoMovimientoMuerte	
				AND OrganizacionID = @OrganizacionID
				AND CAST(FechaMovimiento AS DATE) BETWEEN @FechaInicial AND @FechaFinal
			GROUP BY ah.AnimalID
				,	 ah.Arete
				,	 OrganizacionID

	SET NOCOUNT OFF

END

GO
