USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorCorralesImproductivos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerPorCorralesImproductivos]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorCorralesImproductivos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
=============================================
-- Author:		Jorge Luis Velazquez Araujo
-- Create date: 07/11/2014
-- Description:	Obtiene los Corrales improductivos para la pantalla Corte por Transferencia
-- Corral_ObtenerPorCorralesImproductivos 5
=============================================
*/
CREATE PROCEDURE [dbo].[Corral_ObtenerPorCorralesImproductivos] @TipoCorralID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT co.CorralID
		,co.OrganizacionID
		,co.Codigo
		,co.TipoCorralID
		,co.Capacidad
		,co.MetrosLargo
		,co.MetrosAncho
		,co.Seccion
		,co.Orden
		,co.Activo
	FROM Corral co
	LEFT JOIN Lote lo ON co.CorralID = lo.CorralID AND lo.Activo = 1
	WHERE 1 = 1
		AND lo.FechaCierre IS NULL
		AND ISNULL(lo.Cabezas + 1,0) <= co.Capacidad
		AND co.TipoCorralID = @TipoCorralID
END

GO
