USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoMovimiento_ObtenerTipoMovimientoCalidadPasaeProceso]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoMovimiento_ObtenerTipoMovimientoCalidadPasaeProceso]
GO
/****** Object:  StoredProcedure [dbo].[TipoMovimiento_ObtenerTipoMovimientoCalidadPasaeProceso]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 12/06/2014
-- Description: 
-- SpName     : TipoMovimiento_ObtenerTipoMovimientoCalidadPasaeProceso
--======================================================
CREATE PROCEDURE [dbo].[TipoMovimiento_ObtenerTipoMovimientoCalidadPasaeProceso]
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @PaseProceso INT, @ProduccionFormula INT
	SET @PaseProceso = 25
	SET @ProduccionFormula = 26
	SELECT 
		tm.TipoMovimientoID,
		tm.Descripcion
	FROM TipoMovimiento tm
	WHERE TM.TipoMovimientoID IN (@PaseProceso, @ProduccionFormula)
	SET NOCOUNT OFF;
END

GO
