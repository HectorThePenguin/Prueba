USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimientoHistorico_ObtenerMuertesPorFecha]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalMovimientoHistorico_ObtenerMuertesPorFecha]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimientoHistorico_ObtenerMuertesPorFecha]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 03/10/2014
-- Description: Obtiene las polizas para su conciliacion
-- SpName     : AnimalMovimientoHistorico_ObtenerMuertesPorFecha '20151216'
--======================================================
CREATE PROCEDURE [dbo].[AnimalMovimientoHistorico_ObtenerMuertesPorFecha]
@FechaMuerte	DATE
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @Fecha DATE
	SET @Fecha = @FechaMuerte

	DECLARE @TipoMovimientoMuerte INT
	SET	@TipoMovimientoMuerte = 8

		SELECT AM.AnimalID
			,  AM.OrganizacionID
			,  A.Arete
		FROM AnimalMovimientoHistorico AM(NOLOCK)
		INNER JOIN AnimalHistorico A(NOLOCK)
			ON (AM.AnimalID = A.AnimalID)			
		WHERE TipoMovimientoID = @TipoMovimientoMuerte
			AND CAST(FechaMovimiento AS DATE) = @FechaMuerte
			AND AM.Activo = 1

	SET NOCOUNT OFF

END

GO
